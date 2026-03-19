using UnityEngine;

public class InputReader : MonoBehaviour
{
    public float Move { get; private set; }
    public bool JumpPressed { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            JumpPressed = true;
    }

    public void ResetJump()
    {
        JumpPressed = false;
    }
}