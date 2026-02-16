using System.Collections;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    private PlayerContext _playerContext;
    private AimerData _aimerData;

    private Coroutine _aimCoroutine;

    private void OnDisable()
    {
        UnlockCursor();
    }

    public void Setup(AimerData aimerData, PlayerContext playerContext)
    {
        _aimerData = aimerData;
        _playerContext = playerContext;

        _playerContext.Cinemachine.Damping = _aimerData.DefaultDamping;

        _playerContext.Cinemachine.CameraDistance = _aimerData.DefaultCameraDistance;

        LockCursor();
    }

    public void TakeAim()
    {
        if (_aimCoroutine != null)
        {
            StopCoroutine(_aimCoroutine);
            _aimCoroutine = null;
        }

        _playerContext.Cinemachine.Damping = _aimerData.AimDamping;
        _aimCoroutine = StartCoroutine(SmoothAiming(_aimerData.AimCameraDistance));
    }

    public void StopAiming()
    {
        if (_aimCoroutine != null)
        {
            StopCoroutine(_aimCoroutine);
            _aimCoroutine = null;
        }

        _playerContext.Cinemachine.Damping = _aimerData.DefaultDamping;
        _aimCoroutine = StartCoroutine(SmoothAiming(_aimerData.DefaultCameraDistance));
    }

    private IEnumerator SmoothAiming(float distance)
    {
        while (Mathf.Approximately(_playerContext.Cinemachine.CameraDistance, distance) == false)
        {
            _playerContext.Cinemachine.CameraDistance = Mathf.MoveTowards(_playerContext.Cinemachine.CameraDistance, distance, _aimerData.AimingSpeed * Time.deltaTime);
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
