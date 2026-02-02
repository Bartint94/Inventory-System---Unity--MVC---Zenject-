using DataBase;
using GameplayHud;
using UnityEngine;
using Zenject;

public class HudInstaller : MonoInstaller
{
    [SerializeField] ItemDataBase itemDatabase;
    [SerializeField] SlotsConfig bpConfig;
    [SerializeField] PoolzSystem pool;
    [SerializeField] Hud hud;
    [SerializeField] Selecter selecter;

    public override void InstallBindings()
    {
        Container.Bind<ItemSpawner>().AsSingle().WithArguments(pool);
        Container.Bind<InventoryHudModel>().AsSingle().WithArguments(itemDatabase, bpConfig.Rows, bpConfig.Columns);

        Container.Bind<InventoryHudController>()
         .AsSingle()
         .WithArguments(FindFirstObjectByType<InventoryView>(), bpConfig, pool);

        Container.BindInstance(selecter).AsSingle();
        Container.BindInstance(hud).AsSingle();
    }
}
