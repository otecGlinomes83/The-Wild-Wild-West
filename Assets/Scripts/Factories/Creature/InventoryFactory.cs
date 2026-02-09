using System.Collections.Generic;

public static class InventoryFactory
{
    public static Inventory Create(List<Weapon> weapons, PlayerContext playerContext)
    {
        Inventory inventory = playerContext.InventoryObject.AddComponent<Inventory>();
        inventory.Setup(weapons);

        return inventory;

    }
}
