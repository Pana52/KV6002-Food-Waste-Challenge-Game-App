using UnityEngine;

public class TrashManager : MonoBehaviour
{
    // Singleton instance
    private static TrashManager instance;
    private GameObject[] trash;

    //Ensure only one instance of TrashManager exists.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameObject[] trashItems = Resources.LoadAll<GameObject>("Prefabs/Trash_Items");
        trash = new GameObject[trashItems.Length];
        for (int i = 0; i < trashItems.Length; i++)
        {
            trash[i] = trashItems[i];
        }
    }
    private void Update()
    {
        //For testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generateTrash();
        }
    }
    public void generateTrash()
    {
        Debug.Log("Trash Generated");
        int randomIndex = Random.Range(0, trash.Length);
        Vector3 spawnLocation = new Vector3(-13, 2, 10);
        Instantiate(trash[randomIndex], spawnLocation, Quaternion.identity);
    }
}
