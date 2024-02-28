using System.Collections;
using UnityEngine;

public class DragAndDrop : GameManager
{    
    private Vector3 initialPosition; //Initial position when object is clicked.
    private Vector3 offset;
    private Collider coll;
    public float floatBackTime = 0.5f;
    private void Start()
    {
        coll = GetComponent<Collider>();
    }
    void Update()
        
    {
        if (GetIsDragging())
        {
            //Calculate position of object based on mouse position.
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5; //Adjust based on camera setup.
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;

            //Update the position of the object
            transform.position = objPosition;
        }
    }

    void OnMouseDown()
    {
        
        //Store initial position when object is clicked.
        initialPosition = transform.position;

        //Calculate offset between object's position and mouse position.
        //Debug.Log("Is dragging object.");
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetIsDragging(true);
        coll.enabled = false;
    }

    void OnMouseUp()
    {

        SetIsDragging(false);
        //Debug.Log("Dropped object.");
        coll.enabled = true;
        //Check if object is in valid location.
        if (!IsValidDropLocation())
        {
            //If drop is invalid, return the object to its initial position.
            //transform.position = initialPosition;
            StartCoroutine(FloatBackToInitialPosition());
        }
    }

    bool IsValidDropLocation()
    {
       RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //Check if the ray hits a collider.
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Paper Bin") || hit.collider.CompareTag("Plastic Bin") || hit.collider.CompareTag("Glass Bin"))
                {
                    //Return true for valid drop location.
                    return true;
                }
            }
        }  
        //If no collider is hit, the drop location is invalid.
        return false;
    }

    IEnumerator FloatBackToInitialPosition()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        //Move object back to  initial position over time.
        while (elapsedTime < floatBackTime)
        {
            float t = elapsedTime / floatBackTime;
            transform.position = Vector3.Lerp(startPosition, initialPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Ensure object snaps back to initial position.
        transform.position = initialPosition;
    }
}

