using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class My_Tuggel : MonoBehaviour {

    public Transform zero, one;
    public bool state;
    public UnityEvent change;

    private void Start() {
        if (state) {
            transform.position = one.position;
            transform.rotation = one.rotation;
            change.Invoke();
        } else {
            transform.position = zero.position;
            transform.rotation = zero.rotation;
        }
    }

    private void OnMouseUpAsButton() {
        state = !state;
        change.Invoke();
        Start();
    }

    public void SetState(bool val) {
        state = val;
        Start();
    }
}
