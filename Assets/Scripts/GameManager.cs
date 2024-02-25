
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] trash;
    private bool isDragging;
    public bool GetIsDragging()
    {
        return isDragging;
    }
    public void SetIsDragging(bool value)
    {
        isDragging = value;
        Debug.Log("isDragging bool value changed.");
    }
    void Update()
    {
        //For testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generateTrash();
        }
    }

    public void generateTrash()
    {
        int randomIndex = Random.Range(0, trash.Length);
        Vector3 spawnLocation = new Vector3(-13, 5, 10);

        Instantiate(trash[randomIndex], spawnLocation, Quaternion.identity);
    }

    public void correctBin()
    {
        Debug.Log("Trash placed in the correct Bin - Object Destroyed.");
        Destroy(gameObject);
        SetIsDragging(false);
    }
    public void wrongBin()
    {
        Debug.Log("Trash placed in the incorrect bin - Object Destroyed.");
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
