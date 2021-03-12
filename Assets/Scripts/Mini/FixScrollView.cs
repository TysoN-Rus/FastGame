using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScrollView : MonoBehaviour {
    private void Start() {
        var bee = GetComponent<RectTransform>().anchoredPosition;
        bee.y = 0;
        GetComponent<RectTransform>().anchoredPosition = bee;
    }
}
