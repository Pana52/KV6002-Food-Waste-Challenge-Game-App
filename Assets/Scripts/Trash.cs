using UnityEngine;

public class Trash : GameManager
{
    public string trashName;
    public string trashType;
    public string trashDesc;

    public void checkCollider(Collider trigger, string bin)
    { 
        checkTrash(trigger.gameObject.name, bin);     
    }
}
