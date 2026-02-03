using UnityEngine;

public static class AimerFactory
{
    public static Aimer Create(AimerData aimerData, PlayerContext playerContext)
    {
        Aimer rotator = playerContext.AimerObject.AddComponent<Aimer>();
        rotator.Setup(aimerData, playerContext);

        return rotator;
    }
}
