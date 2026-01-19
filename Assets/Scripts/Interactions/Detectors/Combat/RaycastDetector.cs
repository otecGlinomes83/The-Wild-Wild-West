using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaycastDetector : MonoBehaviour, IDamageableDetector
{
    private DetectionData _detectionData;
    private Transform _startPoint;

    public event Action<HitInfo> Hit;

    private void OnDrawGizmos()
    {
        if (_detectionData == null || _startPoint == null)
            return;

        Gizmos.color = Color.orangeRed;
        Gizmos.DrawRay(_startPoint.position, transform.forward);
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
        List<IDamageable> result = new List<IDamageable>();

        for (int i = 0; i < _detectionData.RayCount; i++)
        {
            Vector3 direction = transform.forward;

            if (_detectionData.IsSpreadEnable)
                direction = CalculateSpread() * direction;

            if (TryFindEnemy(direction, out IDamageable damageable))
            {
                if (result.Contains(damageable) == false)
                    result.Add(damageable);
            }
        }

        return result;
    }

    private bool TryFindEnemy(Vector3 direction, out IDamageable damageable)
    {
        damageable = null;

        Ray ray = new Ray(_startPoint.position, direction);
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo, _detectionData.MaxDistance, _detectionData.DetectLayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out damageable))
            {

                Hit?.Invoke(new HitInfo(HitType.Target, _startPoint.position, direction, hitInfo.normal, hitInfo.distance));
                return true;

            }
            else
            {
                Hit?.Invoke(new HitInfo(HitType.Obstacle, _startPoint.position, direction, hitInfo.normal, hitInfo.distance));
                return false;
            }
        }

        return false;
    }

    private Quaternion CalculateSpread()
    {
        float yaw = Random.Range(-_detectionData.SpreadAngle, _detectionData.SpreadAngle);
        float pitch = Random.Range(-_detectionData.SpreadAngle, _detectionData.SpreadAngle);

        Quaternion spreadRotation = Quaternion.Euler(pitch, yaw, 0f);

        return spreadRotation;
    }
}
