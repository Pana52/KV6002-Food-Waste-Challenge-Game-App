using UnityEngine;

public class Hazardous : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkTrash(trigger.gameObject.tag, "Hazardous Bin");
    }
}
