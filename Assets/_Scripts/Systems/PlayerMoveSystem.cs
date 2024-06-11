using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private VisibleFloatingJoystick joystick;
    private Player player;
    private PlayerConfiguration playerConfiguration;

    private float yZero = 0;

    public void Run()
    {
        if (joystick.Direction == Vector2.zero)
        {
            player.IdleAnimation();
            return;
        }

        Vector3 nextPoint = new Vector3(joystick.Direction.x, player.transform.position.y, joystick.Direction.y);
        Vector3 playerPoint = new Vector3(player.transform.position.x, yZero, player.transform.position.z);

        player.transform.LookAt(playerPoint + nextPoint);
        player.transform.position += nextPoint * playerConfiguration.Speed * Time.deltaTime;
        player.RunAnimation();

        if (player.transform.position.y < -50)
            player.transform.position = Vector3.zero;

        player.cameraRoot.rotation = Quaternion.Euler(0, player.transform.rotation.y, 0);
    }
}
