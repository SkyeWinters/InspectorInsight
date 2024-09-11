using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SnapPoint : MonoBehaviour
{
    private enum Axis {X, Y, Z}
    
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Axis _axisToAlign;

    private Collider _collider;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    
    public bool Contains(Transform objectToCheck)
    {
        return _collider.bounds.Contains(objectToCheck.position);
    }
    
    public void SnapToSnapPoint(Transform objectToPlace)
    {
        var newPosition = objectToPlace.position;
        newPosition[(int)_axisToAlign] = transform.position[(int)_axisToAlign];
        objectToPlace.position = newPosition;
        objectToPlace.eulerAngles = _rotation;
    }
}