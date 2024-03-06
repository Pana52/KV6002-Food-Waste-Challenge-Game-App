
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float conveyorSpeed;
    private bool isDragging;
    public bool GetIsDragging()
    {
        return isDragging;
    }
    public void SetIsDragging(bool value)
    {
        isDragging = value;
        //Debug.Log("isDragging bool value changed.");
    }
    public float GetConveyorSpeed()
    {
        return conveyorSpeed;
    }
    public void SetConveyorSpeed (float value)
    {
        conveyorSpeed = value;
    }

    public void correctBin()
    {
        Debug.Log("CORRECT");
        Destroy(gameObject);
        SetIsDragging(false);
    }
    public void wrongBin()
    {
        Debug.Log("INCORRECT");
        Destroy(gameObject);
        SetIsDragging(false);
    }
    public void generalWaste()
    {
        Debug.Log("Trash item added to general waste.");
        Destroy(gameObject);
    }
    public void checkTrash(string binType, string correctBinType)
    {
        
            if (binType == correctBinType)
            {
                correctBin();
            }
            else if (binType == "Incinerator")
            {
            generalWaste();
            }
            else
            {
                wrongBin();
            }
        
    }
}
