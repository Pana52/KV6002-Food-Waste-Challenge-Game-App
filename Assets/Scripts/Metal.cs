using UnityEngine;

public class Metal : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkTrash(trigger.gameObject.tag, "Metal Bin");
    }
}

