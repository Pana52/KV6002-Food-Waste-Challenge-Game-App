using UnityEngine;

public class Hazardous : Trash
{
    private void OnTriggerEnter(Collider trigger)
    {
        checkCollider(trigger, "Bin_Hazard", null);
    }
}
