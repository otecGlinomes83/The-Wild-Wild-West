using UnityEngine;

public class Rotator : MonoBehaviour
{
    private PlayerContext _playerContext;
    private RotatorData _rotatorData;

    private float _currentVerticalRotation;

    public void Setup(RotatorData rotatorData, PlayerContext playerContext)
    {
        _rotatorData = rotatorData;
        _playerContext = playerContext;
    }

    public void Rotate(Vector2 mouseDelta)
    {
        float mouseX = mouseDelta.x * _rotatorData.Sensitivity * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        _currentVerticalRotation += mouseDelta.y * _rotatorData.Sensitivity * Time.deltaTime;
        _currentVerticalRotation = Mathf.Clamp(_currentVerticalRotation, _rotatorData.MinRotation, _rotatorData.MaxRotation);
        _playerContext.CameraPivotObject.transform.localRotation = Quaternion.Euler(-_currentVerticalRotation, 0f, 0f);
    }
}
