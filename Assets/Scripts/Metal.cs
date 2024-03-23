using UnityEngine;

public class Metal : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkCollider(trigger, "Bin_Recycled", "Metal");
    }
}

