using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    private float conveyorSpeed;
    private void Start()
    {
        PlayerPrefs.SetFloat("ConveyorSpeed", 0.7f);
        conveyorSpeed = PlayerPrefs.GetFloat("ConveyorSpeed");
        Debug.Log("Conveyor Speed set to " + PlayerPrefs.GetFloat("ConveyorSpeed"));
    }
    void Update()
    {
        //Move objects on top of the conveyor belt in the specified direction and speed.
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, Quaternion.identity, LayerMask.GetMask("MoveableObject"));
        foreach (Collider collider in colliders)
        {
            //Apply movement in world space.
            Vector3 movement = direction.normalized * conveyorSpeed * Time.deltaTime;
            collider.transform.position += movement;
        }
    }
}




