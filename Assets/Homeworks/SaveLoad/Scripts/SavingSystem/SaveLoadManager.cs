using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public sealed class SaveLoadManager : MonoBehaviour
    {
        private IGameRepository _gameRepository;
        private DiContainer _container;
        private List<IDataSaver> _dataSavers;

        [Inject]
        private void Construct(IGameRepository gameRepository, DiContainer container, List<IDataSaver> dataSavers)
        {
            _gameRepository = gameRepository;
            _container = container;
            _dataSavers = dataSavers;
        }

        public void SaveGame()
        {
            for (int i = 0; i < _dataSavers.Count; i++)
            {
                _dataSavers[i].SaveData(_gameRepository, _container);
            }

            _gameRepository.SaveState();
        }

        public void LoadGame()
        {
            _gameRepository.LoadState();

            for (int i = 0; i < _dataSavers.Count; i++)
            {
                _dataSavers[i].LoadData(_gameRepository, _container);
            }
        }
    }
}