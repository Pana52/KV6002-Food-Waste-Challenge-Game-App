using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{ 
    public float speed = 1.0f;
    public Vector3 direction = Vector3.right;

    void Update()
    {
        //Move objects on top of the conveyor belt in the specified direction and speed.
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("MoveableObject"))
            {
                //Debug.Log("MoveableObject detected: " + collider.name);
                collider.transform.Translate(direction * speed * Time.deltaTime);
            }
        }
    }
}
