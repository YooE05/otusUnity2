using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        public event Action OnBackToMenu;

        public void LoadMenu()
        {
            OnBackToMenu?.Invoke();
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}