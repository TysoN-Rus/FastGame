using UnityEngine;
public class Repair : MonoBehaviour, ISetPlayerPlane {
    public float health;
    public void SetPlayerPlane(PlayerPlane playerPlane) {
        playerPlane.GetHealth().DeltaHealth(health);
        SoundMaster.Inctance.RepairPick();
        gameObject.SetActive(false);
    }
}
