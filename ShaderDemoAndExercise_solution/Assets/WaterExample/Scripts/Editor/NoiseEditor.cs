using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[Serializable]
[CustomEditor(typeof(NoiseGenerator))]
public class NoiseEditor : Editor {

    [SerializeField]
    private int size = 512;

    private int dsIterations = 8;
    private float dsVariation = .5f;
    private float dsRoughness = .75f;
    private float dsSeed = .5f;

    private int caIterations = 4;
    private int caNeighborhoodSize = 1;
    private CellularNoise.NeighbourhoodType caNeighborhoodType = CellularNoise.NeighbourhoodType.VonNeumann;
    private float caLimit = .5f;
    private float caVariance = .5f;
    private float caDecay = .5f;

    public override void OnInspectorGUI() {
        NoiseGenerator generator = target as NoiseGenerator;

        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        size = EditorGUILayout.IntSlider("Size", size, 20, 1024);

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("Diamond Square", EditorStyles.boldLabel);

        dsIterations = EditorGUILayout.IntSlider("Iterations", dsIterations, 1, 12);
        dsVariation = EditorGUILayout.Slider("Variation", dsVariation, 0f, 1f);
        dsRoughness = EditorGUILayout.Slider("Roughness", dsRoughness, 0f, 1f);
        dsSeed = EditorGUILayout.Slider("Seed", dsSeed, 0f, 1f);

        if (GUILayout.Button("Diamond Square")) {
            generator.DiamonSquare(size, dsIterations, dsVariation, dsRoughness, dsSeed);
        }

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("Cellular", EditorStyles.boldLabel);

        caIterations = EditorGUILayout.IntSlider("Iterations", caIterations, 1, 15);
        caNeighborhoodSize = EditorGUILayout.IntSlider("Neighborhood Size", caNeighborhoodSize, 1, 12);
        caLimit = EditorGUILayout.Slider("Limit", caLimit, 0f, 1f);
        caVariance = EditorGUILayout.Slider("Variance", caVariance, 0f, 1f);
        caDecay = EditorGUILayout.Slider("Decay", caDecay, 0f, 1f);

        caNeighborhoodType = (CellularNoise.NeighbourhoodType) EditorGUILayout.EnumPopup("Neighborhood Type", caNeighborhoodType);

        if (GUILayout.Button("Cellular")) {
            generator.Cellular(size, caNeighborhoodType, caIterations, caNeighborhoodSize, caLimit, caVariance, caDecay);
        }

        GUILayout.Space(10f);

        if (generator.HasTexture() && GUILayout.Button("Save")) {
            Save(generator.Image, generator.TypeName);
        }

        if (generator.HasTexture() && GUILayout.Button("Add to mesh generator")) {
            generator.AddToMeshGenerator();
        }

        if (GUILayout.Button("Clear")) {
            generator.Clear();
        }
    }

    private void Save(Texture2D image, string typeName) {
        string fileName = FileIO.ExportToPNG(image.EncodeToPNG(), typeName);
        AssetDatabase.Refresh();

        string path = string.Format("Assets{0}{1}", FileIO.relativePath, fileName);

        AssetDatabase.ImportAsset(path);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        importer.textureType = TextureImporterType.Default;
        importer.isReadable = true;

        AssetDatabase.WriteImportSettingsIfDirty(path);
    }
}
