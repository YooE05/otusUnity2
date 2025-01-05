using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class UpgradeStationHandler : MonoBehaviour
{
    [SerializeField] private int _initPutAreaSlots;
    [SerializeField] private int _initOutAreaSlots;
    [SerializeField] private ObjectSpawnArea _putArea;
    [SerializeField] private ObjectSpawnArea _outArea;

    [SerializeField] private int _totalResourceValue;
    [SerializeField] private int _putResourceValue;

    [SerializeField] private float _timeToTransform = 3f; //будем менять

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _putArea.InitSpawnArea(_initPutAreaSlots);
        _outArea.InitSpawnArea(_initOutAreaSlots);
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void Start()
    {
        StartTransformProcess(_cancellationTokenSource.Token).Forget();
    }

    [ShowInInspector]
    public void AddResourcesToStation()
    {
        if (_putArea.TryTakeSlot(_putResourceValue, out var restResources))
        {
            _totalResourceValue = _totalResourceValue - _putResourceValue + restResources;
        }
    }

    [ShowInInspector]
    public void CollectAllResourcesFromOutArea()
    {
        _outArea.ReleaseAllSlots();
    }

    private async UniTaskVoid StartTransformProcess(CancellationToken token)
    {
        while (true)
        {
            if (!(_putArea.HasTakenSlots && _outArea.HasEmptySlots))
            {
                await UniTask.DelayFrame(1, cancellationToken: token);
                continue;
            }

            await AsyncCountdown(_timeToTransform, token).SuppressCancellationThrow();
            TransformResources();
        }
    }

    private async UniTask AsyncCountdown(float countdown, CancellationToken token)
    {
        while (countdown >= 0)
        {
            if (token.IsCancellationRequested) break;

            countdown -= 0.1f;
            Debug.Log($"Transformation end in: {Math.Round(countdown, 2)} seconds");
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
        }
    }

    private void CancelTransformation()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void TransformResources()
    {
        if (_outArea.TryTakeSlot(1, out var restResources))
        {
            _putArea.ReleaseSlot();
        }
    }
}