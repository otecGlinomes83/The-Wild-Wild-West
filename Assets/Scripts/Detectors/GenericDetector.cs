using UnityEngine;

public class GenericDetector<T> : MonoBehaviour where T : Component
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _detectingLayer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public bool TryDetect(out T detectTarget)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _detectingLayer);

        detectTarget = null;

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