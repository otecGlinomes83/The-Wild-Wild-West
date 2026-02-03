using UnityEngine;

public class WeaponContext : MonoBehaviour
{
    [SerializeField] private Transform _detectorStartPoint;

    public Transform DetectorStartPoint => _detectorStartPoint;
}