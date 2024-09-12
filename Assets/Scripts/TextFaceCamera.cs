using UnityEngine;

public class TextFaceCamera : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}