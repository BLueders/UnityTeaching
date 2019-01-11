using UnityEngine;
using System.Collections;

public class CubemapGenerator : MonoBehaviour {

    public Vector3 position;
    public Cubemap cubemap;

    public void RenderCubemap() {
        // create temporary camera for rendering
        GameObject go = new GameObject ("CubemapCamera");
        go.AddComponent<Camera>();
        // place it on the object
        go.transform.position = position;
        go.transform.rotation = Quaternion.identity;
        // render into cubemap      
        go.GetComponent<Camera>().RenderToCubemap(cubemap);

        // destroy temporary camera
        DestroyImmediate(go);
    }
}
