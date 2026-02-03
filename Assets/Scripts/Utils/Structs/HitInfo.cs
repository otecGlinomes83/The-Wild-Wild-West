using UnityEngine;

public struct HitInfo
{
    private HitType _hitType;

    private Vector3 _startPoint;
    private Vector3 _hitPoint;
    private Vector3 _direction;
    private Vector3 _hitNormal;

    private float _distance;

    public HitInfo(HitType hitType, Vector3 startPoint, Vector3 hitPoint, Vector3 direction, Vector3 hitNormal, float distance)
    {
        _hitType = hitType;
        _startPoint = startPoint;
        _hitPoint = hitPoint;
        _direction = direction;
        _hitNormal = hitNormal;
        _distance = distance;
    }

    public HitType HitType => _hitType;
    public Vector3 StartPoint => _startPoint;
    public Vector3 Direction => _direction;
    public Vector3 HitNormal => _hitNormal;
    public float Distance => _distance;

    public Vector3 HitPoint => _hitPoint;
}