using UnityEngine;

public class Paper : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkTrash(trigger.gameObject.tag, "Paper Bin");  
    }
}
