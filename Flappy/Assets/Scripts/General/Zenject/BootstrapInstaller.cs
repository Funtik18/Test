using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DataHolder>().AsSingle();
        BindServices();

        Debug.LogError("Binds Global");
    }
    private void BindServices()
    {
        Container.Bind<IScenesService>().To<ScenesManager>().AsSingle();
        Container.Bind<IAudioService>().To<AudioManager>().AsSingle();
    }
}