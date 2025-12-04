using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _sensitivity;

    public void Rotate(Vector2 mouseDelta)
    {
        float mouseX = mouseDelta.x * _sensitivity;
        transform.Rotate(0, mouseX, 0);
    }
}
