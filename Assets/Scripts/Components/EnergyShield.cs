using System;
using UnityEngine;
using UnityEngine.Events;

public class EnergyShield : MonoBehaviour, IDamage {

    public Timer timer;
    public WhoIsIt shield;


    //public UnityEvent EvShotSield = new UnityEvent();

    private void Start() {
        timer.EvSignal.AddListener(DeactivateShield);
        //EvShotSield.AddListener(SoundMaster.Inctance.ShotShield);
    }

    private void DeactivateShield() {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        timer.PauseTikPause(true);
    }

    public bool SetDamage(float val, WhoIsIt whoIsIt = WhoIsIt.none) {
        if (whoIsIt == shield) {
            return false;
        } else {
            SoundMaster.Inctance.ShotShield();
            //EvShotSield.Invoke();
            return true;
        }
    }
}
