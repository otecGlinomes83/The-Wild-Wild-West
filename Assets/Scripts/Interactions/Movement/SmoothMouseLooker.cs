using UnityEngine;

public class SmoothMouseLooker : MonoBehaviour
{
    [SerializeField] private MousePositionConverter _converter;

    [SerializeField] private float _speed = 75f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _converter.transform.position, _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
    }
}
