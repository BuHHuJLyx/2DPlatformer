using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _attackKey  = KeyCode.F;
    
    public event Action JumpPressed;
    public event Action AttackPressed;
    
    public float Move { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw(_horizontalAxis);

        if (Input.GetKeyDown(_jumpKey))
            JumpPressed?.Invoke();
        
        if (Input.GetKeyDown(_attackKey))
            AttackPressed?.Invoke();
    }
}