using UnityEngine;
public class EnergyShieldBuff : MonoBehaviour, ISetPlayerPlane {
    public GameObject energyShield;
    public void SetPlayerPlane(PlayerPlane playerPlane) {
        GameObject temp = Creator.Inctance.GetPoolGO(energyShield, playerPlane.transform.position, playerPlane.transform);
        SoundMaster.Inctance.ShieldCharge();
        gameObject.SetActive(false);
    }
}
