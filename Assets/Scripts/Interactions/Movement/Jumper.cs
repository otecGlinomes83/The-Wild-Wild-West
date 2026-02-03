using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    private JumperData _jumperData;
    private GroundDetector _groundDetector;
    private Rigidbody _rigidbody;

    private Vector3 _jumpDirection;

    private bool _isAbleToJump = false;

    public bool IsJump => _isAbleToJump == false;

    private void OnDisable()
    {
        _groundDetector.TargetLost -= OnGroundLost;
        _groundDetector.TargetDetected -= OnGroundDetected;
    }

    public void Setup(JumperData jumperData, Rigidbody rigidbody, GroundDetector groundDetector)
    {
        _jumperData = jumperData;
        _groundDetector = groundDetector;
        _rigidbody = rigidbody;

        _jumpDirection = new Vector3(0f, _jumperData.Force, 0f);

        _groundDetector.TargetDetected += OnGroundDetected;
        _groundDetector.TargetLost += OnGroundLost;
    }

    public void TryJump()
    {
        if (_isAbleToJump)
        {
            _rigidbody.AddForce(_jumpDirection, ForceMode.Impulse);
        }
    }

    private void OnGroundLost() =>
        _isAbleToJump = false;

    private void OnGroundDetected(Collider collider) =>
        _isAbleToJump = true;
}
