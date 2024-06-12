using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnItemsSystem : IEcsRunSystem
{
    private EcsWorld world;

    private LevelConfiguration level;
    private Prefabs prefabs;
    private SceneObjects sceneObjects;

    private readonly EcsFilter<SpawnRequest> requestFilter;
    private readonly EcsFilter<ItemComponent, FreeItem> itemsFilter;

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
            Transform pickedSpawnPoint = freeSpawnPoints[randomPointIndex];

            Item lastSpawnedItem = Object.Instantiate(prefabs.Items[randomItemIndex], pickedSpawnPoint.position, pickedSpawnPoint.rotation, pickedSpawnPoint);

            EcsEntity itemEntity = world.NewEntity();

            ref ItemComponent itemComponent = ref itemEntity.Get<ItemComponent>();
            itemComponent.ID = randomItemIndex;
            itemComponent.Item = lastSpawnedItem;
            
            itemEntity.Get<FreeItem>();

            lastSpawnedItem.ItemEntity = itemEntity;
        }
    }
}