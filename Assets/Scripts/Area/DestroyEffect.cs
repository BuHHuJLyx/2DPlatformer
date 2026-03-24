using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0.3f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}