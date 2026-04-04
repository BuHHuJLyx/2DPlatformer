using UnityEngine;

public abstract class HealthView<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected Health Health;
    
    protected T Component;
    
    private void Awake()
    {
        Component = GetComponent<T>();
    }
    
    protected void OnEnable()
    {
        Health.Changed += UpdateView;
    }

    protected void OnDisable()
    {
        Health.Changed -= UpdateView;
    }

    protected abstract void UpdateView(int currentValue);
}
