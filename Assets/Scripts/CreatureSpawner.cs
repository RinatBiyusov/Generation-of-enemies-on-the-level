using UnityEngine;
using UnityEngine.Pool;

public class CreatureSpawner : MonoBehaviour
{
    [SerializeField] private float _repeateRate = 2;
    [SerializeField] private Creature _creaturePrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private ObjectPool<Creature> _pool;
    private readonly int _poolCapacity = 10;
    private readonly int _maxPoolCapacity = 15;

    private void Start()
    {
        InvokeRepeating(nameof(GetCreature), 0, _repeateRate);
    }

    private void Awake()
    {
        _pool = new ObjectPool<Creature>(
        createFunc: () => Instantiate(_creaturePrefab),
        actionOnGet: (creature) => ActionOnget(creature),
        actionOnRelease: (creature) => creature.gameObject.SetActive(false),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _maxPoolCapacity);
    }

    private void ActionOnget(Creature creature)
    {
        int numberSpawn = Random.Range(0, _spawnPoints.Length);

        creature.ActionEnd += Release;
        creature.transform.position = _spawnPoints[numberSpawn].position;
        creature.transform.rotation = _spawnPoints[numberSpawn].rotation;
        creature.gameObject.SetActive(true);
    }

    private void GetCreature()
    {
        _pool.Get();
    }

    private void Release(Creature creature)
    {
        creature.ActionEnd -= Release;
        creature.transform.position = transform.position;
        creature.GetComponent<Rigidbody>().velocity = Vector3.zero;

        _pool.Release(creature);
    }
}
