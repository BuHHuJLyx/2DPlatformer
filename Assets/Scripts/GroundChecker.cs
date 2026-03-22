using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGround { get; private set; }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
            IsGround = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
            IsGround = false;
    }
}
