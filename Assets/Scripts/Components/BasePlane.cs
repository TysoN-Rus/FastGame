using UnityEngine;
using UnityEngine.Events;

public class BasePlane : MonoBehaviour, IDamage {

    public Health health;
    public Fire fire;

    public WhoIsIt whoIsItPlane;

    [HideInInspector] public UnityEvent EvDead ;

    public bool SetDamage(float val, WhoIsIt whoIsIt = WhoIsIt.none) {
        if (whoIsIt == whoIsItPlane) {
            return false;
        } else {
            health.Value -= val;
            Alive();
            return true;
        }
    }

    private void Awake() {
        fire.Initialize();
        EvDead.AddListener(SoundMaster.Inctance.PlayClipBoom);
    }

    private void OnDestroy() {
        fire.Destroy();
    }

    private void OnEnable() {
        health.Reset();
        fire.whoIsIt = whoIsItPlane;
        fire.Attack(false);
    }

    private void Alive() {
        if (health.Value <= 0) {
            EvDead.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        IDamage damageContact = other.gameObject.GetComponentInParent<IDamage>();
        if (damageContact != null) {
            damageContact.SetDamage(int.MaxValue);
            SetDamage(int.MaxValue);
        }
    }
}

