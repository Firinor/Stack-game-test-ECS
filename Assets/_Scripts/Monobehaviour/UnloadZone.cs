using Leopotam.Ecs;
using UnityEngine;

public class UnloadZone : MonoBehaviour
{
    public EcsEntity ZoneEntity;

    public int ItemToTakeID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        ZoneEntity.Get<UnloadRequest>().ID = ItemToTakeID;
    }
}
