using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Mover))]
public class Creature : MonoBehaviour
{
    [SerializeField] private int _timeTimer = 10;

    private Mover _mover;
    private Target _target;

    public event Action<Creature> Reached;

    public void Init(Target target)
    {
        _target = target;
        _mover.SetTarget(_target);
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        StartCoroutine(Destroyer());
    }

    private IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(_timeTimer);

        Reached?.Invoke(this);
    }
}
