using UnityEngine;
using Zenject;

public class SpawnedObjectsInstaller : MonoInstaller
{
  
    public override void InstallBindings()
    {
        CurrentSceneDi.Container = Container;
    }
     
}
