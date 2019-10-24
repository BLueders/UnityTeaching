using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoiseGenerator : ToolSingleton<NoiseGenerator> {

    private static GameObject canvasPrefab;

    private Texture2D _image;
    private string _typeName;

    private Renderer _canvas;

    public Texture2D Image { get { return _image; } }
    public string TypeName { get { return _typeName; } }

    private Renderer Canvas {
        get {
            if (_canvas == null) {
                _canvas = GetComponentInChildren<Renderer>();
            }
            return _canvas;
        }
        set {
            _canvas = value;
        }
    }


    void Awake() {
        Clear();
    }

    public bool HasTexture() {
        return _image != null;
    }

    public void AddToMeshGenerator() {
        _image.name = _typeName;
        MeshGenerator.Instance.textures.Add(_image);
    }

    public void Clear() {
        if (Canvas != null) {
            DestroyImmediate(_canvas.gameObject);
            Canvas = null;
        }
        _image = null;
    }

    public void DiamonSquare(int size, int iterations, float variation, float roughness, float seed) {
        DiamondSquareNoise noise = new DiamondSquareNoise(size, iterations, variation, roughness, seed);
        float[,] values = noise.Generate();

        Generate(values);

        _typeName = "DS";
    }

    public void Cellular(int size, CellularNoise.NeighbourhoodType type, int iterations, int neighborhoodSize, float limit, float variance, float decay) {
        CellularNoise noise = new CellularNoise(size, type, iterations, neighborhoodSize, limit, variance, decay);
        float[,] values = noise.Generate();

        Generate(values);

        _typeName = "CA";
    }

    private void Generate(float[,] values) {

        if (_canvas == null) {
            if (canvasPrefab == null) {
                canvasPrefab = Resources.Load<GameObject>("Prefabs/Canvas");
            }

            GameObject obj = Instantiate<GameObject>(canvasPrefab);
            obj.name = "Canvas";
            obj.transform.SetParent(transform);
            _canvas = obj.GetComponent<Renderer>();
        }

        int w = values.GetLength(0);
        int h = values.GetLength(1);

        _image = new Texture2D(w, h);
        _image.wrapMode = TextureWrapMode.Clamp;

        Color[] pixels = _image.GetPixels();

        int i = 0;

        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++) {
                i = y * w + x;

                float value = values[x, y];
                SetBlackAndWhite(ref pixels[i], value);
            }
        }

        _image.SetPixels(pixels);
        _image.Apply();
        _canvas.sharedMaterial.mainTexture = _image;

    }

    private void SetBlackAndWhite(ref Color pixel, float value) {
        pixel.r = value;
        pixel.g = value;
        pixel.b = value;
        pixel.a = 1f;
    }
}

