using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Homeworks.UpgradeManager
{
    public sealed class StationHandler : MonoBehaviour
    {
        public event Action OnResourceTransformed;
        public event Action OnNewTransformSpeedSet;
        public float TimeToTransform => _timeToTransform;

        [SerializeField] private ObjectSpawnArea _putArea;
        [SerializeField] private ObjectSpawnArea _outArea;

        [SerializeField] private Slider _slider;

        private float _timeToTransform;

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _slider.value = 0f;
        }

        private void Start()
        {
            StartTransformProcess(_cancellationTokenSource.Token).Forget();
        }

        public void PutResourcesToStation(int amountToPut, int allResAmount, out int restAmount)
        {
            if (_putArea.TryTake(amountToPut, out var restResources))
            {
                restAmount = allResAmount - amountToPut + restResources;
            }
            else
            {
                restAmount = allResAmount;
            }
        }

        public void CollectAllResourcesFromOutArea()
        {
            _outArea.ReleaseAll();
        }

        public void SetTransformationSpeed(float newTime)
        {
            _timeToTransform = newTime;
            OnNewTransformSpeedSet?.Invoke();
        }

        public void SetPutCapacity(int newPutCapacity)
        {
            _putArea.SetCapacityCount(newPutCapacity);
        }

        public void SetOutCapacity(int newOutCapacity)
        {
            _outArea.SetCapacityCount(newOutCapacity);
        }

        public int GetOutCapacity()
        {
            return _outArea.Capacity;
        }

        public int GetPutCapacity()
        {
            return _putArea.Capacity;
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

                //Debug.Log($"Transformation end in: {Math.Round(countdown, 2)} seconds");
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
            if (_outArea.TryTake(1, out var restResources))
            {
                _putArea.Release();
                OnResourceTransformed?.Invoke();
            }
        }
    }
}