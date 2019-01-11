using UnityEngine;
using System.Collections;

public abstract class InterpolatedNoise : Noise {

    private int hStep, vStep;

    public InterpolatedNoise(int size, float lattice) : base(size) {
        hStep = (int) Mathf.Round((float) size * lattice);
        vStep = (int) Mathf.Round((float) size * lattice);

        if (hStep < 1) hStep = 1;
        if (vStep < 1) vStep = 1;
    }

    protected abstract float Interpolate(float value);

    public override float[,] Generate() {

        float[,] values = GenerateLattices();

        for (int x = 0; x < Size; x++) {
            int hStepProg = x % hStep;

            int x1 = x - hStepProg;
            int x2 = x1 + hStep;
            if (x2 >= Size) x2 = Size - 1;

            float hProg = (float) hStepProg / (float) hStep;
            hProg = Interpolate(hProg);

            for (int y = 0; y < Size; y++) {
                int vStepProg = y % vStep;

                // point is lattice
                if (hStepProg == 0 && vStepProg == 0) continue;

                int y1 = y - vStepProg;
                int y2 = y1 + vStep;
                if (y2 >= Size) y2 = Size - 1;

                float vProg = (float) vStepProg / (float) vStep;
                vProg = Interpolate(vProg);

                float xy1 = hProg;
                float xInterp1 = values[x1, y1] * (1f - hProg) + values[x2, y1] * hProg;
                float xInterp2 = values[x1, y2] * (1f - hProg) + values[x2, y2] * hProg;

                float yInterp = xInterp1 * (1f - vProg) + xInterp2 * vProg;

                values[x, y] = yInterp;
            }
        }

        return values;
    }

    private float[,] GenerateLattices() {

        float[,] values = new float[Size, Size];

        int x = 0; 

        while (x < Size) {
            int y = 0;

            if (x >= Size) x = Size - 1;

            while (y < Size) {
                if (y >= Size) y = Size - 1;

                values[x, y] = Random.value;

                if (y == Size - 1) break;

                y += vStep;

            }

            if (x == Size - 1) break;
           
            x += hStep;
            
        }

        return values;
    }
}

