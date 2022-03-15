using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = ("Level Data"))]
public class LevelData : ScriptableObject
{
    [Range(0, 2)]
    public int environmentNumber = 0;
    public bool ShowTutorial = false;

    [Header("Available Resources")]
    public ResourceType[] hasResources;


    public enum Weather { 
    
        SUNNY,
        RAINY,
        SNOWY
    }

    [Space(30)]
    public Weather weather = Weather.SUNNY;

    public bool isNight = false;
    public bool hasExtras = false;

    [SerializeField] public AudioClip bgSound;

    [Space(20)]
    private EnvProfile envProfile;
}
