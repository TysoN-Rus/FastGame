using UnityEngine;
public class LevelUp : MonoBehaviour, ISetPlayerPlane {
    public void SetPlayerPlane(PlayerPlane playerPlane) {
        playerPlane.GetLevel().Incriment();
        SoundMaster.Inctance.LevelUp();
        gameObject.SetActive(false);
    }
}
