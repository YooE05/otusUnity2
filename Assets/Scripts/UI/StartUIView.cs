using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    [Serializable]
    public class StartUIView 
    {
        [SerializeReference] private Button _startButton;
        [SerializeReference] private TextMeshProUGUI _countDownText;

        public Button StartButton => _startButton;
        public TextMeshProUGUI CountDownText => _countDownText;
    }
}