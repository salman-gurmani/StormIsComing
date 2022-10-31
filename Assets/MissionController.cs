using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
public class MissionController : MonoBehaviour
{
    public GameObject btn;
    public Text textInfo;
    public SafeHouseController safeHouse;
    // Start is called before the first frame update
    void Start()
    {
        AssignText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AcceptJob()
    {
        Toolbox.DB.prefs.JobAccepted = true;
        //ConversationManager.Instance.PressSelectedOption();
        //FindObjectOfType<HUDListner2>().ConversationPanel.SetActive(true);
        Toolbox.GameManager.InstantiatePopup_Message("Good, You have accepted the Contract Now go to Storage to get the resources you need");
    }
    public void RejectJob()
    {
        
    }
    public void AssignText()
    {
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        textInfo.text = curLevelData.levelInfo;

    }
}
