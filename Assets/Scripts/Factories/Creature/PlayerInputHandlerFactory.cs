using UnityEngine;

public static class PlayerInputHandlerFactory
{
    public static PlayerInputHandler Create(PlayerContext playerContext)
    {
        PlayerInputHandler playerInputHandler = playerContext.InventoryObject.AddComponent<PlayerInputHandler>();

        return playerInputHandler;
    }
}
