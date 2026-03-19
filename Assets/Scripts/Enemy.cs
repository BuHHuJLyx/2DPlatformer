using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _distance = 0.5f;
    [SerializeField] private Transform _groundDetection;
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
        
        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distance);

        if (groundInfo.collider == null)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        _moveSpeed *= -1;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(_moveSpeed);
        transform.localScale = scale;
    }
}