using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaButton : MonoBehaviour{
    public List<Image> images = new List<Image>();
    public float AlphaThreshold = 0.1f;

    void Awake() {
        for (int i = 0; i < images.Count; i++) {
            images[i].alphaHitTestMinimumThreshold = AlphaThreshold;
        }
    }
}