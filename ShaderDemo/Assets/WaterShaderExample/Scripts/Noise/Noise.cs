using UnityEngine;
using System.Collections;

public abstract class Noise {

    protected int Size {
        get; set;
    }

    public Noise(int size) {
        Size = size;
    }

    public abstract float[,] Generate();

}