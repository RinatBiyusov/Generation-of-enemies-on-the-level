using UnityEngine;

public class Mover : MonoBehaviour
{ 
    [SerializeField] private float _speedMovement = 5f;

    private void Update()
    {
        transform.position += gameObject.transform.forward * Time.deltaTime * _speedMovement;
    }
}
