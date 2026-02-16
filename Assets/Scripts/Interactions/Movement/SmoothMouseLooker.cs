using UnityEngine;

public class SmoothMouseLooker : MonoBehaviour
{
    private MousePositionConverter _converter;

    private SmoothLookerData _data;

    private bool _isSetupFinished = false;

    public void Setup(SmoothLookerData smoothLookerData, MousePositionConverter mousePositionConverter)
    {
        _data = smoothLookerData;
        _converter = mousePositionConverter;

        _isSetupFinished = true;
    }

    private void LateUpdate()
    {
        if (_isSetupFinished == false)
            return;

        transform.position = Vector3.Lerp(transform.position, _converter.transform.position, _data.Speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
    }
}
