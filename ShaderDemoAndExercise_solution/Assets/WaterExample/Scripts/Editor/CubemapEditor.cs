using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubemapGenerator))]
public class CubemapEditor : Editor {

    public override void OnInspectorGUI() {
 
        DrawDefaultInspector();

        CubemapGenerator generator = target as CubemapGenerator;

        if (GUILayout.Button("Generate")) {
            generator.RenderCubemap();
        }
    }
}
