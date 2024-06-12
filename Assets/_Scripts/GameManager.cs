using Leopotam.Ecs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EcsWorld world;
    private EcsSystems systems;
    private EcsSystems fixedUpdateSystem;

    [SerializeField]
    private Player player;
    [SerializeField]
    private PlayerData playerData = new();
    [SerializeField]
    private VisibleFloatingJoystick joystick;
    [SerializeField]
    private Prefabs prefabs;
    [SerializeField]
    private SceneObjects sceneObjects; 
    [SerializeField]
    private GameUIObjects GameUI;

    [SerializeField]
    private PlayerConfiguration playerConfiguration;
    [SerializeField]
    private LevelConfiguration levelConfiguration;

    void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);
        fixedUpdateSystem = new EcsSystems(world);

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif

        systems
            .Add(new UnloadZoneInitializator())

            .Add(new PlayerMoveSystem())
            .Add(new PlayerStackSystem())

            .Add(new SpawnTimerSystem())
            .Add(new SpawnItemsSystem())

            .OneFrame<SpawnRequest>()
            .OneFrame<PickupRequest>()
            .OneFrame<UnloadRequest>()

            .Inject(player)
            .Inject(playerData)
            .Inject(joystick)
            .Inject(prefabs)
            .Inject(sceneObjects)
            .Inject(GameUI)
            .Inject(playerConfiguration)
            .Inject(levelConfiguration)

            .Init();

        fixedUpdateSystem
            .Add(new PlayerMoveFixedSystem())

            .OneFrame<PlayerNextMovePoint>()

            .Inject(player)
            .Inject(playerConfiguration)

            .Init();
    }

    private void Update()
    {
        systems?.Run();
    }

    private void FixedUpdate()
    {
        fixedUpdateSystem?.Run();
    }

    private void OnDestroy()
    {
        systems?.Destroy();
        systems = null;
        world?.Destroy();
        world = null;
    }
}
