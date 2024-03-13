using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    // You can assign this through the Unity Inspector
    public GameObject targetGameObject;
    public void buttonClick()
    {
        // Check if the targetGameObject is not null to avoid errors
        if (targetGameObject != null)
        {
            // Toggle the active state
            targetGameObject.SetActive(!targetGameObject.activeSelf);
        }
    }
}
