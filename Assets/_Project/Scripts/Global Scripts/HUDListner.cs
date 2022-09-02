using System;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using TMPro;

public class HUDListner : MonoBehaviour {

    public Text timeTxt;
    public Text goldTxt;
    public GameObject uiParent;
    public GameObject Bar;
    public GameObject CoinsCollect;
    public RectTransform resourcesParent;
    private int resourceIndex = 0;
    public Image progressbar;
    [HideInInspector]public float progress;
    [Tooltip("Should be in the order of resources")]
    public Text[] resourcesTxts;
    public GameObject MiniMap;
    public RectTransform[] resourcePosition;
    //public int maxAmountPlayerCanCarry = 3;
    float reportTime = 20;
    [Space]
    public NPCConversation levelZeroTutorialConversation;
    public NPCConversation levelOneTutorialConversation;
    public NPCConversation levelTwoTutorialConversation;
    public NPCConversation levelThreeTutorialConversation;
    public NPCConversation levelFourTutorialConversation;
    public bool startTime { get; set; }
    public float tempTime { get; private set; }

    private bool isGamePaused = false;
    private LevelData curLevelData;


    private void OnEnable()
    {
        UpdateTxt(); 
    }

    void Awake() {
        
        Toolbox.Set_HudListner(this.GetComponent<HUDListner>());
        if (Toolbox.DB.prefs.LastSelectedLevel == 2 || Toolbox.DB.prefs.LastSelectedLevel == 5 || Toolbox.DB.prefs.LastSelectedLevel == 8 || Toolbox.DB.prefs.LastSelectedLevel == 11 || Toolbox.DB.prefs.LastSelectedLevel == 14)
        {
           // Bar.SetActive(false);
            CoinsCollect.SetActive(true);
        }
        else
        {
            CoinsCollect.SetActive(false);
        }
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        //AdsManager.instance.RequestBannerWithSpecs( Tapdaq.TDMBannerSize.TDMBannerStandard, Tapdaq.TDBannerPosition.Top);
    }

    private void Start()
    {
        switch (Toolbox.DB.prefs.LastSelectedLevel)
        {
            case 0:
                ConversationManager.Instance.StartConversation(levelZeroTutorialConversation);
                break;
            case 1:
                ConversationManager.Instance.StartConversation(levelOneTutorialConversation);
                break;
            case 2:
                ConversationManager.Instance.StartConversation(levelTwoTutorialConversation);
                break;
            case 3:
                ConversationManager.Instance.StartConversation(levelThreeTutorialConversation);
                break;
            case 4:
                ConversationManager.Instance.StartConversation(levelFourTutorialConversation);
                break;
        }

        if (Toolbox.GameplayScript.levelsManager.CurLevelData.isBonus)
        {

        }
    }

    public void SetIsGamePaused(bool _isGamePaused)
    {
        isGamePaused = _isGamePaused;
    }

    public void SetTutorialDialogueOptions(bool _isActive)
    {
        ConversationManager.Instance.OptionsPanel.gameObject.SetActive(_isActive);
    }

    public void DisableHUD() 
    {        
        uiParent.SetActive(false);
    }

    public void EnableHUD()
    {
        uiParent.SetActive(true);
    }
    public void UpdateTxt()
    { 
        goldTxt.text = Toolbox.DB.prefs.GoldCoins.ToString();

    }

    private void Update()
    {
        HandleTime();
    }

    public void StartTime(float _val) {

        tempTime = _val;
        startTime = true;
    }

    private void HandleTime()
    {
        if (isGamePaused)
            return;

        if (startTime)
        {
            reportTime -= Time.deltaTime;

            tempTime -= Time.deltaTime;
            timeTxt.transform.parent.gameObject.SetActive(true);
            int roundedSec = Mathf.RoundToInt(tempTime);
            int min = roundedSec / 60;
            int seconds = roundedSec - (min * 60);

            timeTxt.text = String.Format("{0:D2} : {1:D2}", min, seconds);

            //Debug.LogError("roundedSec = " + roundedSec);

            if (reportTime <= 0) {
                if (Toolbox.DB.prefs.LastSelectedLevel == 2 || Toolbox.DB.prefs.LastSelectedLevel == 5 || Toolbox.DB.prefs.LastSelectedLevel == 8 || Toolbox.DB.prefs.LastSelectedLevel == 11 || Toolbox.DB.prefs.LastSelectedLevel == 14)
                {
                    reportTime = 25;
                }
                else
                {
                    Toolbox.GameManager.InstantiatePopup_MessageBar("Disaster is coming in " + roundedSec + " seconds.");
                    reportTime = 25;
                }
                    
            }

            if (tempTime <= 0)
            {
                startTime = false;
                if (Toolbox.DB.prefs.LastSelectedLevel == 2 || Toolbox.DB.prefs.LastSelectedLevel == 5 || Toolbox.DB.prefs.LastSelectedLevel == 8 || Toolbox.DB.prefs.LastSelectedLevel == 11 || Toolbox.DB.prefs.LastSelectedLevel == 14)
                {
                    Toolbox.GameManager.Instantiate_LevelComplete(0);
                }
                
                else
                {
                    Toolbox.GameplayScript.OnStormHandling();
                }
                
            }
        }
    }

    public void EnableResource(int _index) {

        
        Transform resource = resourcesParent.GetChild(_index).transform;
        resource.gameObject.SetActive(true);
        UpdateResourceTxt(_index);

        resource.GetComponent<RectTransform>().position = resourcePosition[resourceIndex].position;
        resourceIndex++;
    }

    public void UpdateResourceTxt(int _index) {

        resourcesTxts[_index].text = Toolbox.DB.prefs.ResourceAmount[_index].value.ToString() + "/" + Toolbox.DB.prefs.MaxCarryLimit;
    }

    public void UpdateAllResourceText()
    {
        for (int i = 0; i < resourcesTxts.Length; i++)
        {
            UpdateResourceTxt(i);
        }
    }

    public void Press_Pause() {

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
        Toolbox.GameManager.Instantiate_PauseMenu();
    }

    public void Press_Camera()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
    }
    public void Press_ControlChange()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
    }

    public void Press_Settings()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        Toolbox.GameManager.Instantiate_SettingsMenu();
    }

    public void SetProgressBarFill(float _val) {
        //Debug.LogError("Fill = " + _val);
        progress = _val;
        progressbar.fillAmount = _val;
    }

}
