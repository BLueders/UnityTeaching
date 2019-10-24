using UnityEngine;
using System.Collections;

public class DiamondSquareNoise : Noise {

    private float[,] map;
    public int iterations;
    public float variation, cVariation;
    public float roughness;
    public float seed;
    private int size;

    public DiamondSquareNoise(int size, int iterations, float variation, float roughness, float seed) : base(size) {
        this.iterations = iterations;
        this.variation = variation;
        this.roughness = roughness;
        this.seed = seed;
    }

    public override float[,] Generate() {
        size = (int) Mathf.Pow(2, iterations) + 1;

        Debug.Log(string.Format("Diamond Square size: {0}x{0}", size));

        map = new float[size, size];

        map[0, 0] = seed;
        map[0, size - 1] = seed;
        map[size - 1, 0] = seed;
        map[size - 1, size - 1] = seed;

        for (int i = 0; i < iterations; i++) {

            cVariation = variation * Mathf.Pow(roughness, i);
            if (cVariation < 0) cVariation = 1;

            DiamondSteps(i);
            SquareSteps(i);
        }

        float[,] values = new float[Size, Size];

        float xRatio = (float) (size - 1) / (float) (Size - 1);
        float yRatio = (float) (size - 1) / (float) (Size - 1);

        for (int x = 0; x < Size; x++) {
            for (int y = 0; y < Size; y++) {
                int xM = (int) Mathf.Round(x * xRatio);
                int yM = (int) Mathf.Round(y * yRatio);
                values[x, y] = map[xM, yM];
            }
        }

        return values;
    }

    private void DiamondSteps(int i) {
        int dim = (int) Mathf.Pow(2f, i);
        int step = (size - 1) / dim;

        for (int x = 0; x < size - 1; x += step) {
            for (int y = 0; y < size - 1; y += step) {
                DiamondStep(x, y, step);
            }
        }
    }

    private void DiamondStep(int x, int y, int size) {

        int cx = (int) (.5f * size + x);
        int cy = (int) (.5f * size + y);

        float avg = (map[x, y] + map[x, y + size] + map[x + size, y] + map[x + size, y + size]) / 4f;

        float value = avg + (Random.value - .5f) * cVariation;

        map[cx, cy] = Mathf.Clamp01(value);
    }

    private void SquareSteps(int i) {
        int dim = (int) Mathf.Pow(2, i + 1);
        int step = (size - 1) / dim;
        int steps = (size - 1) / step;

        for (int x = 0; x < steps; x++) {
            for (int y = x % 2 == 0 ? 1 : 0; y < steps; y += 2) {
                SquareStep(x * step, y * step, step);
            }
        }
    }

    private void SquareStep(int cx, int cy, int radius) {

        int s = size - 1;

        // When cx or cy = 0, we need to do 2 things
      
        // 1: Wrap one reference point around to other side, since it would be negative (+ s % s)
        float avg = (
            map[cx + radius, cy] + 
            map[(cx - radius + s) % s, cy] +
            map[cx, cy + radius] +
            map[cx, (cy - radius + s) % s]) / 4f;

        float value = Mathf.Clamp01(avg + (Random.value - .5f) * cVariation);

        map[cx, cy] = value;
    
        // 2: Dublicate value to opposite side
        // cx and cy cannot be 0 simultaneously
        if (cx == 0) {
            map[s, cy] = value;
        } else if (cy == 0) {
            map[cx, s] = value;
        }
    }
}

