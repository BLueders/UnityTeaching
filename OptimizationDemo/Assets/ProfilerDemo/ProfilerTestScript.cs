using UnityEngine;
using System.Collections;
using UnityEngine.Profiling;

public class ProfilerTestScript : MonoBehaviour {

    public int countTo = 1000000;

    void Start() {
	
    }

    void Update() {

        Profiler.BeginSample("Profiler Test");
        int i = 0;
        while (i < countTo) {
            i++;
        }
        Profiler.EndSample();

    }
}
