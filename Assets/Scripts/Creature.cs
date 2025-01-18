using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Mover))]
public class Creature : MonoBehaviour
{
    [SerializeField] private int _timeTimer = 10;

    private Mover _mover;
    private Rigidbody _rigidbody;
    private Target _target;

    public event Action<Creature> ActionEnd;

    public void Init(Target target)
    {
        _target = target;
        _mover.SetTarget(_target);
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Destroyer));
    }

    private IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(_timeTimer);

        ActionEnd?.Invoke(this);
    }
}
