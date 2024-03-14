using UnityEngine;

public class Paper : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkCollider(trigger, "Bin_Paper");
    }
}
