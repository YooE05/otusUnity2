using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Homeworks.UpgradeManager
{
    public sealed class StationHandler : MonoBehaviour
    {
        public float TimeToTransform => _timeToTransform;

        [SerializeField] private int _initPutAreaSlots;
        [SerializeField] private int _initOutAreaSlots;
        [SerializeField] private ObjectSpawnArea _putArea;
        [SerializeField] private ObjectSpawnArea _outArea;

        [SerializeField] private float _timeToTransform = 3f;
        [SerializeField] private Slider _slider;
        
        private CancellationTokenSource _cancellationTokenSource;
        private void Awake()
        {
            _putArea.InitSpawnArea(_initPutAreaSlots);
            _outArea.InitSpawnArea(_initOutAreaSlots);
            _cancellationTokenSource = new CancellationTokenSource();

            _slider.value = 0f;
        }

        private void Start()
        {
            StartTransformProcess(_cancellationTokenSource.Token).Forget();
        }

        [ShowInInspector]
        public void PutResourcesToStation(int amountToPut,int allResAmount, out int restAmount)
        {
            if (_putArea.TryTakeSlot(amountToPut, out var restResources))
            {
                restAmount = allResAmount - amountToPut + restResources;
            }

            restAmount = allResAmount;
        }

        [ShowInInspector]
        public void CollectAllResourcesFromOutArea()
        {
            _outArea.ReleaseAllSlots();
        }

        [ShowInInspector]
        public void SetTransformationSpeed(float newTime)
        {
            _timeToTransform = newTime;
        }

        [ShowInInspector]
        public void IncreasePutCapacity(int newPutCapacity)
        {
            _putArea.SetTotalSlotsCount(newPutCapacity);
        }

        [ShowInInspector]
        public void SetTotalSlotsCount(int newOutCapacity)
        {
            _outArea.SetTotalSlotsCount(newOutCapacity);
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
            var startCountdown = countdown;
            while (countdown >= 0)
            {
                if (token.IsCancellationRequested) break;

                Debug.Log($"Transformation end in: {Math.Round(countdown, 2)} seconds");
                countdown -= 0.1f;
                _slider.value = Mathf.Lerp(0f, 1f, (startCountdown - countdown) / startCountdown);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
            }

            _slider.value = 0f;
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
}