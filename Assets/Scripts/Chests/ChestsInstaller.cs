using System.Collections.Generic;
using Chests;
using UnityEngine;
using Zenject;

public class ChestsInstaller : MonoInstaller
{
    [SerializeField] private List<Chest> _chests = new();

    public override void InstallBindings()
    {
        Container.Bind<ChestService>().AsCached().WithArguments(_chests).NonLazy();
    }
}