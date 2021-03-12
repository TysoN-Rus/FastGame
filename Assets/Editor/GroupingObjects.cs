using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GroupingObjects : Editor
{
    [MenuItem("GameObject/MyCategory/GroupingObject %g", false, 10)]
    public static void GroupingObject()
    {
        var gameObjects = new List<Transform>(Selection.objects.Select(x => ((GameObject) x).transform));

        if (gameObjects.Count > 1)
        {
            var go = new GameObject().transform;
            Undo.RegisterCreatedObjectUndo(go.gameObject, "Created go");

            if (gameObjects[0].parent)
                go.parent = gameObjects[0].parent;

            Vector3 newPosition = new Vector3();
            foreach (var item in gameObjects)
                newPosition += item.position;
            newPosition /= gameObjects.Count;
            go.position = newPosition;
            
            foreach (var item in gameObjects)
                Undo.SetTransformParent(item, go, "Set new parent");

            Selection.activeObject = go;
            EditorUtility.SetDirty(go);
        }
    }
}