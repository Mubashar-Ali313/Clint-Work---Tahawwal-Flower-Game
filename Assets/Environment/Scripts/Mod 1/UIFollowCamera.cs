using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Camera cam;
    public float distance = 2f; // How far in front of the camera
    public Vector3 offset = Vector3.zero; // Optional position offset (not rotation)

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // Always in front of camera (with optional offset)
        transform.position = cam.transform.position + cam.transform.forward * distance + offset;

        // Exactly match camera's rotation (no rotational offset)
        transform.rotation = cam.transform.rotation;
    }
}
