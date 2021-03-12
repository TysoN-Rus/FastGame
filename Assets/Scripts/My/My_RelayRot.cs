using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_RelayRot : MonoBehaviour {

    public enum VectorDirection {
        pX, pY, pZ, mX, mY, mZ,
    }
    public VectorDirection selectVectorDirection;

    private float count = 0;
    public float max = 1, min = 0;
    public float sensMouse = 1;
    public float sensRot = 1;

    private Vector3 vectorStart;
    private Vector3 vectorDirection;

//    private bool dragging;

    void Start() {
        count = Mathf.Clamp(count, min, max);
        vectorStart = transform.localEulerAngles;

        switch (selectVectorDirection) {
            case VectorDirection.pX:
                vectorDirection = Vector3.right;
                break;
            case VectorDirection.pY:
                vectorDirection = Vector3.up;
                break;
            case VectorDirection.pZ:
                vectorDirection = Vector3.forward;
                break;
            case VectorDirection.mX:
                vectorDirection = -Vector3.right;
                break;
            case VectorDirection.mY:
                vectorDirection = -Vector3.up;
                break;
            case VectorDirection.mZ:
                vectorDirection = -Vector3.forward;
                break;
            default:
                break;
        }
    }

    //public void OnMouseDown() {
    //    dragging = true;
    //}

    //public void OnMouseUp() {
    //    dragging = false;
    //}
    //private void Update() {
    //    if (dragging) {
    //        count += Input.GetAxis("Mouse X") * sensMouse;
    //        count = Mathf.Clamp(count, min, max);
    //        transform.localEulerAngles = vectorStart + vectorDirection * ((int)count * sensRot);
    //    }
    //}

    private void OnMouseDrag() {
        count += Input.GetAxis("Mouse X") * sensMouse;
        count = Mathf.Clamp(count, min, max);
        transform.localEulerAngles = vectorStart + vectorDirection * ((int)count * sensRot);
    }

    public int GetCount() {
        return (int)count;
    }
}