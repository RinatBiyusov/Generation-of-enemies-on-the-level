using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speedMovement = 5f;

    private Target _target;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speedMovement * Time.deltaTime);
    }

    public void SetTarget(Target target)
    {
        _target = target;
    }
}
