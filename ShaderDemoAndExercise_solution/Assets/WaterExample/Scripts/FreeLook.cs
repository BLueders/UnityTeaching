using UnityEngine;
using System.Collections;

public class FreeLook : MonoBehaviour {

    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2

    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    public float velocity = 5f;

    void Update() {

        if (Input.GetMouseButton(1)) {
            if (axes == RotationAxes.MouseXAndY) {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
            } else if (axes == RotationAxes.MouseX) {
                transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
            } else {
                rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3 (-rotationY, transform.localEulerAngles.y, 0);
            }
        }

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction.z++;
        if (Input.GetKey(KeyCode.S))
            direction.z--;
        if (Input.GetKey(KeyCode.D))
            direction.x++;
        if (Input.GetKey(KeyCode.A))
            direction.x--;

        direction = direction.normalized;

        float v = velocity;

        if (Input.GetKey(KeyCode.LeftShift)) {
            v *= 2f;
        }

        if (direction != Vector3.zero) {
            transform.Translate(direction * v * Time.deltaTime);
        }
    }

    void Start() {

    }
}

