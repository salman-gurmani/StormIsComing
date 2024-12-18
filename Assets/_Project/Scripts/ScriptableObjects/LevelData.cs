﻿using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = ("Level Data"))]
public class LevelData : ScriptableObject
{
    [Range(0, 4)]
    public int environmentNumber = 0;
    public bool ShowTutorial = false;

    public float time = 60;
    public string LevelTxt;
    public ResourceType[] hasResources;
    public DisasterType disaster;
    public bool isBonus;
    public int noOfBonusThings;

    public enum Weather { 
    
        SUNNY,
        RAINY,
        SNOWY
    }

    [Space(30)]
    public Weather weather = Weather.SUNNY;

    public bool isNight = false;
    public bool hasExtras = false;
    public bool truckOn = false;
    [SerializeField] public AudioClip bgSound;

    [Space(20)]
    private EnvProfile envProfile;
    public string levelInfo;
}
