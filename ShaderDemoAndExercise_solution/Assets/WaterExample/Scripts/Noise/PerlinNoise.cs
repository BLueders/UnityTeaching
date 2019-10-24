using UnityEngine;
using System.Collections;

public class PerlinNoise : Noise {

    private int lSize;
    private int res;
    private float lattice;
    private Vector2[,] lattices;

    public PerlinNoise(int size, int res, float lattice) : base(size) {
        this.lattice = lattice;
        this.res = res;
        lSize = (int) (res * lattice);
    }

    public override float[,] Generate() {

        float[,] values = new float[Size, Size];

        GenerateLattices();

        for (int x = 0; x < res; x++) {
            for (int y = 0; y < res; y++) {

                float nx = (float) (x) / (float) res;
                float ny = (float) (y) / (float) res;
            
                values[x, y] = Query(nx, ny);
            }
        }

        return values;
    }

    private float Query(float x, float y) {

        int x0 = (int) (x * (lSize - 1));
        int y0 = (int) (y * (lSize - 1));

        Vector2 gnw = lattices[x0, y0];
        Vector2 gne = lattices[x0 + 1, y0];
        Vector2 gsw = lattices[x0, y0 + 1];
        Vector2 gse = lattices[x0 + 1, y0 + 1];

        float fx0 = (float) x0 / (float) (lSize - 1);
        float fx1 = (float) (x0 + 1) / (float) (lSize - 1);
        float fy0 = (float) y0 / (float) (lSize - 1);
        float fy1 = (float) (y0 + 1) / (float) (lSize - 1);

        float xn = (x - fx0) / (fx1 - fx0);
        float yn = (y - fy0) / (fy1 - fy0);

        Vector2 p = new Vector2(xn, yn);

        Vector2 vnw = new Vector2(0f, 0f) - p;
        Vector2 vne = new Vector2(1f, 0f) - p;
        Vector2 vsw = new Vector2(0f, 1f) - p;
        Vector2 vse = new Vector2(1f, 1f) - p;

        float dnw = Vector2.Dot(gnw, vnw);
        float dne = Vector2.Dot(gne, vne);
        float dsw = Vector2.Dot(gsw, vsw);
        float dse = Vector2.Dot(gse, vse);

        float xInterp1 = dnw * (1f - xn) + dne * xn;
        float xInterp2 = dsw * (1f - xn) + dse * xn;

        float yInterp = xInterp1 * (1f - yn) + xInterp2 * yn;

        return yInterp;
    }

    private void GenerateLattices() {
        lattices = new Vector2[lSize, lSize];

        for (int x = 0; x < lSize; x++) {
            for (int y = 0; y < lSize; y++) {
                lattices[x, y] = new Vector2(Random.value, Random.value).normalized;
            }
        }
    }
}

