using UnityEngine;

[CreateAssetMenu(fileName = "NewDetectionData", menuName = "Weapons/DetectionData")]
public class DetectionData : ScriptableObject
{
    [SerializeField] private LayerMask _detectLayer;
    [SerializeField] private DetectType _detectType;

    [Header("Raycast Settings")]
    [SerializeField] private float _spreadDegree = 1f;
    [SerializeField] private bool _isSpreadEnable;
    [SerializeField] private int _rayCount;

    [Header("Overlap Settings")]
    [SerializeField] private int _radius;

    [Header("Generic Settings")]
    [SerializeField] private float _maxDistance;
    [SerializeField] private int _detectionCount;

    public LayerMask DetectLayer => _detectLayer;
    public DetectType DetectType => _detectType;

    public int RayCount => _rayCount;
    public int Radius => _radius;

    public int DetectionCount => _detectionCount;

    public float SpreadAngle => _spreadDegree;
    public bool IsSpreadEnable => _isSpreadEnable;

    public float MaxDistance => _maxDistance;
}