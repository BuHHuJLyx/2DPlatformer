using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundChecker))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    
    [SerializeField] private Rotator _rotator;
    
    private Rigidbody2D _rigidbody;
    private GroundChecker _groundChecker;
    
    public Vector2 LinearVelocity => _rigidbody.linearVelocity;
    public bool IsGround => _groundChecker.IsGround;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
    }
    
    public void Move(float input)
    {
        _rigidbody.linearVelocity = new Vector2(input * _moveSpeed, _rigidbody.linearVelocity.y);
        
        _rotator.Rotate(input);
    }

    public void Jump()
    {
        if (_groundChecker.IsGround)
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
    }
    
    public void Stop()
    {
        _rigidbody.linearVelocity = Vector2.zero;
    }
}