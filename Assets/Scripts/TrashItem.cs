using System;

[Serializable]
public class TrashItem
{
    public string itemName;
    public TrashType itemType;
    public bool isRecyclable;

    // Constructor to initialize a new TrashItem
    public TrashItem(string name, TrashType type, bool recyclable)
    {
        itemName = name;
        itemType = type;
        isRecyclable = recyclable;
    }
}

// Enum to define different types of trash
public enum TrashType
{
    AerosolCan,
    CardboardBox,
    GlassBottle,
    GlassJar,
    SodaCan,
    WaterBottle
    // Add more types as needed
}
