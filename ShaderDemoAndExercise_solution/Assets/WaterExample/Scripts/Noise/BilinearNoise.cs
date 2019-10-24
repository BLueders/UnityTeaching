using UnityEngine;
using System.Collections;

public class BilinearNoise : InterpolatedNoise {

    public BilinearNoise(int size, float lattice) : base(size, lattice) {
    }

    protected override float Interpolate(float value) {
        return value;
    }
}

