using Leopotam.EcsLite;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EcsWorld world;
    IEcsSystems systems;

    void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);
    }

    // Update is called once per frame
    void Update()
    {
        systems?.Run();
    }
}
