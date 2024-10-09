using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    [Serializable]
    public class PauseUIView
    {
        [SerializeReference] private Button _pauseButton;
        
        public Button PauseButton => _pauseButton;
    }
}