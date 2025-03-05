using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chests
{
    public enum ChestType
    {
        Wooden = 0,
        Iron = 1,
        Gold = 2
    }

    public class Chest : MonoBehaviour
    {
        public event Action<ChestType> OnOpened;

        [SerializeField] private Button _openButton;
        [SerializeField] private TextMeshProUGUI _openButtonText;


        [SerializeField] private ChestType _type;
        [SerializeField] private float _cooldown;
        private DateTime _nextAvailableTime;

        private bool CanOpen() => DateTime.Now >= _nextAvailableTime;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(TryOpen);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveAllListeners();
        }

        public void Reset()
        {
            _nextAvailableTime = DateTime.Now.AddSeconds(_cooldown);
        }

        public void SetOpenAbilityView()
        {
            if (CanOpen())
            {
                _openButtonText.text = "open";
                _openButton.image.color = Color.gray;
            }
            else
            {
                var left = DateTime.Now.AddSeconds(-1) - _nextAvailableTime;
                _openButtonText.text = left.ToString(@"hh\:mm\:ss");
                _openButton.image.color = Color.green;
            }
        }

        private void TryOpen()
        {
            if (!CanOpen()) return;

            OnOpened?.Invoke(_type);
            _nextAvailableTime = DateTime.Now.AddSeconds(_cooldown);
        }
    }
}