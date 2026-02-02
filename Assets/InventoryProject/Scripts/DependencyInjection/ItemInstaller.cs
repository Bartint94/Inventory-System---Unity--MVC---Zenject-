using GameplayHud;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class ItemInstaller : MonoInstaller
{
    
    public override void InstallBindings()
    {
        CurrentSceneDi.Container.Bind<InventoryHudController>().AsSingle();
    }
}
