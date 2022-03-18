using UnityEngine;

public enum ResourceType
{
    WOOD_LOG,
    STONE_BLOCK,
    MUD_BLOCK,
    MUD_BRICK,
    CEMENT_BLOCK,
    CEMENT_SACK,
    IRON_BLOCK,
    STEEL_ROD
}
public enum ResourceStructure
{
    CHIMNEY,
    CEMENT_MACHINE,
    IRON_MACHINE
}

public enum DisasterType
{
    EARTHQUAKE,
    STORM,
    VOLCANO,
    TSUNAMI,
    TORNADO
}

[System.Serializable]
public class ResourceAmount
{
    public string name;
    public int value;
}

public class DataHolder : MonoBehaviour
{
   
}
