using UnityEngine;

public class ConveyorBelt : GameManager
{ 
    private Vector3 direction = Vector3.right;
    private static ConveyorBelt instance;
    private void Start()
    {
        PlayerPrefs.SetFloat("ConveyorSpeed", 0.7f);
        Debug.Log("Conveyor Speed set to " + PlayerPrefs.GetFloat("ConveyorSpeed"));
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
                collider.transform.Translate(direction * PlayerPrefs.GetFloat("ConveyorSpeed") * Time.deltaTime);
            }
        }
    }
}
