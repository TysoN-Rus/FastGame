using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner Inctance;

    public GameObject[] enemy;

    public GameObject basePlaneBoss;
    public Counter spawnBoss;
    public GameObject levelUpGem;
    public Counter spawnLevelUp;
    public GameObject[] buffPrefabs;
    public TimerRand spawnBuff;

    public TimerRand timerSpawn;
    public float positionZSpawn;
    public Vector2 deltaXUpZ;

    public float defSpeed;

    public int countEnemyMaxScene = 5;

    private int countEnemyNow;

    private void Awake() {
        if (Inctance == null) {
            Inctance = this;
        } else if (Inctance == this) {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        Settings.Inctance.EvStartGame.AddListener(StartSpawn);
        Settings.Inctance.EvStopGame.AddListener(StopGame);
    }

    private void StartSpawn() {
        timerSpawn.EvSignal.AddListener(SpawnPlane);
        spawnLevelUp.EvSignal.AddListener(SpawnLevelGem);
        spawnBoss.EvSignal.AddListener(SpawnBoss);
        spawnBuff.EvSignal.AddListener(SpawnDuff);
        spawnBuff.PauseTikPause(true);
        timerSpawn.TikPauseTik(true);
    }

    private void StopGame() {
        spawnBoss.Reset();
        spawnLevelUp.Reset();
        OnDestroy();
    }

    private void SpawnLevelGem() {
        SetVel(Creator.Inctance.GetPoolGO(levelUpGem, NewPosSpawn()));
    }

    private void SpawnBoss() {
        GameObject temp = Creator.Inctance.GetGO(basePlaneBoss, NewPosSpawn());
        
        temp.transform.forward = Vector3.back;

        timerSpawn.EvSignal.RemoveListener(SpawnPlane);
        spawnBuff.PauseTikPause(false);
        
        temp.GetComponent<BasePlane>().EvDead.AddListener(Settings.Inctance.WinGame);
    }

    private void SpawnDuff() {
        if (buffPrefabs.Length > 0) {
            int num = Settings.Inctance.MyRand(buffPrefabs.Length);
            SetVel(Creator.Inctance.GetPoolGO(buffPrefabs[num], NewPosSpawn()));
        }
    }

    private void SpawnPlane() {
        if (countEnemyNow < countEnemyMaxScene) {
            int num = Settings.Inctance.MyRand(enemy.Length);

            GameObject temp = Creator.Inctance.GetPoolGO(enemy[num].gameObject, NewPosSpawn());
            
            temp.transform.forward = Vector3.back;
            temp.GetComponent<EnemyControl>().EvDisable.AddListener(EnemyDisable);
            temp.GetComponent<EnemyControl>().planeActive.EvDead.AddListener(EnemyDead);
            countEnemyNow++;
        }
    }

    private void SetVel(GameObject temp) {
        temp.GetComponent<Rigidbody>().velocity = Vector3.back * defSpeed;
    }


    public Vector3 NewPosSpawn() {
        Vector3 newPositionSpawn;
        newPositionSpawn.x = Settings.Inctance.MyRand(-deltaXUpZ.x, deltaXUpZ.x);
        newPositionSpawn.y = 0;
        newPositionSpawn.z = deltaXUpZ.y;
        return newPositionSpawn;
    }

    public void EnemyDead() {
        spawnBoss.Incriment();
        spawnLevelUp.Incriment();
        countEnemyNow--;
    }

    public void EnemyDisable(EnemyControl enemy) {
        enemy.EvDisable.RemoveListener(EnemyDisable);
        enemy.planeActive.EvDead.RemoveListener(EnemyDead);
    }

    private void OnDestroy() {
        timerSpawn.EvSignal.RemoveListener(SpawnPlane);
        spawnLevelUp.EvSignal.RemoveListener(SpawnLevelGem);
        spawnBoss.EvSignal.RemoveListener(SpawnBoss);
        spawnBuff.EvSignal.RemoveListener(SpawnDuff);
        spawnBuff.PauseTikPause(false);
        timerSpawn.TikPauseTik(false);
    }

}
