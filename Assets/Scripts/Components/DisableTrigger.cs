using UnityEngine;
public class DisableTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Rigidbody rigidbody;
        rigidbody = other.GetComponentInParent<Rigidbody>();
        //if (!rigidbody) {
        //    rigidbody = other.GetComponent<Rigidbody>();
        //}

        if (rigidbody) {
            rigidbody.gameObject.SetActive(false);
        }
    }
}