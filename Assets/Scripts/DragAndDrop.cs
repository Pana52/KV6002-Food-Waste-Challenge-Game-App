using System.Collections;
using UnityEngine;

public class DragAndDrop : GameManager
{    
    private Vector3 initialPosition; //Initial position when object is clicked.
    private Vector3 offset;
    private Collider coll;
    public float transitionTime = 0.5f;
    private Camera mainCamera;


    // Define threshold for initial y-axis movement
    public float yToZThreshold = 0.5f;
    // Define threshold for y-axis movement
    public float yThreshold = 0.1f;
    // Define threshold for x-axis movement
    public float xThreshold = 0.1f;
    // Define z-axis movement factor
    public float zMovementFactor = 0.1f;

    private Vector3 initialMousePosition;
    private void Start()
    {
        coll = GetComponent<Collider>();
        mainCamera = Camera.main;
        

    }
    void Update()
        
    {
        if (GetIsDragging())
        {/**
            //Calculate position of object based on mouse position.
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 1.25F; //Adjust based on camera setup.
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;

            //Update the position of the object
            transform.position = objPosition;
        }**/
        }
    }

    void OnMouseDrag()
    {
        if (!GetIsDragging()) return;
        
        //Calculate current mouse position in world space.
        Vector3 currentMousePosition = GetMouseWorldPos();
        //Calculate x and y-axis movement.
        float xMovement = currentMousePosition.x - initialMousePosition.x;
        float yMovement = currentMousePosition.y - initialMousePosition.y;

        //Check if object is moving upwards and has reached the yToZThreshold.
        if (yMovement > 0 && Mathf.Abs(yMovement) > yToZThreshold)
        {
            // Calculate z-axis movement based on y-axis movement
            float zMovement = (Mathf.Abs(yMovement) - yToZThreshold) * zMovementFactor;
            // Update object's position (including z-axis movement towards the camera)
            transform.position = initialPosition + new Vector3(xMovement/6, Mathf.Clamp(yMovement, -yThreshold, yThreshold), zMovement*2);
        }
        else
        {
            //Update object's position (without z-axis movement).
            transform.position = initialPosition + new Vector3(xMovement/6, Mathf.Clamp(yMovement, -yThreshold, yThreshold), 0);
        }
    }
        



        void OnMouseDown()
    {
        //Store initial position when object is clicked.
        initialPosition = transform.position;
        initialMousePosition = GetMouseWorldPos();

        //Calculate offset between object's position and mouse position.
        //Debug.Log("Is dragging object.");
        //offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        offset = transform.position - GetMouseWorldPos();

        SetIsDragging(true);
        coll.enabled = false;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z; // Adjust z position based on camera's z position

        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    void OnMouseUp()
    {
        SetIsDragging(false);
        //Debug.Log("Dropped object.");
        coll.enabled = true;
        //Check if object is in valid location.
        if (!IsValidDropLocation())
        {
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
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            transform.position = Vector3.Lerp(startPosition, initialPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //Ensure object snaps back to initial position.
        transform.position = initialPosition;
    }
}

