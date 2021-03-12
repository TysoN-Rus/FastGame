using UnityEngine.Events;
using UnityEngine;
using System;

[Serializable] public class Health {

    public float max;
    private float value;

    public float Value { get => value;
        set {
            this.value = Mathf.Clamp(value, 0, max);
            EvChangeNormal.Invoke(value / max);
        }
    }

    public float GetNormal() {
        return value / max;
    }

    public Health() {
        value = max;
    }

    public void Reset() {
        value = max;
    }

    [HideInInspector] public UnityEvent<float> EvChangeNormal = new UnityEvent<float>();

    public void DeltaHealth(float val) {
        value += val;

        value = Mathf.Clamp(value, 0, max);

        EvChangeNormal.Invoke(value / max);
    }

}
