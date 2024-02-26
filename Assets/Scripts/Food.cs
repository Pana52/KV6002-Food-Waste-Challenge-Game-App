using UnityEngine;

public class Food : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkTrash(trigger.gameObject.tag, "Food Bin");
    }
}
