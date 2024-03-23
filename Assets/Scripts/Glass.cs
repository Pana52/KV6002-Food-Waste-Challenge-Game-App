using UnityEngine;

public class Glass : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkCollider(trigger, "Bin_Glass", null);
    }
}
