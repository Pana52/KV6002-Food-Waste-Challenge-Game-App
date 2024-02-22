using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialPosition; // Store the initial position when the object is clicked
    private Vector3 offset;

    void Update()
    {
        if (isDragging)
        {
            // Calculate the position of the object based on the mouse position
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Adjust this value based on your camera setup
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;

            // Update the position of the object
            transform.position = objPosition;
        }
    }

    void OnMouseDown()
    {
        // Store the initial position when the object is clicked
        initialPosition = transform.position;

        // Calculate the offset between the object's position and the mouse position
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check if the object is in a valid location
        if (!IsValidDropLocation())
        {
            // If the drop is invalid, revert the object to its initial position
            transform.position = initialPosition;
        }
    }

    bool IsValidDropLocation()
    {
        // Implement your logic here to check if the drop location is valid
        // For example, you can check if the drop location is within certain bounds or conditions
        // Return true if the drop location is valid, false otherwise
        return false; // Change this condition based on your game's requirements
    }
}
