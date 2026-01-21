using System;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour, IDamageableDetector
{
    private Transform _startPoint;
    private DetectionData _detectionData;

    public event Action<HitInfo> Hit;

    private void OnDrawGizmos()
    {
        if (_detectionData == null || _startPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_startPoint.position, _detectionData.Radius);
    }

    public void Setup(DetectionData detectionData)
    {
        _detectionData = detectionData;
    }

    public void Bind(WeaponContext weaponContext)
    {
        _startPoint = weaponContext.DetectorStartPoint;
    }

    public List<IDamageable> Detect()
    {
        List<IDamageable> damageablers = new List<IDamageable>();
        RaycastHit[] results = new RaycastHit[_detectionData.DetectionCount];
        Ray ray = new Ray(_startPoint.position, transform.forward);

        int resultsCount = Physics.SphereCastNonAlloc(ray, _detectionData.Radius, results, _detectionData.MaxDistance, _detectionData.DetectLayer);

        for (int i = 0; i < resultsCount; i++)
        {
            Vector3 colliderPosition = results[i].transform.position;
            Vector3 hitPoint = results[i].point;
            Vector3 hitNormal = (hitPoint - _startPoint.position).normalized;

            if (results[i].collider.gameObject.TryGetComponent(out IDamageable damageable) == false)
            {
                Hit?.Invoke(new HitInfo(HitType.Obstacle, _startPoint.position, hitPoint, transform.forward, hitNormal, results[i].distance));
                Debug.Log($"<color=red>Obstacle Hit! {gameObject.name}</color>");

                continue;
            }

            Hit?.Invoke(new HitInfo(HitType.Target, _startPoint.position, hitPoint, transform.forward, hitNormal, results[i].distance));
            damageablers.Add(damageable);
        }

        return damageablers;
    }
}