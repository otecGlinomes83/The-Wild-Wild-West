using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _force = 10f;

    private Rigidbody _rigidbody;

    private Vector3 _jumpDirection;

    private bool _isAbleToJump = false;

    public bool IsJump => _isAbleToJump == false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _jumpDirection = new Vector3(0f, _force, 0f);
    }

    private void OnEnable()
    {
        _groundDetector.TargetDetected += OnGroundDetected;
        _groundDetector.TargetLost += OnGroundLost;
    }

    private void OnDisable()
    {
        _groundDetector.TargetLost -= OnGroundLost;
        _groundDetector.TargetDetected -= OnGroundDetected;
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