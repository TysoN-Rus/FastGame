using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMaster : MonoBehaviour {
    public static SoundMaster Inctance;

    public AudioMixer audioMixer;

    public AudioClip boom;
    public AudioClip shotShield;
    public AudioClip repair;
    public AudioClip levelUp;
    public AudioClip shieldCharge;

    private AudioSource main;
    private void Awake() {
        if (Inctance == null) {
            Inctance = this;
        } else if (Inctance == this) {
            Destroy(gameObject);
            return;
        }
        main = GetComponent<AudioSource>();
        if (!main) {
            Debug.LogError("Нет компонента AudioSource");
        }
    }

    public void SliderMusic(float val) {
        audioMixer.SetFloat("Music", Mathf.Lerp(-80,0,val));
    }

    public void SliderSoundc(float val) {
        audioMixer.SetFloat("Sound", Mathf.Lerp(-80, 0, val));
    }

    public void PlayClipBoom() {
        main.PlayOneShot(boom);
    }

    public void ShotShield() {
        main.PlayOneShot(shotShield);
    }

    public void RepairPick() {
        main.PlayOneShot(repair);
    }

    public void LevelUp() {
        main.PlayOneShot(levelUp);
    }

    public void ShieldCharge() {
        main.PlayOneShot(shieldCharge);
    }


}
