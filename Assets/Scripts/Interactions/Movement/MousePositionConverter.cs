using UnityEngine;

public class MousePositionConverter : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerInputHandler _inputHandler;

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_inputHandler.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Physics.DefaultRaycastLayers,
        QueryTriggerInteraction.Ignore))
            transform.position = hit.point;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.075f);
    }
}
