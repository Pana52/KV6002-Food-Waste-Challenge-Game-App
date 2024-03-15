using UnityEngine;

public class Plastic : Trash
{
    public string plasticSubType;
    private void OnTriggerEnter(Collider trigger)
    {
        if (plasticSubType == "Recyclable")
        {
            checkCollider(trigger, "Bin_Recycled");
        }
        if (plasticSubType == "Non-Recyclable" || plasticSubType == null)
        {
            checkCollider(trigger, "Bin_General");
        }
    }
}
