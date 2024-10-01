using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Test: MonoBehaviour
    {
        private LevelInstaller _levelInstaller;
        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
               // _gameManager.Initialize();
                _gameManager.OnStart();
            }
        }
    }
}