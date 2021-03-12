using UnityEngine;
using UnityEngine.Events;

public class PlayerPlane : MonoBehaviour {

    [SerializeField] private BasePlane[] basePlaneLevel;

    private BasePlane planeActive;
    private Level level = new Level();

    public UnityEvent EvKillPlaeyr = new UnityEvent();
    public UnityEvent<float> EvChenchHeath = new UnityEvent<float>();

    private void Start() {
        Settings.Inctance.EvStartGame.AddListener(StartGame);
    }

    private void StartGame() {
        gameObject.SetActive(true);
        level.Reset();
        level.EvChange.AddListener(LevelUp);
        CreatePlane();
    }

    private void Update() {
        if (planeActive) {
            if (Input.GetMouseButtonDown(0))
                planeActive.fire.Attack(true);
            if (Input.GetMouseButtonUp(0))
                planeActive.fire.Attack(false);
        }
    }

    private void LevelUp() {
        if (level.Value > basePlaneLevel.Length - 1) {
            GetHealth().Reset();
        } else {
            CreatePlane();
        }
    }

    private void CreatePlane() {
        bool fireState = false;

        if (planeActive) {
            fireState = planeActive.fire.onAttack;
            planeActive.EvDead.RemoveListener(TransferEvDead);
            GetHealth().EvChangeNormal.RemoveListener(ChenchHeath);
            Destroy(planeActive.gameObject);
        }

        planeActive = Creator.Inctance.GetGO(basePlaneLevel[level.Value].gameObject, transform.position, transform).GetComponent<BasePlane>();

        planeActive.EvDead.AddListener(TransferEvDead);
        GetHealth().EvChangeNormal.AddListener(ChenchHeath);
        ChenchHeath(GetHealth().GetNormal());
        planeActive.fire.Attack(fireState);
    }

    private void ChenchHeath(float val) {
        EvChenchHeath.Invoke(val);
    }

    private void TransferEvDead() {
        EvKillPlaeyr.Invoke();
        level.EvChange.RemoveListener(LevelUp);
        planeActive.fire.Attack(false);
        gameObject.SetActive(false);
    }

    public Health GetHealth() {
        return planeActive.health;
    }

    public Level GetLevel() {
        return level;
    }

    private void OnTriggerEnter(Collider other) {
        ISetPlayerPlane pickUp = other.gameObject.GetComponentInParent<ISetPlayerPlane>();
        if (pickUp != null) {
            pickUp.SetPlayerPlane(this);
        }
    }
}
