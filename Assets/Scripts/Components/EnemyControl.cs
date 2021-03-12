using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControl : MonoBehaviour {

    public BasePlane planeActive;
    public TimerRand timerStartAttack;
    public TimerRand timerStopAttack;

    private void Start() {
        timerStartAttack.EvSignal.AddListener(StartAttack);
        timerStopAttack.EvSignal.AddListener(StopAttack);
        StartAttack();
    }

    private void StartAttack() {
        Changed(true);
    }

    private void StopAttack() {
        Changed(false);
    }

    private void Changed(bool val) {
        planeActive.fire.Attack(val);
        timerStopAttack.PauseTikPause(val);
        timerStartAttack.PauseTikPause(!val);
    }

    public UnityEvent<EnemyControl> EvDisable = new UnityEvent<EnemyControl>();

    public void Disable() {
        planeActive.fire.Attack(false);
        timerStopAttack.PauseTikPause(false);
        timerStartAttack.PauseTikPause(false);
        timerStopAttack.EvSignal.RemoveListener(StopAttack);
        timerStartAttack.EvSignal.RemoveListener(StartAttack);
        EvDisable.Invoke(this);
    }

    public void OnEnable() {
        Start();
    }

    private void OnDisable() {
        Disable();
    }
}
