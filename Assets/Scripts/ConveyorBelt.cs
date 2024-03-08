using UnityEngine;

public class ConveyorBelt : GameManager
{ 
    public Vector3 direction = Vector3.right;
    private void Start()
    {
        SetConveyorSpeed(1f);
    }
    void Update()
    {
        //Move objects on top of the conveyor belt in the specified direction and speed.
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("MoveableObject"))
            {
                //Debug.Log("MoveableObject detected: " + collider.name);
                collider.transform.Translate(direction * GetConveyorSpeed() * Time.deltaTime);
            }
        }
    }
}
