using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CreatureSpawner : MonoBehaviour
{
    private readonly int _poolCapacity = 10;
    private readonly int _maxPoolCapacity = 15;

    [SerializeField] private float _repeateRate = 2;
    [SerializeField] private Creature _creature;
    [SerializeField] private Target _target;

    private ObjectPool<Creature> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Creature>(
        createFunc: () => Instantiate(_creature),
        actionOnGet: (creature) => ActionOnget(creature),
        actionOnRelease: (creature) => creature.gameObject.SetActive(false),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _maxPoolCapacity);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void ActionOnget(Creature creature)
    {
        creature.Reached += Release;
        creature.Init(_target);
        creature.transform.position = transform.position;
        creature.transform.rotation = transform.rotation;
        creature.gameObject.SetActive(true);
    }

    private void GetCreature()
    {
        _pool.Get();
    }

    private void Release(Creature creature)
    {
        creature.Reached -= Release;
        creature.transform.position = transform.position;
        StopCreture(creature);

        _pool.Release(creature);
    }

    private void StopCreture(Creature creature)
    {
        creature.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            GetCreature();
            yield return new WaitForSeconds(_repeateRate);
        }
    }
}
