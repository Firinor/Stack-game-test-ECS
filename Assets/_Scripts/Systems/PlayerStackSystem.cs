using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackSystem : IEcsRunSystem
{
    public Stack<EcsEntity> items = new();

    private Player player;
    private PlayerData playerData;
    private GameUIObjects gameUI;
    private PlayerConfiguration playerConfiguration;

    private readonly EcsFilter<PickupRequest> filterPickup;
    private readonly EcsFilter<UnloadRequest> filterUnload;

    private readonly float distance = .3f;

    public void Run()
    {
        foreach (var it in filterPickup)
        {
            AddItem(ref filterPickup.GetEntity(it));
        }
        foreach (var it in filterUnload)
        {
            UnloadItems(ref filterUnload.GetEntity(it));
        }
    }

    private void UnloadItems(ref EcsEntity ecsEntity)
    {
        if (items.Count <= 0)
            return;

        int itemID = ecsEntity.Get<UnloadRequest>().ID;

        int counter = 0;

        while (true)
        {
            if (items.Count <= 0)
                break;
            if(items.Peek().Get<ItemComponent>().ID != itemID)
                break;

            EcsEntity itemEntity = items.Pop();

            ItemComponent itemComponent = itemEntity.Get<ItemComponent>();

            GameObject ItemGO = itemComponent.Item.gameObject;
            Object.Destroy(ItemGO);
            itemEntity.Destroy();

            playerData.TotalScore += 100;
            counter++;
        }

        if(counter != 0)
            RemuveItemsFromPlayerData(itemID, counter);

        DrawUIAddItem();
    }

    private void AddItem(ref EcsEntity itemEntity)
    {
        if (items.Count >= playerConfiguration.StackMax)
            return;

        items.Push(itemEntity);

        ref ItemComponent item = ref itemEntity.Get<ItemComponent>();

        Transform itemTransform = item.Item.transform;

        itemTransform.SetParent(player.itemsRoot, worldPositionStays: false);
        itemTransform.localPosition = new Vector3(0, items.Count * distance, 0);

        itemEntity.Del<FreeItem>();

        AddItemToPlayerData(item.ID);

        DrawUIAddItem();
    }

    private void AddItemToPlayerData(int ID)
    {
        switch (ID)
        {
            case 0:
                playerData.SphereCount++;
                break;
            case 1:
                playerData.BoxCount++;
                break;
            default:
                break;
        }
    }

    private void RemuveItemsFromPlayerData(int ID, int count)
    {
        switch (ID)
        {
            case 0:
                playerData.SphereCount -= count;
                break;
            case 1:
                playerData.BoxCount -= count;
                break;
            default:
                break;
        }
    }

    private void DrawUIAddItem()
    {
        gameUI.SphereText.text = playerData.SphereCount.ToString();
        gameUI.BoxText.text = playerData.BoxCount.ToString();
        gameUI.PlayerScore.text = playerData.TotalScore.ToString();
    }
}
