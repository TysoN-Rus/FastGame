using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

public enum WhoIsIt {
    none,
    Player,
    Enemy,
}

public class Settings : MonoBehaviour {

    public static Settings Inctance;

    public UnityEvent EvStartGame = new UnityEvent();
    public UnityEvent EvStopGame = new UnityEvent();
    public UnityEvent EvWinGame = new UnityEvent();

    public void StartGame() {
        EvStartGame.Invoke();
    }

    public void StopGame() {
        EvStopGame.Invoke();
    }

    public void WinGame() {
        EvWinGame.Invoke();
    }

    private void Awake() {
        if (Inctance == null) {
            Inctance = this;
        } else if (Inctance == this) {
            Destroy(gameObject);
            return;
        }

        Random.InitState((int)DateTime.Now.Ticks);

    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

    public void ExitGame() {
        Application.Quit();
    }

    #region MyRandom

    public float MyRand() {
        return Random.value;
    }

    public float MyRand(float val) {
        return Random.Range(0, val);
    }

    public int MyRand(int val) {
        return Random.Range(0, val);
    }

    public float MyRand(float min, float max) {
        return Random.Range(min, max);
    }

    public int MyRand(int min, int max) {
        return Random.Range(min, max);
    }

    #endregion
}
