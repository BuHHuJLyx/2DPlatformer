using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private string _horizontalAxis = "Horizontal";
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _attackKey  = KeyCode.F;
    [SerializeField] private KeyCode _vampirismKey  = KeyCode.E;
    
    private bool _isJump;
    private bool _isAttack;
    private bool _isVampirism;
    
    public float Move { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw(_horizontalAxis);

        if (Input.GetKeyDown(_jumpKey))
            _isJump = true;
        
        if (Input.GetKeyDown(_attackKey))
            _isAttack = true;
        
        if (Input.GetKeyDown(_vampirismKey))
            _isVampirism = true;
    }
    
    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);
    
    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);
    public bool GetIsVampirism() => GetBoolAsTrigger(ref _isVampirism);
    
    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}