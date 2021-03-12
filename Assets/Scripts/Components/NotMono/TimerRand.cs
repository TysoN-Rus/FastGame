using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimerRand : Timer {
    
    public Vector2 deltaRand;

    public override void PauseTikPause(bool on) {
        deltaTime = Settings.Inctance.MyRand(deltaRand.x, deltaRand.y);
        base.PauseTikPause(on);
    }

    public override void TikPauseTik(bool on) {
        deltaTime = Settings.Inctance.MyRand(deltaRand.x, deltaRand.y);
        base.TikPauseTik(on);
    }

}
