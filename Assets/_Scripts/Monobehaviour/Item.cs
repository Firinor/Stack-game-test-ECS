using Leopotam.Ecs;
using UnityEngine;

public class Item : MonoBehaviour
{
    public EcsEntity ItemEntity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        ItemEntity.Get<PickupRequest>();
    }
}

