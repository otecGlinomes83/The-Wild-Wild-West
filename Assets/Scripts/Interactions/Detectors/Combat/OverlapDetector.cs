using System;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour, IDamageableDetector
{
    private WeaponContext _weaponContext;
    private DetectionData _detectionData;

    public event Action<HitInfo> Hit;

    private void OnDrawGizmos()
    {
        if (_detectionData == null || _weaponContext.DetectorStartPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_weaponContext.DetectorStartPoint.position, _detectionData.Radius);
    }

    public void Setup(DetectionData detectionData, WeaponContext weaponContext)
    {
        _detectionData = detectionData;
        _weaponContext = weaponContext;
    }

    public List<IDamageable> Detect()
    {
        List<IDamageable> damageablers = new List<IDamageable>();

        Collider[] results = new Collider[_detectionData.DetectionCount];

        int resultsCount = Physics.OverlapSphereNonAlloc(_weaponContext.DetectorStartPoint.position, _detectionData.Radius, results, _detectionData.DetectLayer, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < resultsCount; i++)
        {
            Collider currentCollider = results[i];
            Vector3 hitPoint = currentCollider.ClosestPoint(_weaponContext.DetectorStartPoint.position);

            Vector3 origin = _weaponContext.DetectorStartPoint.position;
            Vector3 direction = (currentCollider.ClosestPoint(origin) - origin).normalized;
            Ray ray = new Ray(origin, direction);

            if (Physics.Raycast(ray, out RaycastHit info, _detectionData.Radius, _detectionData.DetectLayer))
            {
                if (info.collider.gameObject.TryGetComponent(out IDamageable damageable) == false)
                {
                    Hit?.Invoke(new HitInfo(HitType.Obstacle, _weaponContext.DetectorStartPoint.position, info.point, Vector3.zero, info.normal, 0));
                    continue;
                }

                Hit?.Invoke(new HitInfo(HitType.Target, _weaponContext.DetectorStartPoint.position, info.point, Vector3.zero, info.normal, 0f));
                damageablers.Add(damageable);
            }
        }

        return damageablers;
    }
}