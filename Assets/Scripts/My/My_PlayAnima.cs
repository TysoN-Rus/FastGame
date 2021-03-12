using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_PlayAnima : MonoBehaviour {

    public Animator animator;
    public string nameAnima;
    public int layer = 0;

    public int singl = -1;

    public float speed = 1;
    public float state = 0;

    private void Start() {
        animator.Play(nameAnima, layer);
    }

    private void Update() {
        state += Time.deltaTime * singl * speed;
        state = Mathf.Clamp(state, 0, 1);

        animator.SetFloat(nameAnima, state);
    }

    public void Reverse() {
        singl *= -1;
    }

    public void Break() {
        state = 0;
        singl = -1;
    }

    private void OnDisable() {
        Break();
    }
}