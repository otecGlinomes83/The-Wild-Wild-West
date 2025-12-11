using UnityEngine;

public class MousePositionConverter : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private Transform _moveTarget;

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_inputHandler.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            _moveTarget.transform.position = hit.point;
    }
}
