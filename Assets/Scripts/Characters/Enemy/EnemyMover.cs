using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Rotator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    
    private Rigidbody2D _rigidbody;
    private Rotator _rotator;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
    }
    
    public void Move(float direction)
    {
        _rigidbody.linearVelocity = new Vector2(direction * _moveSpeed, _rigidbody.linearVelocity.y);
        
        _rotator.Rotate(direction);
    }
}