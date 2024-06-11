using Leopotam.Ecs;
using UnityEngine;

public class SpawnTimerSystem : IEcsRunSystem
{
    private EcsWorld world;

    private LevelConfiguration levelConfiguration;

    private EcsFilter<SpawnTimeLeftComponent> filter;

    public void Run()
    {
        if (filter.IsEmpty())
        {
            ref var timer = ref world.NewEntity().Get<SpawnTimeLeftComponent>();
            timer.TimeLeft = levelConfiguration.SpawnSpeed;
        }

        foreach(var it in filter)
        {
            ref var timer = ref filter.Get1(it);
            timer.TimeLeft -= Time.deltaTime;
            if (timer.TimeLeft < 0)
            {
                world.NewEntity().Get<SpawnRequest>();
                timer.TimeLeft = levelConfiguration.SpawnSpeed;
            }
        }
    }
}


