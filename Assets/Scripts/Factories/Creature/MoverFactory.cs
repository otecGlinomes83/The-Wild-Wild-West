using UnityEngine;

public static class MoverFactory
{
    public static Mover Create(MoverData moverData, PlayerContext playerContext)
    {
        Mover mover = playerContext.MoverObject.AddComponent<Mover>();
        mover.Setup(moverData);

        return mover;
    }
}
