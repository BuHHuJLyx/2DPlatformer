using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool _isGrounded;
    
    public bool IsGrounded => _isGrounded;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
        {
            _isGrounded = false;
        }
    }
}
