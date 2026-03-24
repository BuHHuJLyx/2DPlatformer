using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : PickupItem<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (item) => SpawnItem(item),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            actionOnDestroy: (item) => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void SpawnItem(T item)
    {
        Transform spawnPoint = GetRandomSpawnPoint();
 
        item.Activate(spawnPoint.position);

        item.Collected += ReturnToPool;
        item.Expired += ReturnToPool;
    }

    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[index];
    }

    private void GetFromPool()
    {
        _pool.Get();
    }

    private void ReturnToPool(T item)
    {
        item.Collected -= ReturnToPool;
        item.Expired -= ReturnToPool;
        _pool.Release(item);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds delay = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            GetFromPool();
            yield return delay;
        }
    }
}
