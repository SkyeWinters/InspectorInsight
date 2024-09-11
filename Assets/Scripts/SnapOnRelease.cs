using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class SnapOnRelease : MonoBehaviour
{
    [SerializeField] private MeshRenderer _materialDisplay;
    [SerializeField] private Material _canPlaceMaterial;
    [SerializeField] private Material _canNotPlaceMaterial;
    [SerializeField] private UnityEvent _onSnap;

    private SnapPoint  _snapPoint;
    private Rigidbody _rigidBody;
    private bool _grabbed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_grabbed) return;

        _materialDisplay.material = _snapPoint.Contains(transform) ? _canPlaceMaterial : _canNotPlaceMaterial;
    }

    public void SetSnapCollider(SnapPoint snapTo)
    {
        _snapPoint = snapTo;
    }

    public void OnGrab()
    {
        _grabbed = true;
        _rigidBody.constraints = RigidbodyConstraints.None;
        _rigidBody.useGravity = true;
    }

    public void OnRelease()
    {
        Debug.Log("Released");
        _grabbed = false;
        _materialDisplay.material = _canNotPlaceMaterial;

        if (!_snapPoint.Contains(transform)) return;
        
        SnapToPoint();
    }

    private void SnapToPoint()
    {
        _snapPoint.SnapToSnapPoint(transform);
        _rigidBody.useGravity = false;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log("In Area");
        _onSnap.Invoke();
    }
}
