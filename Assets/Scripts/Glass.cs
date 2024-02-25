using UnityEngine;

public class Glass : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkTrash(trigger.gameObject.tag, "Glass Bin");
    }
}
