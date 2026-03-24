using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _attackKey  = KeyCode.F;
    
    private bool _isJump;
    private bool _isAttack;
    
    public float Move { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw(_horizontalAxis);

        if (Input.GetKeyDown(_jumpKey))
            _isJump = true;
        
        if (Input.GetKeyDown(_attackKey))
            _isAttack = true;
    }
    
    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);
    
    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);
    
    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}