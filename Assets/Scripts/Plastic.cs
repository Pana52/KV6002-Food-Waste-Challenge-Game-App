using UnityEngine;

public class Plastic : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkCollider(trigger, "Bin_Recycled");
    }
}
