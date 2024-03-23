using UnityEngine;

public class NotRecycleable : Trash
{   
    private void OnTriggerEnter(Collider trigger)
    {
        checkCollider(trigger, "Bin_General", null);
    }
}
