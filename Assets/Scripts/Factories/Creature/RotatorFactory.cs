using UnityEngine;

public static class RotatorFactory
{
    public static Rotator Create(RotatorData rotatorData, PlayerContext playerContext)
    {
        Rotator rotator = playerContext.MoverObject.AddComponent<Rotator>();
        rotator.Setup(rotatorData, playerContext);

        return rotator;
    }
}
