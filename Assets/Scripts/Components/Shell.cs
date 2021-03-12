using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public float damage = 1;
    private Rigidbody rb;

    public WhoIsIt whoIsIt { get; set; }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float val) {
        rb.velocity = transform.forward * val;
    }

    private void OnTriggerEnter(Collider other) {
        IDamage damageContact = other.gameObject.GetComponentInParent<IDamage>();

        if (damageContact == null) {
            damageContact = other.gameObject.GetComponent<IDamage>();
        }
        
        if (damageContact != null) {
            gameObject.SetActive(!damageContact.SetDamage(damage, whoIsIt));
        }
    }
}
