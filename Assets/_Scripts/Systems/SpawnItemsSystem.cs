using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SpawnItemsSystem : IEcsRunSystem
{
    private EcsWorld world;

    private LevelConfiguration level;
    private Prefabs prefabs;
    private SceneObjects sceneObjects;

    private readonly EcsFilter<SpawnRequest> requestFilter;
    private readonly EcsFilter<Item> itemsFilter;

    public void Run()
    {
        if (itemsFilter.GetEntitiesCount() >= level.MaxItems)
            return;

        foreach (var it in requestFilter)
        {
            int randomItemIndex = Random.Range(0, prefabs.Items.Length);

            var spawnPoints = from p in sceneObjects.SpawnPoints
                                  where p.childCount == 0
                                  select p;

            if(!spawnPoints.Any())
                return;

            List<Transform> freeSpawnPoints = spawnPoints.ToList();

            int randomPointIndex = Random.Range(0, freeSpawnPoints.Count);

            world.NewEntity().Get<Item>().ID = randomItemIndex;

            Object.Instantiate(prefabs.Items[randomItemIndex], freeSpawnPoints[randomPointIndex].position, Quaternion.identity, freeSpawnPoints[randomPointIndex]);
        }
    }
}


