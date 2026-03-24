using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private int _collisionCount = 0;
    
    public bool IsGround => _collisionCount > 0;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
            _collisionCount++;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
            _collisionCount--;
    }
}
