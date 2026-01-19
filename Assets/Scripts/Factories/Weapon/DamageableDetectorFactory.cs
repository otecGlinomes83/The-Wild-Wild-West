using UnityEngine;

public static class DamageableDetectorFactory
{
    public static IDamageableDetector Create(DetectionData detectionData, WeaponContext weaponContext, Transform parent = null)
    {
        GameObject gameobject = new GameObject($"Detector {detectionData.DetectType}");

        if (parent != null)
            gameobject.transform.SetParent(parent, false);

        switch (detectionData.DetectType)
        {
            case DetectType.Raycast:
                {
                    RaycastDetector detector = gameobject.AddComponent<RaycastDetector>();

                    detector.Setup(detectionData);
                    detector.Bind(weaponContext);

                    return detector;
                }

            case DetectType.Overlap:
                {
                    OverlapDetector detector = gameobject.AddComponent<OverlapDetector>();

                    detector.Setup(detectionData);
                    detector.Bind(weaponContext);

                    return detector;
                }

            default:
                {
                    OverlapDetector detector = gameobject.AddComponent<OverlapDetector>();

                    detector.Setup(detectionData);
                    detector.Bind(weaponContext);

                    return detector;
                }
        }
    }
}

