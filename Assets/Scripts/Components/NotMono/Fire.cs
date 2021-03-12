using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Fire {

    public Transform firePoints;
    public Timer timer;

    public GameObject shellPref;
    public float speedShell;
    private int countFire;

    public AudioSource fireSound;

    public WhoIsIt whoIsIt { get; set; }

    private Transform parentShell;

    public void Initialize() {
        countFire = firePoints.childCount;
        timer.EvSignal.AddListener(OnAttack);
    }

    [HideInInspector] public bool onAttack;

    public void Attack(bool on) {
        onAttack = on;
        timer.TikPauseTik(onAttack);
    }

    private void OnAttack() {
        for (int i = 0; i < countFire; i++) {
            GameObject temp = Creator.Inctance.GetPoolGO(shellPref, firePoints.GetChild(i).transform.position);
            fireSound.pitch = Settings.Inctance.MyRand(0.9f, 1.1f);
            fireSound.PlayOneShot(fireSound.clip);


            //temp.transform.position = firePoints.GetChild(i).transform.position;
            temp.transform.rotation = firePoints.GetChild(i).transform.rotation;
            Shell shell = temp.GetComponent<Shell>();
            shell.SetSpeed(speedShell);
            shell.whoIsIt = whoIsIt;
        }
    }

    public void Destroy() {
        timer.TikPauseTik(false);
        timer.EvSignal.RemoveListener(OnAttack);
    }
}
