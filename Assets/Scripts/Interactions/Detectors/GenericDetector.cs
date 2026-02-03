using System.Collections.Generic;
using UnityEngine;

public class GenericDetector<T> : MonoBehaviour
{
    private DetectorData _detectorData;

    private void OnDrawGizmos()
    {
        if (_detectorData == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectorData.Radius);
    }

    public void Setup(DetectorData detectorData)
    {
        _detectorData = detectorData;
    }

    public bool TryDetect(out T detectTarget)
    {
        List<T> detections = new List<T>();

        Collider[] hits = Physics.OverlapSphere(transform.position, _detectorData.Radius, _detectorData.DetectionLayer);

        detectTarget = default;

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.TryGetComponent(out detectTarget))
            {
                return true;
            }
        }

        return false;
    }
}