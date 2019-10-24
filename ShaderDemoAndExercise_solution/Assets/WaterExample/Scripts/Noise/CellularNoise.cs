using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class CellularNoise : Noise {

    public enum NeighbourhoodType { VonNeumann, Moore }
 
    private NeighbourhoodType neighbourhoodType;
    private int iterations, neighbourhoodSize;
    private float limit, variance, decay;
    private float cVariance;

    public CellularNoise(int size, NeighbourhoodType type, int iterations, int neighborhoodSize, float limit, float variance, float decay) : base(size) {
        this.neighbourhoodType = type;
        this.iterations = iterations;
        this.neighbourhoodSize = neighborhoodSize;
        this.limit = limit;
        this.variance = variance;
        this.decay = decay;
    }

    public override float[,] Generate() {

        float[,] values = new float[Size, Size];

        for (int x = 0; x < Size; x++) {
            for (int y = 0; y < Size; y++) {
                values[x, y] = Random.value;
            }
        }

        for (int i = 0; i < iterations; i++) {
            cVariance = variance * Mathf.Pow(1f - Mathf.Clamp01(decay), i);
            values = Iterate(values);
        }

        return values;
    }

    private float[,] Iterate(float[,] values) {

        float[,] target = new float[Size, Size];

        for (int x = 0; x < Size; x++) {
            for (int y = 0; y < Size; y++) {

                float avg = 0f;
                switch (neighbourhoodType) {
                    case NeighbourhoodType.VonNeumann:
                        avg = VonNeumann(values, neighbourhoodSize, x, y);
                        break;

                    case NeighbourhoodType.Moore:
                        avg = Moore(values, neighbourhoodSize, x, y);
                        break;

                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            
                float value = values[x, y] + cVariance * (avg >= limit ? 1f : -1f);
                value = Mathf.Clamp01(value);
                target[x, y] = value;
            }
        }

        return target;
    }

    private float VonNeumann(float[,] values, int size, int x, int y) {
        float total = 0f;
        int count = 0;

        for (int xn = -size; xn <= size; xn++) {

            int y0 = -size + Mathf.Abs(xn);
            for (int yn = y0; yn <= -y0; yn++) {
                if (xn == 0 && yn == 0) continue;

                total += values[(x + xn + Size) % Size, (y + yn + Size) % Size];
                count++;    
            }
        }

        return total / (float) count;
    }

    private float Moore(float[,] values, int size, int x, int y) {
        float total = 0f;
        int count = 0;

        for (int xn = -size; xn <= size; xn++) {
            for (int yn = -size; yn <= size; yn++) {
                if (xn == 0 && yn == 0) continue;

                total += values[(x + xn + Size) % Size, (y + yn + Size) % Size];
                count++;    
            }
        }

        return total / (float) count;
    }
}
