
using UnityEngine;

public class GameManager : MonoBehaviour
{

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
    public void incinerate()
    {
        Debug.Log("Trash item incinerated.");
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
                incinerate();
            }
            else
            {
                wrongBin();
            }
        
    }
}
