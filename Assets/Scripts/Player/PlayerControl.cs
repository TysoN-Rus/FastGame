using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CameraControl camControl;
    public float speed = 10;
    public bool cameraTracking = false;

    Rigidbody rb;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.freezeRotation = true;
        if (!camControl)
        {
            camControl = GetComponentInChildren<CameraControl>();
            if (!camControl)
            {
                Debug.LogError("Нет CameraControl в дочерних объектах");
            }
        }
        if (cameraTracking)
        {
            camControl.ignoreRotationY = cameraTracking;
            camControl.rotationY = transform.eulerAngles.y;
        }
    }

    void LateUpdate()
    {
        if (cameraTracking)
        {
            if (!Cursor.visible)
            {
                camControl.rotationY += Input.GetAxis("Mouse X") * camControl.sensitivityY;
                transform.localRotation = Quaternion.Euler(0, camControl.rotationY, 0);
            }
        }
        rb.velocity = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * speed + transform.up * rb.velocity.y;
    }

}