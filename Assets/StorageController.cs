using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageController : MonoBehaviour
{
    public GameObject travelpnl;
    public int[] checkToTransfer;
    public bool[] checkToTransferBool;
    public int[] temp;
    public int capacity = 20;
    public int[] resources;
    public int[] resourcesInStorage;
    public Text[] resourcesTexts;
    public Text[] resourcesTextsInStorage;
    public GameObject btn;
    //public SafeHouseController safeHouse;
    // Start is called before the first frame update
    void Start()
    {
        Toolbox.GameManager.InstantiatePopup_Message1();
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        //Debug.Log(curLevelData);
        for (int i = 0; i < curLevelData.hasResources.Length; i++)
        {
            //  checkToTransfer[i] = ((int)curLevelData.hasResources[i]);
         //   Debug.Log(((int)curLevelData.hasResources[i]));
           

                checkToTransferBool[(int)curLevelData.hasResources[i]] = true;
            
        }
        //for (int i = 0; i < 9; i++)
        //{
        //    if (((int)curLevelData.hasResources[i]) == 1)
        //    {
        //        checkToTransferBool[i] = true;
        //    }
        //}
        StoreResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateStorage()
    {
       
        for (int i = 0; i < 9; i++)
        {
           
            resources[i] = Toolbox.DB.prefs.ResourceAmount[i].value;
            resourcesTexts[i].text = resources[i].ToString();
        }
        for (int i = 0; i < 9; i++)
        {
           
            resourcesInStorage[i] = Toolbox.DB.prefs.ResourceAmountInStorage[i].value;
            resourcesTextsInStorage[i].text = resourcesInStorage[i].ToString();
        }
    }
    public void StoreResources()
    {
        for (int i = 0; i < 9; i++)
        {
            Toolbox.DB.prefs.ResourceAmountInStorage[i].value = Toolbox.DB.prefs.ResourceAmountInStorage[i].value + Toolbox.DB.prefs.ResourceAmount[i].value;
            Toolbox.DB.prefs.ResourceAmount[i].value = 0;
            resources[i] = Toolbox.DB.prefs.ResourceAmount[i].value;
            resourcesTexts[i].text = resources[i].ToString();
            resourcesInStorage[i] = Toolbox.DB.prefs.ResourceAmountInStorage[i].value;
            resourcesTextsInStorage[i].text = resourcesInStorage[i].ToString();
            Toolbox.GameManager.InstantiatePopup_MessageBar("Resources Stored In Storage");
            
        }
        if (Toolbox.GameplayScript)
        {
            FindObjectOfType<PlayerController>().UpdateResourceMinus();
        }
    }
    public void RetriveResources()
    {
        for (int i = 0; i < 9; i++)
        {
            if (checkToTransferBool[i])
            {
                Toolbox.DB.prefs.ResourceAmount[i].value = Toolbox.DB.prefs.ResourceAmount[i].value + Toolbox.DB.prefs.ResourceAmountInStorage[i].value;
                Toolbox.DB.prefs.ResourceAmountInStorage[i].value = 0;
                CheckExtraItem();
                resources[i] = Toolbox.DB.prefs.ResourceAmount[i].value;
                resourcesTexts[i].text = resources[i].ToString();
                resourcesInStorage[i] = Toolbox.DB.prefs.ResourceAmountInStorage[i].value;
                resourcesTextsInStorage[i].text = resourcesInStorage[i].ToString();
                Toolbox.GameManager.InstantiatePopup_MessageBar("Resources Retrived In Inventory");
               
            }
        }
         if (Toolbox.GameplayScript)
                {
                    FindObjectOfType<PlayerController>().UpdateResource();
                }
    }
    public void CheckExtraItem()
    {
       
        for (int i = 0; i < 9; i++)
        {
            if (Toolbox.DB.prefs.ResourceAmount[i].value > capacity)
            {
                temp[i] = Toolbox.DB.prefs.ResourceAmount[i].value - capacity;
                Toolbox.DB.prefs.ResourceAmountInStorage[i].value = Toolbox.DB.prefs.ResourceAmountInStorage[i].value + temp[i];
                Toolbox.DB.prefs.ResourceAmount[i].value = Toolbox.DB.prefs.ResourceAmount[i].value - temp[i];
            }
        } 
    }
}
