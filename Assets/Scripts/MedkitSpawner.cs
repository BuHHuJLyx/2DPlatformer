using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private MedkitSpawnPoint[] _spawnPoints;
    [SerializeField] private float _repeatRate = 30f;
    [SerializeField] private int _poolCapacity = 2;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Medkit> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Medkit>(
            createFunc: () => Instantiate(_medkitPrefab),
            actionOnGet: (medkit) => SpawnCoin(medkit),
            actionOnRelease: (medkit) => medkit.gameObject.SetActive(false),
            actionOnDestroy: (medkit) => Destroy(medkit.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnMedkits());
    }

    private void SpawnCoin(Medkit medkit)
    {
        MedkitSpawnPoint spawnPoint = GetRandomSpawnPoint();
 
        medkit.Activate(spawnPoint.Position);

        medkit.Collected += ReturnMedkit;
    }

    private MedkitSpawnPoint GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[index];
    }

    private void GetMedkit()
    {
        _pool.Get();
    }

    private void ReturnMedkit(Medkit medkit)
    {
        medkit.Collected -= ReturnMedkit;
        _pool.Release(medkit);
    }

    private IEnumerator SpawnMedkits()
    {
        WaitForSeconds delay = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            GetMedkit();
            yield return delay;
        }
    }
}
