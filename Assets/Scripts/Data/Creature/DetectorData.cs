using UnityEngine;

[CreateAssetMenu(fileName = "NewDetectorData", menuName = "Player/DetectorData")]
public class DetectorData : ScriptableObject
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _detectingLayer;

    public float Radius => _radius;
    public LayerMask DetectionLayer => _detectingLayer;
}