using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour {
    
    public static UI Instance { private set; get; }

    private void Awake() {
        Instance = this;
    }

}
