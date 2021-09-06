using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private EntitySettingsSD settings;
    [Space]
    [SerializeField] private UIManager uiManager;

    [SerializeField] private BaseSpawner spawnerPrefab;
    [SerializeField] private BaseObstacle obstaclePrefab;

    public override void InstallBindings()
    {
        BindPlayer();

        Container.Bind<UIManager>().FromInstance(uiManager);

        BindSpawner();

        Debug.LogError("Binds Local");
    }
    
    private void BindPlayer()
    {
        Container.Bind<EntitySettingsSD>().FromInstance(settings);

        Player player = Container.InstantiatePrefabForComponent<Player>(playerPrefab, Vector2.zero, Quaternion.identity, null);
        Container.Bind<Player>().FromInstance(player).AsSingle();
    }
    private void BindSpawner()
    {
        Container.Bind<BaseObstacle>().FromInstance(obstaclePrefab);
        Container.Bind<ISpawner>().To<BaseSpawner>().FromComponentInNewPrefab(spawnerPrefab).AsSingle();
    }
}