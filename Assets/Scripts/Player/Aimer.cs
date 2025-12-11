using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    [SerializeField] private CinemachineThirdPersonFollow _camera;
    [SerializeField] private float _defaultCameraDistance = 4f;
    [SerializeField] private float _aimCameraDistance = 3f;
    [SerializeField] private float _aimingSpeed = 6f;

    [SerializeField] private Vector3 _defaultDamping = new Vector3(0.3f, 0.3f, 0.3f);
    [SerializeField] private Vector3 _aimDamping = new Vector3(0.1f, 0.1f, 0.1f);

    private Coroutine _aimCoroutine;

    private void Awake()
    {
        LockCursor();
    }

    private void Start()
    {
        _camera.Damping = _defaultDamping;
    }

    private void OnDisable()
    {
        UnlockCursor();
    }

    public void TakeAim()
    {
        if (_aimCoroutine != null)
        {
            StopCoroutine(_aimCoroutine);
            _aimCoroutine = null;
        }

        _camera.Damping = _aimDamping;
        _aimCoroutine = StartCoroutine(SmoothAiming(_aimCameraDistance));
    }

    public void StopAiming()
    {
        if (_aimCoroutine != null)
        {
            StopCoroutine(_aimCoroutine);
            _aimCoroutine = null;
        }

        _camera.Damping = _defaultDamping;
        _aimCoroutine = StartCoroutine(SmoothAiming(_defaultCameraDistance));
    }

    private IEnumerator SmoothAiming(float distance)
    {
        while (Mathf.Approximately(_camera.CameraDistance, distance) == false)
        {
            _camera.CameraDistance = Mathf.MoveTowards(_camera.CameraDistance, distance, _aimingSpeed * Time.deltaTime);
            yield return null;
        }

        yield break;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
