using System.Collections;
using UnityEngine;

public class MousePositionConverter : MonoBehaviour
{
    private Camera _mainCamera;
    private PlayerInputHandler _inputHandler;

    private bool _isSetupFinished = false;

    private void OnDrawGizmos()
    {
        if (_isSetupFinished == false)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.075f);
    }

    public void Setup(Camera camera, PlayerInputHandler playerInputHandler)
    {
        _mainCamera = camera;
        _inputHandler = playerInputHandler;

        _isSetupFinished = true;

        StartCoroutine(UpdateState());
    }

    private IEnumerator UpdateState()
    {
        yield return null;

        while (enabled)
        {
            Ray ray = _mainCamera.ScreenPointToRay(_inputHandler.MousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
                transform.position = hit.point;

            yield return null;
        }
    }
}
