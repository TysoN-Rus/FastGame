#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Testing;



public class CreateElementsTest : MonoBehaviour
{
    [TextArea(10,500)]
    public string bee;
    

    [ContextMenu("Создать")]
    public void CreateVariantForSelectedPrefab() {
        var q = bee.Split('#');
        for (int i = 0; i < q.Length; i++) {
            var a = q[i].Split('@');
            ElementForTest asset = ScriptableObject.CreateInstance<ElementForTest>();
            asset.question = a[0];
            asset.answer = new string[a.Length - 1];
            int num = 1;
            var t = bee[0];
            for (int j = 1; j < a.Length; j++) {
                if (a[j][0] == '+') {
                    asset.answer[0] = a[j].Remove(0, 1);
                } else {
                    asset.answer[num] = a[j];
                    num++;
                }
            }
            AssetDatabase.CreateAsset(asset, "Assets/Temp/Test/Generaite/ElementTest " + i + ".asset");
        }
        AssetDatabase.SaveAssets();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#endif