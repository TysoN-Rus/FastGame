using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class Counter {

    public int valueTik;
    private int countNow = 0;

    [HideInInspector] public UnityEvent EvSignal = new UnityEvent();

    public void Incriment() {
        countNow++;
        if (countNow >= valueTik) {
            EvSignal.Invoke();
            countNow = 0;
        }
    }

    internal void Reset() {
        countNow = 0;
    }
}
