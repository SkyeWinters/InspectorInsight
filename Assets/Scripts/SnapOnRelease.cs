using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnapOnRelease : MonoBehaviour
{
    [SerializeField] BoxCollider _snapTo;
    [SerializeField] MeshRenderer _materialDisplay;
    [SerializeField] Material _canPlaceMaterial;
    [SerializeField] Material _canNotPlaceMaterial;

    private Rigidbody _rigidBody;
    private bool grabbed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!grabbed) return;

        _materialDisplay.material = _snapTo.bounds.Contains(transform.position) ? _canPlaceMaterial : _canNotPlaceMaterial;
    }

    public void OnGrab()
    {
        grabbed = true;
        _rigidBody.constraints = RigidbodyConstraints.None;
        _rigidBody.useGravity = true;
    }

    public void OnRelease()
    {
        Debug.Log("Released");
        grabbed = false;
        _materialDisplay.material = _canNotPlaceMaterial;

        if (_snapTo.bounds.Contains(transform.position))
        {
            transform.position = new Vector3(_snapTo.transform.position.x, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(-90, 0, -90);
            _rigidBody.useGravity = false;
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("In Area");
        }
    }
}
