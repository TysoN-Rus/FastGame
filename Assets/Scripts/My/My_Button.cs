using UnityEngine;
using UnityEngine.Events;

public class My_Button : MonoBehaviour {

    public UnityEvent click;
    public UnityEvent press;
    public UnityEvent down;
    public UnityEvent up;

    private void OnMouseUpAsButton() {
        click.Invoke();
    }

    private void OnMouseDrag() {
        press.Invoke();
    }

    private void OnMouseDown() {
        down.Invoke();
    }

    private void OnMouseUp() {
        up.Invoke();
    }
}
