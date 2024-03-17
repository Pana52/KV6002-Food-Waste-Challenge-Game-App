using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeedX = 50f;
    public float rotateSpeedY = 50f;
    public float rotateSpeedZ = 50f;
    

    void Update()
    {
        //Rotate around the X axis.
        transform.Rotate(Vector3.right, rotateSpeedX * Time.deltaTime, Space.World);

        //Rotate around the Y axis.
        transform.Rotate(Vector3.up, rotateSpeedY * Time.deltaTime, Space.World);

        //Rotate around the Z axis.
        transform.Rotate(Vector3.forward, rotateSpeedZ * Time.deltaTime, Space.World);
    }
}