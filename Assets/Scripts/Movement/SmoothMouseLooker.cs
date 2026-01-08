using UnityEngine;

public class SmoothMouseLooker : MonoBehaviour
{
    [SerializeField] private MousePositionConverter _converter;
    [SerializeField] private float _speed = 75f;

    [SerializeField] private float _minZ = 0f;
    [SerializeField] private float _maxZ = 2f;

    private void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _converter.transform.localPosition, _speed * Time.deltaTime);

        Vector3 localPosition = transform.localPosition;
        localPosition.z = Mathf.Clamp(localPosition.z, _minZ, _maxZ);
        transform.localPosition = localPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
    }
}
