using System.Collections.Generic;

namespace Chests
{
    public class ChestService
    {
        private readonly List<Chest> _chests;
        private readonly Dictionary<ChestType, List<IReward>> _rewardsDictionary = new();

        public ChestService(List<Chest> chests)
        {
            _chests = chests;

            _rewardsDictionary.Add(ChestType.Wooden, new List<IReward> { new SoftCurrencyReward(10, 20) });
            _rewardsDictionary.Add(ChestType.Iron,
                new List<IReward> { new SoftCurrencyReward(20, 50), new ResourceReward("Iron", 5, 10) });
            _rewardsDictionary.Add(ChestType.Gold,
                new List<IReward> { new SoftCurrencyReward(50, 100), new ResourceReward("Gold", 10, 20) });

            for (var i = 0; i < _chests.Count; i++)
            {
                _chests[i].OnOpened += GetReward;
            }
        }

        public void ResetTimers()
        {
            for (var i = 0; i < _chests.Count; i++)
            {
                _chests[i].Reset();
            }
        }

        public void CheckTimersReady()
        {
            for (var i = 0; i < _chests.Count; i++)
            {
                _chests[i].SetOpenAbilityView();
            }
        }

        private void GetReward(ChestType chestType)
        {
            var rewardsList = _rewardsDictionary[chestType];
            for (var i = 0; i < rewardsList.Count; i++)
            {
                rewardsList[i].Give();
            }
        }

        ~ChestService()
        {
            for (var i = 0; i < _chests.Count; i++)
            {
                _chests[i].OnOpened -= GetReward;
            }
        }
    }
}