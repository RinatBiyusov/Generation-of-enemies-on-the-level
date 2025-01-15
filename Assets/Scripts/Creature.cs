using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Creature : MonoBehaviour
{
    [SerializeField] private int _timeTimer = 10; 

    private Rigidbody _rigidbody;

    public event Action<Creature> ActionEnd;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Destroyer));
    }

    private IEnumerator Destroyer()
    {
        Debug.Log("Nyanya");

        yield return new WaitForSeconds(_timeTimer);

        ActionEnd?.Invoke(this);
    }
}
