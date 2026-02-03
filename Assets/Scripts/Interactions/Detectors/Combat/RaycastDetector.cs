using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaycastDetector : MonoBehaviour, IDamageableDetector
{
    private DetectionData _detectionData;
    private WeaponContext _weaponContext;

    public event Action<HitInfo> Hit;

    private void OnDrawGizmos()
    {
        if (_detectionData == null || _weaponContext.DetectorStartPoint == null)
            return;

        Gizmos.color = Color.orangeRed;
        Gizmos.DrawRay(_weaponContext.DetectorStartPoint.position, transform.forward);
    }

    public void Setup(DetectionData detectionData, WeaponContext weaponContext)
    {
        _detectionData = detectionData;
        _weaponContext = weaponContext;
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

        Ray ray = new Ray(_weaponContext.DetectorStartPoint.position, direction);
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo, _detectionData.MaxDistance, _detectionData.DetectLayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out damageable))
            {

                Hit?.Invoke(new HitInfo(HitType.Target, _weaponContext.DetectorStartPoint.position, hitInfo.point, direction, hitInfo.normal, hitInfo.distance));
                return true;

            }
            else
            {
                Hit?.Invoke(new HitInfo(HitType.Obstacle, _weaponContext.DetectorStartPoint.position, hitInfo.point, direction, hitInfo.normal, hitInfo.distance));
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
