using System;
using UnityEngine.SceneManagement;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        public event Action OnBackToMenu;
        
        //TODO: Сделать через Addressables
        public void LoadMenu()
        {
            OnBackToMenu?.Invoke();
            SceneManager.LoadScene("Menu");
            //отгружать локации
        }
    }
}