using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator))]
public class MeshEditor : Editor {

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        MeshGenerator generator = target as MeshGenerator;

        if (GUILayout.Button("Create Terrain")) {
            generator.GenerateTerrain();
        }

        if (GUILayout.Button("Create Water")) {
            generator.GenerateWater();
        }

        if (GUILayout.Button("Clear textures")) {
            generator.ClearTextures();
        }
    }
}
