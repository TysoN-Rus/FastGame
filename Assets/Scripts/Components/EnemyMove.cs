using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float minMaxScrinX;
    public float nextZ;
    public TimerRand timer;
    public Vector2 speedMove;

    private void Start() {
        timer.EvSignal.AddListener(NextPointX);
        timer.PauseTikPause(true);
        NextPointZ();
    }

    protected float newX;
    protected float newZ;

    public virtual void NextPointX() {
        OnEnable();
        newX = Settings.Inctance.MyRand(-minMaxScrinX, minMaxScrinX);
    }

    public virtual void NextPointZ() {
        newZ = nextZ;
    }

    private bool fastMove = false;

    private void Update() {
        GoPoint();
    }

    private float stateX = 0;
    private float stateY = 0;

    private Vector3 defPos;

    private float x;
    private float z;
    private void GoPoint() {

        stateX += Time.deltaTime * speedMove.x;// / 1000;
        stateY += Time.deltaTime * speedMove.y;// / 1000;

        x = Mathf.Lerp(defPos.x, newX, stateX);
        z = Mathf.Lerp(defPos.z, newZ, stateY);

        transform.position = new Vector3(x, 0, z);
    }

    private void OnEnable() {
        stateX = 0;
        stateY = 0;
        defPos = transform.position;
    }

}
