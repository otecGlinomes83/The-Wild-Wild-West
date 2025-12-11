using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivotTransform;

    [SerializeField] private float _sensitivity;
    [SerializeField] private float _maxRotation = 70f;
    [SerializeField] private float _minRotation = -30f;

    private float _currentVerticalRotation;

    public void Rotate(Vector2 mouseDelta)
    {
        float mouseX = mouseDelta.x * _sensitivity * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        _currentVerticalRotation += mouseDelta.y * _sensitivity * Time.deltaTime;
        _currentVerticalRotation = Mathf.Clamp(_currentVerticalRotation, _minRotation, _maxRotation);
        _cameraPivotTransform.localRotation = Quaternion.Euler(-_currentVerticalRotation, 0f, 0f);
    }
}
