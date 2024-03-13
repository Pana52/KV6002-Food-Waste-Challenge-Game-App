using UnityEngine;

public class Plastic : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkTrash(trigger.gameObject.tag, "Blue Bin");
    }
}
