using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : GameManager
{
    private Vector3 initialPosition; // Initial position when object is clicked.
    private Vector3 initialMousePosition; // Initial mouse position in world space. 
    private Collider coll;
    private float transitionTime = 0.5f;
    private Camera mainCamera;
    float mouseSpeed = 1.5f;
    private TextMeshProUGUI objectInfoText;

    private bool hoverSoundPlayed = false;


    private GameObject itemCopy = null;
    private static Transform copyLocation;

    // New: Keep track of previously hovered bins
    private List<Animator> previouslyHoveredBins = new List<Animator>();

    AudioManager audioManager;

    private void Awake()
    {

        if (copyLocation == null) //Find copy location.
        {
            GameObject locationMarker = GameObject.FindGameObjectWithTag("CopyLocation");
            if (locationMarker != null)
            {
                copyLocation = locationMarker.transform;
            }
            else
            {
                Debug.LogError("Copy location marker not found. Ensure it's tagged correctly.");
            }
        }

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        objectInfoText = TrashManager.Instance.GetObjectInfoText();
    }

    private void Start()
    {
        coll = GetComponent<Collider>();
        mainCamera = Camera.main;
    }

    void OnMouseDrag()
    {
        if (!GetIsDragging()) return;
        {

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            Vector3 currentMousePosition = GetMouseWorldPos();
            Vector3 positionDelta = currentMousePosition - initialMousePosition;
            Vector3 newPosition = initialPosition + new Vector3(positionDelta.x * mouseSpeed, 0, positionDelta.y * mouseSpeed);
            transform.position = newPosition;

            // Check and update bin hover states
            CheckForBinBelow();
        }
    }

    void OnMouseDown()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        initialPosition = transform.position;
        initialMousePosition = GetMouseWorldPos();
        SetIsDragging(true);
        coll.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (objectInfoText != null)
        {
            UpdateUIWithTrashInfo();
        }
        else
        {
            Debug.LogError("objectInfoText not assigned in OnMouseDown");
        }
        PlayItemPickupSound();


        if (itemCopy == null && copyLocation != null)
        {
            itemCopy = Instantiate(gameObject, copyLocation.position, Quaternion.identity, copyLocation);
            float scalerObj = 250f;
            DisableComponentsForDisplay(itemCopy, scalerObj);
        }


    }

    void PlayItemPickupSound()
    {

        // Safely access the Trash component and its trashType
        Trash trashComponent = this.transform.GetComponent<Trash>();
        if (trashComponent != null)
        {
            string trashName = trashComponent.trashName;

            switch (trashName)
            {
                case "Cereal Box":
                    audioManager.PlaySFX(audioManager.cerealBox, 0.75f);
                    break;
                case "Food Can":
                    audioManager.PlaySFX(audioManager.foodCan, 0.55f);
                    break;
                case "Magazine":
                case "Newspaper":
                    audioManager.PlaySFX(audioManager.paperFlip, 0.95f);
                    break;
                case "Milk Bottle":
                case "Jam Jar":
                case "Glass Bottle":
                case "Bottle":
                    audioManager.PlaySFX(audioManager.glassBottle, 0.90f);
                    break;
                case "Milk Carton":
                case "Coffee Carton":
                case "Cup":
                    audioManager.PlaySFX(audioManager.carton, 0.75f); // Maybe change sound
                    break;
                case "Aluminium Tray":
                    audioManager.PlaySFX(audioManager.foilTray, 0.75f);
                    break;
                case "Pizza Box":
                    audioManager.PlaySFX(audioManager.cardboardBox, 0.75f); // Maybe change sound
                    break;
                case "Plastic Bottle":
                case "Plastic Cup":
                case "Yoghurt Tub":
                    audioManager.PlaySFX(audioManager.plastic, 0.75f); // Maybe change sound
                    break;
                case "Plastic Straw":
                    audioManager.PlaySFX(audioManager.straw, 0.75f); // Change sound
                    break;
                case "Toy Soldier":
                    audioManager.PlaySFX(audioManager.toy, 0.75f); // Change sound
                    break;
                case "Take-Out Container":
                    audioManager.PlaySFX(audioManager.polystyrene, 0.85f);
                    break;
                case "Aerosol Can":
                    audioManager.PlaySFX(audioManager.aerosol, 0.65f);
                    break;
                case "AA Battery":
                    audioManager.PlaySFX(audioManager.battery, 0.85f);
                    break;
                case "Cigarette":
                    audioManager.PlaySFX(audioManager.cigarette, 0.75f);
                    break;
                case "Crisp Packet":
                case "Carrier Bag":
                case "Plastic Wrap":
                    audioManager.PlaySFX(audioManager.plasticBag, 1.20f);  // Change sound
                    break;
                case "Soda Can":
                    audioManager.PlaySFX(audioManager.canOpen, 0.55f);
                    break;
                case "Light Bulb":
                    audioManager.PlaySFX(audioManager.lightBulb, 0.85f);
                    break;
                case "Cigarette Lighter":
                    audioManager.PlaySFX(audioManager.lighter, 0.85f);
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
        Cursor.lockState = CursorLockMode.Confined;
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

        if (objectInfoText != null)
        {
            ClearUI();
        }
        else
        {
            Debug.LogError("objectInfoText not assigned in OnMouseUp");
        }
        if (itemCopy != null)
        {
            Destroy(itemCopy);
            itemCopy = null;
        }
    }

    void CheckForBinBelow()
    {
        float radius = 0.1f; // Adjust as needed
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        HashSet<Animator> currentlyHoveredBins = new HashSet<Animator>();
        bool isCurrentlyHoveringOverBin = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Bin"))
            {
                isCurrentlyHoveringOverBin = true;
                Animator binAnimator = hitCollider.GetComponent<Animator>();
                if (binAnimator != null)
                {
                    binAnimator.SetBool("isHover", true);
                    currentlyHoveredBins.Add(binAnimator);
                    if(!hoverSoundPlayed)
                    {
                        audioManager.PlaySFX(audioManager.binOpen, 0.70f);
                        hoverSoundPlayed = true;
                    }
                }
            }
        }

        if(!isCurrentlyHoveringOverBin)
        {
            hoverSoundPlayed = false;
        }

        // Reset bins that are no longer hovered over
        foreach (var prevBin in previouslyHoveredBins)
        {
            if (!currentlyHoveredBins.Contains(prevBin))
            {
                prevBin.SetBool("isHover", false);
                audioManager.PlaySFX(audioManager.binClose, 0.65f);
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
        // Dynamically adjust the Z based on the object's distance from the camera
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
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

    void UpdateUIWithTrashInfo()
    {
        Trash trashComponent = GetComponent<Trash>();
        if (TrashManager.Instance != null && TrashManager.Instance.objectInfoText != null)
        {
            string objectInfo = $"{trashComponent.trashName}\n{trashComponent.trashDesc}";
            //Update the UI element.
            TrashManager.Instance.objectInfoText.text = objectInfo;

            ApplySpriteToSymbol(TrashManager.Instance.symbol_01, trashComponent.isRecycleSymbol);
            ApplySpriteToSymbol(TrashManager.Instance.symbol_02, trashComponent.recyclingSymbol);
            
        }
        else
        {
            Debug.LogError("TrashManager instance or objectInfoText is not set");
        }
    }

    void ApplySpriteToSymbol(GameObject symbol, Sprite sprite)
    {
        // Assuming the GameObject has an Image component
        Image image = symbol.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprite;
        }
        else
        {
            Debug.LogError("Image component not found on the symbol GameObject.");
        }
    }

    void ClearUI()
    {
        TrashManager.Instance.symbol_01.GetComponent<Image>().sprite = null;
        TrashManager.Instance.symbol_02.GetComponent<Image>().sprite = null;

        if (objectInfoText == null)
        {
            Debug.LogError("objectInfoText not assigned in ClearUI");
            return;
        }
        objectInfoText.text = "";

    }

    private void DisableComponentsForDisplay(GameObject obj, float scaleMultiplier = 10f) // Added a scaleMultiplier parameter with a default value
    {
        // Disable Rigidbody.
        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Add and configure RotateObject script.
        var rotateScript = obj.AddComponent<RotateObject>();
        rotateScript.rotateSpeedX = 0f;
        rotateScript.rotateSpeedY = 15f;
        rotateScript.rotateSpeedZ = 10f;

        // Rescale the object.
        obj.transform.localScale *= scaleMultiplier;
    }


}
