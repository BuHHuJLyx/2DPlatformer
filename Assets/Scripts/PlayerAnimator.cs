using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int YVelocity = Animator.StringToHash(nameof(YVelocity));
    public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Setup(Vector2 linearVelocity, bool isGrounded)
    {
        _animator.SetFloat(Speed, Mathf.Abs(linearVelocity.x));
        _animator.SetFloat(YVelocity, linearVelocity.y);
        _animator.SetBool(IsGrounded, isGrounded);
    }
}
