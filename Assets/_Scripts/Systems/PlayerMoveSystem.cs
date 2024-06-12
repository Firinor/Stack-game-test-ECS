using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private EcsWorld world;

    private VisibleFloatingJoystick joystick;
    private Player player;
    private PlayerConfiguration playerConfiguration;

    private EcsFilter<PlayerNextMovePoint> filter;

    private bool runAnimation = false;
    public void Run()
    {
        if (joystick.Direction == Vector2.zero)
        {
            if (runAnimation)
            {
                player.IdleAnimation();
                runAnimation = false;
            }
            return;
        }

        if (!runAnimation)
        {
            player.RunAnimation();
            runAnimation = true;
        }

        Vector3 nextPoint = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        nextPoint.Normalize();

        if (filter.IsEmpty())
        {
            world.NewEntity().Get<PlayerNextMovePoint>().NextPoint = nextPoint;
        }
        else
        {
            foreach (var it in filter)
            {
                filter.Get1(it).NextPoint = nextPoint;
            }
        }
    }
}

public class PlayerMoveFixedSystem : IEcsRunSystem
{
    private Player player;
    private PlayerConfiguration playerConfiguration;

    private EcsFilter<PlayerNextMovePoint> filter;

    private float gravity = -10;

    public void Run()
    {
        if (filter.IsEmpty())
        {
            return;
        }

        Vector3 nextPoint = filter.Get1(0).NextPoint;
        player.transform.LookAt(player.transform.position + nextPoint);

        Vector3 playerVelosity = new Vector3(nextPoint.x * playerConfiguration.Speed, gravity, nextPoint.z * playerConfiguration.Speed);
        player.body.AddForce(playerVelosity, ForceMode.VelocityChange);

        if (player.transform.position.y < -50)
            player.transform.position = Vector3.zero;

        player.cameraRoot.LookAt(player.transform.position + Vector3.forward);
    }
}

