using UnityEngine;
using Zenject;

public class LocalInstaller : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private Player playerPrefab;
    [SerializeField] private EntitySettingsSD settings;
    [Header("Spawner")]
    [SerializeField] private BaseSpawner spawnerPrefab;
    [SerializeField] private BaseObstacle obstaclePrefab;
    [SerializeField] private int initialSize = 3;
    [Space]
    [SerializeField] private UIManager uiManager;

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
        Container.Bind<IEntity>().To<Player>().FromInstance(player).AsSingle();
    }
    private void BindSpawner()
    {
        //Container.BindFactory<IObstacle, BaseObstacle.ZenjectFactory>().FromComponentInNewPrefab(obstaclePrefab);
        //Container.BindMemoryPool<BaseObstacle, BaseObstacle.ZenjectPool>().WithInitialSize(3).FromComponentInNewPrefab(obstaclePrefab);

        Container.BindFactory<Vector3, Vector3, BaseObstacle, BaseObstacle.ZenjectFactory>()
            .FromMonoPoolableMemoryPool(x => x.WithInitialSize(initialSize).FromComponentInNewPrefab(obstaclePrefab));

        Container.Bind<ISpawner>().To<BaseSpawner>().FromComponentInNewPrefab(spawnerPrefab).AsSingle();
    }
}