using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageController : MonoBehaviour
{
    public GameObject travelpnl;
    public GameObject travelpnl2;
    public int[] checkToTransfer;
    public bool[] checkToTransferBool;
    public int[] temp;
    public int capacity = 5;
    public int[] resources;
    public int[] resourcesInStorage;
    public Text[] resourcesTexts;
    public Text[] resourcesTextsInStorage;
    public GameObject btn;
    public Button store;
    public Button retrieve;
    //public SafeHouseController safeHouse;
    // Start is called before the first frame update
    void Start()
    {
        store.interactable = true;
        retrieve.interactable = true;
        //Toolbox.GameManager.InstantiatePopup_Message1();
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
        //StoreResources();
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
            store.interactable = false;
            //FindObjectOfType<PlayerController>().HUDSH.SetActive(true);


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
                int Player = Toolbox.DB.prefs.ResourceAmount[0].value + Toolbox.DB.prefs.ResourceAmount[1].value + Toolbox.DB.prefs.ResourceAmount[2].value + Toolbox.DB.prefs.ResourceAmount[3].value + Toolbox.DB.prefs.ResourceAmount[4].value + Toolbox.DB.prefs.ResourceAmount[5].value + Toolbox.DB.prefs.ResourceAmount[6].value + Toolbox.DB.prefs.ResourceAmount[7].value + Toolbox.DB.prefs.ResourceAmount[8].value;
                if (Player > Toolbox.DB.prefs.MaxCarryLimit)
                    return;
                else
                {
                    Toolbox.DB.prefs.ResourceAmount[i].value = Toolbox.DB.prefs.ResourceAmount[i].value + Toolbox.DB.prefs.ResourceAmountInStorage[i].value;
                    Toolbox.DB.prefs.ResourceAmountInStorage[i].value = 0;
                    //CheckExtraItem();
                    resources[i] = Toolbox.DB.prefs.ResourceAmount[i].value;
                    resourcesTexts[i].text = resources[i].ToString();
                    resourcesInStorage[i] = Toolbox.DB.prefs.ResourceAmountInStorage[i].value;
                    resourcesTextsInStorage[i].text = resourcesInStorage[i].ToString();
                    Toolbox.GameManager.InstantiatePopup_MessageBar("Resources Retrieved In Inventory");
                    retrieve.interactable = false;
                    //FindObjectOfType<PlayerController>().HUDSH.SetActive(true);
                }
                

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
    public void Close()
    {
        travelpnl2.SetActive(false);
        FindObjectOfType<PlayerController>().HUDSH.SetActive(true);
    }
}