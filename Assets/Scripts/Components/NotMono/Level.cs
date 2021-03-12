using System;
using UnityEngine.Events;

public class Level {
    private int value = 0;

    public int Value { get => value;
        set {
            this.value = value;
            EvChange.Invoke();
        }
    }

    public UnityEvent EvChange = new UnityEvent();

    public void Incriment() {
        value++;
        EvChange.Invoke();
    }

    internal void Reset() {
        value = 0;
    }
}
