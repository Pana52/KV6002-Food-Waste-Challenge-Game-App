using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragAndDrop : GameManager
{
    private Vector3 initialPosition; // Initial position when object is clicked.
    private Vector3 initialMousePosition; // Initial mouse position in world space. 
    private Collider coll;
    private float transitionTime = 0.5f;
    private Camera mainCamera;
    float mouseSpeed = 0.2f;

    // New: Keep track of previously hovered bins
    private List<Animator> previouslyHoveredBins = new List<Animator>();

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        coll = GetComponent<Collider>();
        mainCamera = Camera.main;
    }


    //[SerializeField] private TextMeshProUGUI objectInfoText;


    void OnMouseDrag()
    {
        if (!GetIsDragging()) return;
        { 

            Cursor.visible = false;
            Vector3 currentMousePosition = GetMouseWorldPos();
            Vector3 positionDelta = currentMousePosition - initialMousePosition;
            Vector3 newPosition = initialPosition + new Vector3(positionDelta.x * mouseSpeed, 0, positionDelta.y * mouseSpeed);
            transform.position = newPosition;

        // Check and update bin hover states
            CheckForBinBelow();


            ///---------------------Dash Display Text - Work in progress. 
            // Check if an object is currently being dragged
                                  /**  if (Input.GetMouseButton(0))
                                    {
                                        RaycastHit hit;
                                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                                        if (Physics.Raycast(ray, out hit))
                                        {
                                            // Get the dragged object
                                            GameObject draggedObject = hit.collider.gameObject;

                                            // Check if the dragged object has a Trash component
                                            Trash trashComponent = draggedObject.GetComponent<Trash>();
                                            if (trashComponent != null)
                                            {
                                                // Get the object's name, material, and description
                                                string trashName = trashComponent.trashName;
                                                string trashType = trashComponent.trashType; // Replace with actual material information
                                                string trashDesc = trashComponent.trashDesc; // Replace with actual description

                                                // Build the object information string
                                                string objectInfo = "Name: " + trashName + "\n" +
                                                                    "Material: " + trashType + "\n" +
                                                                    "Description: " + trashDesc;

                                                // Display the object information on the UI text element
                                                objectInfoText.text = objectInfo;
                                            }
                                            else
                                            {
                                                // Clear the UI text if no Trash component is found
                                                objectInfoText.text = "";
                                            }
                                        }
                                        else
                                        {
                                            // Clear the UI text if no object is hit by the raycast
                                            objectInfoText.text = "";
                                        }
                                    }
                                    else
                                    {
                                        // Clear the UI text if no mouse button is pressed
                                        objectInfoText.text = "";
                                    }
            **/
            //------------------------------------
        }
    }

    void OnMouseDown()
    {         
        initialPosition = transform.position;
        initialMousePosition = GetMouseWorldPos();
        SetIsDragging(true);
        coll.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        PlayItemPickupSound();
    }

    void PlayItemPickupSound()
    {
        // Log to confirm the method is called
        Debug.Log("PlayItemPickupSound called.");

        // Safely access the Trash component and its trashType
        Trash trashComponent = this.transform.GetComponent<Trash>();
        if (trashComponent != null)
        {
            string trashName = trashComponent.trashName;
            // Log the trashType to see what we are working with
            Debug.Log($"Attempting to play sound for trash type: {trashName}");

            switch (trashName)
            {
                case "Newspaper":
                    audioManager.PlaySFX(audioManager.newspaper);
                    break;
                case "Milk Bottle":
                    audioManager.PlaySFX(audioManager.milkBottle);
                    break;
                default:
                    Debug.Log("No sound for this item. Trash type: " + trashName);
                    break;
            }
        }
        else
        {
            Debug.LogError("Trash component not found on the object.");
        }
    }


    void OnMouseUp()
    {
        Cursor.visible = true;
        SetIsDragging(false);
        coll.enabled = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Reset all bins' hover state when dragging stops
        ResetHoverState();

        if (!IsValidDropLocation())
        {
            StartCoroutine(FloatBackToInitialPosition());
        }
    }

    void CheckForBinBelow()
    {
        float radius = 0.1f; // Adjust as needed
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        HashSet<Animator> currentlyHoveredBins = new HashSet<Animator>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Bin"))
            {
                Animator binAnimator = hitCollider.GetComponent<Animator>();
                if (binAnimator != null)
                {
                    binAnimator.SetBool("isHover", true);
                    currentlyHoveredBins.Add(binAnimator);
                }
            }
        }

        // Reset bins that are no longer hovered over
        foreach (var prevBin in previouslyHoveredBins)
        {
            if (!currentlyHoveredBins.Contains(prevBin))
            {
                prevBin.SetBool("isHover", false);
                audioManager.PlaySFX(audioManager.binClose);
            }
        }

        previouslyHoveredBins = new List<Animator>(currentlyHoveredBins);
    }

    // New method to reset the hover state of all previously hovered bins when needed
    void ResetHoverState()
    {
        foreach (var binAnimator in previouslyHoveredBins)
        {
            binAnimator.SetBool("isHover", false);
        }
        previouslyHoveredBins.Clear();
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(initialPosition).z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    bool IsValidDropLocation()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if ( hit.collider.CompareTag("Bin"))
            {
                return true;         
            }
        }
        return false;
    }

    IEnumerator FloatBackToInitialPosition()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < transitionTime)
        {
            transform.position = Vector3.Lerp(startPosition, initialPosition, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
    }
}
