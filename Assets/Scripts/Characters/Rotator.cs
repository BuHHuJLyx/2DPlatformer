using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Quaternion _rightRotation = Quaternion.Euler(0,0,0);
    private Quaternion _leftRotation = Quaternion.Euler(0,180,0);

    public void Rotate(float direction)
    {
        if (direction > 0)
            transform.rotation = _rightRotation;
        
        if (direction < 0)
            transform.rotation = _leftRotation;
    }
}
