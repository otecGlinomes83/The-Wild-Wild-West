using UnityEngine;

public static class JumperFactory
{
    public static Jumper Create(JumperData jumperData, PlayerContext playerContext, Rigidbody rigidbody, GroundDetector groundDetector)
    {
        Jumper jumper = playerContext.JumperObject.AddComponent<Jumper>();
        jumper.Setup(jumperData, rigidbody, groundDetector);

        return jumper;
    }
}
