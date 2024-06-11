using Leopotam.Ecs;
using System.Collections.Generic;

public class PlayerStackSystem : IEcsSystem
{
    public Stack<Item> items;

    private PlayerConfiguration playerConfiguration;

    public void AddItem(Item item)
    {
        if (items.Count >= playerConfiguration.StackMax)
            return;

        items.Push(item);
    }
}
