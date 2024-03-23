using UnityEngine;

public class Trash : GameManager
{
    public string trashName;
    public string trashType;
    public string trashDesc;

    public Sprite isRecycleSymbol;
    public Sprite recyclingSymbol;

    public void checkCollider(Collider trigger, string bin, string subType) //Check which bin. 
    { 
        checkTrash(trigger.gameObject.name, bin, subType);
    }
}
