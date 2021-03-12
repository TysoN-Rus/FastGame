using UnityEngine;

public class Bar : MonoBehaviour {

    public RectTransform bar;
    private Vector2 defPos;
    private float scale;

    void Start() {
        defPos = bar.sizeDelta;
        scale = defPos.x;
    }

    // val 0 - 1 (0 - 100%)
    public void SetVal(float val) {
        defPos.x = scale * val;
        bar.sizeDelta = defPos;
    }
}
