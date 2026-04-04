using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class HealthButton : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected int Value;
    
    protected Button Button;
    
    protected void Awake()
    {
        Button = GetComponent<Button>();
    }
    
    protected void OnEnable()
    {
        Button.onClick.AddListener(Clicked);
    }
    
    protected void OnDisable()
    {
        Button.onClick.RemoveListener(Clicked);
    }
    
    protected abstract void Clicked();
}