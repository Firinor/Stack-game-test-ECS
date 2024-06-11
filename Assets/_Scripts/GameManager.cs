using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class GameManager : MonoBehaviour
{
    private EcsWorld world;
    private EcsSystems systems;

    [SerializeField]
    private Player player;
    [SerializeField]
    private VisibleFloatingJoystick joystick;
    [SerializeField]
    private Prefabs prefabs;
    [SerializeField]
    private SceneObjects sceneObjects;

    [SerializeField]
    private PlayerConfiguration playerConfiguration;
    [SerializeField]
    private LevelConfiguration levelConfiguration;

    void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif

        systems
            .Add(new PlayerMoveSystem())
            .Add(new PlayerStackSystem())

            .Add(new SpawnTimerSystem())
            .Add(new SpawnItemsSystem())
            .OneFrame<SpawnRequest>()

            .Inject(player)
            .Inject(joystick)
            .Inject(prefabs)
            .Inject(sceneObjects)
            .Inject(playerConfiguration)
            .Inject(levelConfiguration)

            .Init();
    }

    void Update()
    {
        systems?.Run();
    }

    private void OnDestroy()
    {
        systems?.Destroy();
        systems = null;
        world?.Destroy();
        world = null;
    }
}


