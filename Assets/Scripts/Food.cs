using UnityEngine;

public class Food : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkCollider(trigger, "Bin_General", null);
    }
}
