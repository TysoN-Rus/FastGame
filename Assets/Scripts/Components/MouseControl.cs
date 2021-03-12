using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

    public Vector2 minMaxScrinXY;

    void Update() {
        transform.position = Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public Vector3 Move(Vector3 pos) {
        pos.y = 0;
        pos.x = Mathf.Clamp(pos.x, -minMaxScrinXY.x, minMaxScrinXY.x);
        pos.z = Mathf.Clamp(pos.z, -minMaxScrinXY.y, minMaxScrinXY.y);
        return pos;

    }
}
