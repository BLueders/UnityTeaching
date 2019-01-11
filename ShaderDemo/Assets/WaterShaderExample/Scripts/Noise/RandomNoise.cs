using UnityEngine;
using System.Collections;

public class RandomNoise : Noise {

    public RandomNoise(int size) : base(size) {}

    public override float[,] Generate() {
        
        float[,] values = new float[Size, Size];

        for (int x = 0; x < Size; x++) {
            for (int y = 0; y < Size; y++) {
                values[x, y] = Random.value;
            }
        }

        return values;
    }
}

