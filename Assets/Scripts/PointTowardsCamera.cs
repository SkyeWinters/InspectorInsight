using UnityEngine;

public class PointTowardsCamera : MonoBehaviour
{
    [SerializeField] private GameObject _objectToFace;
    [SerializeField] private Camera _cameraToFace;

    private void Update()
    {
        // Calculate the direction to the camera
        Vector3 directionToCamera = _cameraToFace.transform.position - _objectToFace.transform.position;

        // Zero out the y component to keep the object upright
        directionToCamera.y = 0;

        // Set the rotation to look at the camera with an upright orientation
        _objectToFace.transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}