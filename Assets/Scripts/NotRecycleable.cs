using UnityEngine;

public class NotRecycleable : Trash
{   
    private void OnTriggerEnter(Collider trigger)
    {
        checkTrash(trigger.gameObject.tag, "General Waste");
    }
}
