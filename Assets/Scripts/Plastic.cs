using UnityEngine;

public class Plastic : Trash
{
    public string plasticSubType;
    private void OnTriggerEnter(Collider trigger)
    {
        if (plasticSubType == "PH")
        {
            checkCollider(trigger, "Bin_Recycled");
        }
        if (plasticSubType == "PH" || plasticSubType == null)
        {
            checkCollider(trigger, "Bin_General");
        }
    }
}
