using UnityEngine;

public class Mover : MonoBehaviour
{
    private MoverData _moverData;

    private Vector2 _currentDirection;

    private float _directionThreshold = 0.2f;
    private float _currentSpeed = 0f;

    public Vector2 CurrentDirection => _currentDirection;
    public float Speed => _currentSpeed;

    public void Setup(MoverData moverData)
    {
        _moverData = moverData;
    }

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
            maxSpeed = _moverData.MaxSpeed;
        else
            maxSpeed = _moverData.MaxWalkSpeed;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, maxSpeed, _moverData.Acceleration * Time.deltaTime);
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

        transform.Translate(moveDirection * _currentSpeed * Time.deltaTime);
    }

    public float GetMaxSpeed() =>
        _moverData.MaxSpeed;
}
