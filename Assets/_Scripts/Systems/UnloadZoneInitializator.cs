using Leopotam.Ecs;

public class UnloadZoneInitializator : IEcsInitSystem
{
    private EcsWorld world;
    private SceneObjects sceneObjects;

    public void Init()
    {
        foreach (UnloadZone scene in sceneObjects.UnloadZones)
        {
            EcsEntity zoneEntity = world.NewEntity();
            zoneEntity.Get<UnloadZoneComponent>().ItemToTakeID = scene.ItemToTakeID;
            scene.ZoneEntity = zoneEntity;
        }
    }
}