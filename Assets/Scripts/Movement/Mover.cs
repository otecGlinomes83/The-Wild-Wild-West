using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _maxWalkSpeed = 6f;
    [SerializeField] private float _acceleration = 5f;

    private Vector2 _currentDirection;

    private float _directionThreshold = 0.2f;
    private float _currentSpeed = 0f;

    public Vector2 CurrentDirection => _currentDirection;
    public float MaxWalkSpeed => _maxWalkSpeed;
    public float Speed => _currentSpeed;

    public void Move(Vector2 direction, bool isRunning)
    {
        if (direction.magnitude < _directionThreshold)
        {
            _currentDirection = Vector2.zero;
            _currentSpeed = 0;
            return;
        }

        _currentDirection = direction;

        float maxSpeed = 0;

        if (isRunning)
            maxSpeed = _maxSpeed;
        else
            maxSpeed = _maxWalkSpeed;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, maxSpeed, _acceleration * Time.deltaTime);
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

        transform.Translate(moveDirection * _currentSpeed * Time.deltaTime);
    }
}
