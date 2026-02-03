using UnityEngine;

public static class GroundDetectorFactory
{
    public static GroundDetector Create(DetectorData detectorData, PlayerContext playerContext)
    {
        GroundDetector detector = playerContext.DetectorObject.AddComponent<GroundDetector>();
        detector.Setup(detectorData);

        return detector;
    }
}