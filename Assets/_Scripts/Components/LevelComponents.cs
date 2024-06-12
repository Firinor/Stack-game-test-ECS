public struct ItemComponent
{
    public int ID;
    public Item Item;
}

public struct FreeItem
{
}

public struct PickupRequest
{
}
public struct UnloadRequest
{
    public int ID;
}
public struct UnloadZoneComponent
{
    public int ItemToTakeID;
}

public struct SpawnTimeLeftComponent
{
    public float TimeLeft;
}

public struct SpawnRequest
{
}