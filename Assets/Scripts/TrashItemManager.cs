using System.Collections.Generic;
using UnityEngine;

public class TrashItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject aerosolCanPrefab;
    [SerializeField]
    private GameObject cardboardBoxPrefab;
    [SerializeField]
    private GameObject glassBottlePrefab;
    [SerializeField]
    private GameObject glassJarPrefab;
    [SerializeField]
    private GameObject sodaCanPrefab;
    [SerializeField]
    private GameObject waterBottlePrefab;

    // Dictionary to map TrashType to prefab
    private Dictionary<TrashType, GameObject> trashPrefabs;

    private void Awake()
    {
        // Initialize the dictionary
        trashPrefabs = new Dictionary<TrashType, GameObject>
        {
            { TrashType.AerosolCan, aerosolCanPrefab },
            { TrashType.CardboardBox, cardboardBoxPrefab },
            { TrashType.GlassBottle, glassBottlePrefab },
            { TrashType.GlassJar, glassJarPrefab },
            { TrashType.SodaCan, sodaCanPrefab },
            { TrashType.WaterBottle, waterBottlePrefab }
            // Add more mappings as needed
        };
    }

    public GameObject GetRandomTrashItemPrefab()
    {
        TrashItem randomTrashItem = GetRandomTrashItem();
        if (trashPrefabs.TryGetValue(randomTrashItem.itemType, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogError("Prefab not found for type: " + randomTrashItem.itemType);
            return null;
        }
    }
    // Example list of predefined trash items
    private List<TrashItem> availableTrashItems = new List<TrashItem>
    {
        new TrashItem("Aerosol Can", TrashType.AerosolCan, false),
        new TrashItem("Cardboard Box", TrashType.CardboardBox, true),
        new TrashItem("Glass Bottle", TrashType.GlassBottle, true),
        new TrashItem("Glass Jar", TrashType.GlassJar, true),
        new TrashItem("Soda Can", TrashType.SodaCan, true),
        new TrashItem("Water Bottle", TrashType.WaterBottle, true),
        // Add more predefined items here
    };

    // Method to get a random TrashItem
    public TrashItem GetRandomTrashItem()
    {
        int index = UnityEngine.Random.Range(0, availableTrashItems.Count);
        return availableTrashItems[index];
    }
}
