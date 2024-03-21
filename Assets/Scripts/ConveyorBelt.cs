using System.Collections;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    private bool isOperating = true;
    private void Start()
    {
        PlayerPrefs.SetFloat("ConveyorSpeed", 0.7f);
    }
    void Update()
    {
        //Only move objects if the conveyor is operating.
        if (isOperating)
        {
            MoveObjectsOnBelt();
        }
        //For Testing.
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleConveyor();
        }
    }
    void MoveObjectsOnBelt()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, Quaternion.identity, LayerMask.GetMask("MoveableObject"));
        foreach (Collider collider in colliders)
        {
            Vector3 movement = direction.normalized * PlayerPrefs.GetFloat("ConveyorSpeed") * Time.deltaTime;
            collider.transform.position += movement;
        }
    }

    
    public void ToggleConveyor() //Stop conveyor movement.
    {
        if (isOperating)
        {
            StartCoroutine(StopAndRestartConveyor());
        }
    }

    IEnumerator StopAndRestartConveyor()
    {
        isOperating = false; //Stop the conveyor.
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        isOperating = true; //Restart the conveyor.
    }


}




