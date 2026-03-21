using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action JumpPressed;
    public event Action AttackPressed;
    
    public float Move { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            JumpPressed?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.F))
            AttackPressed?.Invoke();
    }
}