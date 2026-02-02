using UnityEngine;
using Zenject;

public class InteractionInstaller : MonoInstaller
{
    [SerializeField] Ui3dView view;
    public override void InstallBindings()
    {
        Container.BindInstance(view).AsSingle();

        Container.Bind<Ui3dController>().AsSingle();    
    }
}
