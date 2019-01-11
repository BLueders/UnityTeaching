using UnityEngine;
using System.Collections;

public class BicubicNoise : InterpolatedNoise {

    public BicubicNoise(int size, float lattice) : base(size, lattice) {
        
    }

    protected override float Interpolate(float value) {
        return -2f * Mathf.Pow(value, 3) + 3 * Mathf.Pow(value, 2);
    }
}

