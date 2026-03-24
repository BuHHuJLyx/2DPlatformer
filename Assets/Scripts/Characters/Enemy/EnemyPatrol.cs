using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance = 0.5f;
    [SerializeField] private Transform _groundDetection;
    
    private float _direction = 1f;
    
    public float GetDirection()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _groundCheckDistance);

        if (groundInfo.collider == null)
            _direction *= -1;
        
        return _direction;
    }
}