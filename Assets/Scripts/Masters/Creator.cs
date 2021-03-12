using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

    public static Creator Inctance;

    private struct Data {
        public List<GameObject> GO;
        public Transform parent;
    }

    private Dictionary<GameObject, Data> poolGO = new Dictionary<GameObject, Data>();

    private List<GameObject> allCreatGO = new List<GameObject>();

    private void Awake() {
        if (Inctance == null) {
            Inctance = this;
        } else if (Inctance == this) {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        Settings.Inctance.EvStopGame.AddListener(StopGame);
    }

    public GameObject GetGO(GameObject target, Vector3 newPosition, Transform parent = null) {
        GameObject temp = Instantiate(target, newPosition, Quaternion.identity);
        temp.transform.SetParent(parent ? parent : transform);
        allCreatGO.Add(temp);
        return temp;
    }

    public GameObject GetPoolGO(GameObject target, Vector3 newPosition, Transform parent = null) {

        if (!poolGO.ContainsKey(target)) {
            CreateKey(target);
        }

        GameObject temp = poolGO[target].GO.Find(x => x.activeSelf == false);
        if (temp) {
            temp.transform.position = newPosition;
            temp.SetActive(true);
        } else {
            temp = GetGO(target, newPosition, parent ? parent : poolGO[target].parent);
            poolGO[target].GO.Add(temp);
        }
        return temp;
    }

    private void CreateKey(GameObject target) {

        List<GameObject> GO = new List<GameObject>();
        Transform parent = new GameObject(target.name).transform;
        parent.SetParent(transform);

        Data data = new Data {
            GO = GO,
            parent = parent
        };

        allCreatGO.Add(parent.gameObject);
        poolGO.Add(target, data);
    }

    private void StopGame() {
        poolGO.Clear();
        for (int i = 0; i < allCreatGO.Count; i++) {
            Destroy(allCreatGO[i]);
        }
    }
}
