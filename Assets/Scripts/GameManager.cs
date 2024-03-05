using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TrashItemManager trashItemManager;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateRandomTrashItem();
        }
    }

    public void GenerateRandomTrashItem()
    {
        GameObject prefab = trashItemManager.GetRandomTrashItemPrefab();
        if (prefab != null)
        {
            // Instantiate the prefab at a desired position and rotation
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
