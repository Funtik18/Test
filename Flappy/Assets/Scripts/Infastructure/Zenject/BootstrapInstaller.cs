using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ScenesManager>().FromInstance(ScenesManager.Instance);
        Container.Bind<AudioManager>().FromInstance(AudioManager.Instance);
        Debug.LogError("Binds Global");
    }
}