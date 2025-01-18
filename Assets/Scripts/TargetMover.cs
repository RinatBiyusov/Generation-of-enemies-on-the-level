using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _speed;

    private int _currentWayPoint;

    private void Update()
    {
        if (transform.position == _wayPoints[_currentWayPoint].position)
            _currentWayPoint = (_currentWayPoint + 1) % _wayPoints.Length;

        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_currentWayPoint].position, Time.deltaTime * _speed);
    }
}
