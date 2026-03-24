using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int YVelocity = Animator.StringToHash(nameof(YVelocity));
    public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));

    private Animator _animator;

    private float _lastSpeed;
    private float _lastYVelocity;
    private bool _wasGrounded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        if (Mathf.Approximately(_lastSpeed, speed))
            return;

        _animator.SetFloat(Speed, speed);

        _lastSpeed = speed;
    }

    public void SetVerticalVelocity(float yVelocity)
    {
        if (Mathf.Approximately(_lastYVelocity, yVelocity))
            return;

        _animator.SetFloat(YVelocity, yVelocity);

        _lastYVelocity = yVelocity;
    }

    public void SetGrounded(bool isGrounded)
    {
        if (_wasGrounded == isGrounded)
            return;

        _animator.SetBool(IsGrounded, isGrounded);

        _wasGrounded = isGrounded;
    }
}