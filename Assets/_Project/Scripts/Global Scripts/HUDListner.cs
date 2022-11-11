using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DialogueEditor;
using TMPro;

public class HUDListner : MonoBehaviour {

    public int intervalSpeedLess; 
    public float temp;
    public Text timeTxt;
    public Text lvlTxt; 
    public Text goldTxt;
    public Text playerCapacity;
    public GameObject uiParent; 
    public GameObject Bar;
    public GameObject playerCarry;
    public GameObject MiniMap; 
    public GameObject RewardedAd;
    public GameObject GlowImage;
    public GameObject TimeObject;
    public GameObject miniMap;
    public GameObject closeBtn;
    public Sprite redTimer;
    public Sprite defaultTimer;
    public RectTransform resourcesParent;
    private int resourceIndex = 0;
    public Image progressbar;
    [HideInInspector]public float progress;
    [Tooltip("Should be in the order of resources")]
    public Text[] resourcesTxts;
    public GameObject MiniMapScaleUp; 
    public RectTransform[] resourcePosition;
    //public int maxAmountPlayerCanCarry = 3;
    float reportTime = 20;
    [Space]
    public NPCConversation levelZeroTutorialConversation;
    public NPCConversation levelOneTutorialConversation;
    public NPCConversation levelTwoTutorialConversation;
    public NPCConversation levelThreeTutorialConversation;
    public NPCConversation levelFourTutorialConversation;
    public NPCConversation levelSixTutorialConversation;
    public NPCConversation levelSevenTutorialConversation;
    public NPCConversation levelTwelveTutorialConversation;
    public GameObject ConversationPanel;
    public GameObject CloseBtn;
    public bool IsEndNode;
    public GameObject BGPanel;
    bool RewardedVideoAd = true;
    bool SirenAd = true;
    public bool startTime { get; set; }
    public float tempTime { get; set; }

    private bool isGamePaused = false;
    private LevelData curLevelData;
    private Vector3 Pos, Scale ;
    public Text FillPercentage;

    private void OnEnable()
    {
        UpdateTxt();
        FillPercentage.text = ("0 %");
    }

    void Awake() {
        
        Toolbox.Set_HudListner(this.GetComponent<HUDListner>());
        if (Toolbox.DB.prefs.LastSelectedLevel == 2 || Toolbox.DB.prefs.LastSelectedLevel == 5 || Toolbox.DB.prefs.LastSelectedLevel == 8 || Toolbox.DB.prefs.LastSelectedLevel == 11 || Toolbox.DB.prefs.LastSelectedLevel == 14 || Toolbox.DB.prefs.LastSelectedLevel == 17 || Toolbox.DB.prefs.LastSelectedLevel == 19)
        {
            playerCarry.SetActive(false);
           // Bar.SetActive(false); 
        }
        else
        {
            playerCarry.SetActive(true);
            //CoinsCollect.SetActive(false);
        }
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        //AdsManager.instance.RequestBannerWithSpecs( Tapdaq.TDMBannerSize.TDMBannerStandard, Tapdaq.TDBannerPosition.Top);
    }

    private void Start()
    {
        Toolbox.GameplayScript.StartGame();
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        lvlTxt.text = curLevelData.LevelTxt;
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
            case 6:
                ConversationManager.Instance.StartConversation(levelSixTutorialConversation);
                break;
            case 7:
                ConversationManager.Instance.StartConversation(levelSevenTutorialConversation);
                break;
            case 12:
                ConversationManager.Instance.StartConversation(levelTwelveTutorialConversation);
                break;
            default:
                BGPanel.SetActive(false);
                break;

        }

        if (Toolbox.GameplayScript.levelsManager.CurLevelData.isBonus)
        {

        }
    }

    public void HideConversationPanel()
    {
        ConversationPanel.SetActive(false);
        CloseBtn.SetActive(false);
    }

    public void ShowCloseBtn()
    {
        //Invoke("CloseButtonFunc", 2f);
    }

    //public void CloseButtonFunc()
    //{
    //    CloseBtn.SetActive(true);
    //}
    public void SetIsGamePaused(bool _isGamePaused)
    {
        isGamePaused = _isGamePaused;
    }

    public void SetTutorialDialogueOptions(bool _isActive)
    {
        ConversationManager.Instance.OptionsPanel.gameObject.SetActive(_isActive);
        IsEndNode = _isActive;
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
            temp += Time.deltaTime;
            //Debug.LogError("roundedSec = " + roundedSec);
            //if(temp >= intervalSpeedLess)
            //{
            //    temp = 0f;
            //    if(Toolbox.GameplayScript.player.playerSpeed > 1)
            //    {
            //        Toolbox.GameplayScript.player.playerSpeed--;
            //    }
            //    if(Toolbox.GameplayScript.player.playerSpeed == 1)
            //    {
            //        Toolbox.GameplayScript.player.drunk = true;
            //      //Toolbox.GameplayScript.player.anim.SetBool("Run", false);
            //      //Toolbox.GameplayScript.player.anim.SetBool("Walk", true);
            //    }
            //}
            if (reportTime <= 0) {
                if (Toolbox.DB.prefs.LastSelectedLevel == 2 || Toolbox.DB.prefs.LastSelectedLevel == 5 || Toolbox.DB.prefs.LastSelectedLevel == 8 || Toolbox.DB.prefs.LastSelectedLevel == 11 || Toolbox.DB.prefs.LastSelectedLevel == 14 || Toolbox.DB.prefs.LastSelectedLevel == 17 || Toolbox.DB.prefs.LastSelectedLevel == 19)
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
                if (Toolbox.DB.prefs.LastSelectedLevel == 2 || Toolbox.DB.prefs.LastSelectedLevel == 5 || Toolbox.DB.prefs.LastSelectedLevel == 8 || Toolbox.DB.prefs.LastSelectedLevel == 11 || Toolbox.DB.prefs.LastSelectedLevel == 14 || Toolbox.DB.prefs.LastSelectedLevel == 17 || Toolbox.DB.prefs.LastSelectedLevel == 19)
                {
                    Toolbox.GameManager.Instantiate_LevelComplete(0);
                    Toolbox.Soundmanager.Stop_PlayingBGSound();
                }
                
                else
                {
                    MiniMap.SetActive(false);
                    GlowImage.SetActive(false);
                    closeBtn.SetActive(false);
                    Toolbox.GameplayScript.OnStormHandling();
                    Toolbox.Soundmanager.Stop_PlayingBGSound();
                }
                
            }
            if(tempTime <= 30f && tempTime >= 28f && SirenAd == true)
            {
                //Toolbox.GameManager.Instantiate_LevelComplete(0);
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.siren1);
                SirenAd=false;
            }
            if (tempTime <= 30 && RewardedVideoAd == true)
            {
                RewardedAd.SetActive(true);
                TimeObject.GetComponent<Image>().sprite = redTimer;
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

        resourcesTxts[_index].text = Toolbox.DB.prefs.ResourceAmount[_index].value.ToString();
        Toolbox.DB.prefs.CarryLimit = Toolbox.DB.prefs.ResourceAmount[0].value + Toolbox.DB.prefs.ResourceAmount[1].value + Toolbox.DB.prefs.ResourceAmount[2].value + Toolbox.DB.prefs.ResourceAmount[3].value + Toolbox.DB.prefs.ResourceAmount[4].value + Toolbox.DB.prefs.ResourceAmount[5].value + Toolbox.DB.prefs.ResourceAmount[6].value + Toolbox.DB.prefs.ResourceAmount[7].value + Toolbox.DB.prefs.ResourceAmount[8].value;
        playerCapacity.text = Toolbox.DB.prefs.CarryLimit + "/" + Toolbox.DB.prefs.MaxCarryLimit;
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
    public void OnPress_Store()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
        //Toolbox.GameManager.Instantiate_Shop();
    }
    public void Press_RewardedVideo()
    {

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        //--
        AdsManager.instance.SetNShowRewardedAd(AdsManager.RewardType.FREECOINS, 20);
        RewardedVideoAd = false;
        RewardedAd.SetActive(false);
        //TimeObject.GetComponent<Image>().sprite = defaultTimer;

    }

    public void Press_Camera()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
    }
    public void Press_ControlChange()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
    }
    public void OnPress_Settings()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
        Toolbox.GameManager.Instantiate_SettingsMenu();
    }
    public void Press_MiniMap()
    {
        GlowImage.SetActive(false);
        closeBtn.SetActive(true);
        Pos = MiniMapScaleUp.transform.position;
        Scale = MiniMapScaleUp.transform.localScale;
        Scale.x = 7;
        Scale.y = 7;
        Pos.x -= -270;
        Pos.y -= 500;
        MiniMapScaleUp.transform.position = Pos;
        MiniMapScaleUp.transform.localScale = Scale;
        MiniMapScaleUp.GetComponent<Button>().interactable = false;
        //StartCoroutine(TimeDelay());
    } 
    public void Press_CloseBtn()
    {
        closeBtn.SetActive(false);
        Pos = MiniMapScaleUp.transform.position;
        Scale = MiniMapScaleUp.transform.localScale;
        Scale.x = 1.7265f;
        Scale.y = 1.7265f;
        Pos.x += -270;
        Pos.y += 500;
        MiniMapScaleUp.GetComponent<Button>().interactable = true;
        MiniMapScaleUp.transform.position = Pos;
        MiniMapScaleUp.transform.localScale = Scale;
        GlowImage.SetActive(true);
    }
    /*IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(7f);
        Pos = MiniMapScaleUp.transform.position;
        Scale = MiniMapScaleUp.transform.localScale;
        Scale.x = 1.7265f;
        Scale.y = 1.7265f;
        Pos.x += -270;
        Pos.y += 500;
        MiniMapScaleUp.GetComponent<Button>().interactable = true;
        MiniMapScaleUp.transform.position = Pos;
        MiniMapScaleUp.transform.localScale = Scale;
        GlowImage.SetActive(true);
    }*/

    public void Press_Settings()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        Toolbox.GameManager.Instantiate_SettingsMenu();
    }

    public void SetProgressBarFill(float _val) {
        //Debug.LogError("Fill = " + _val);
        progress = _val;
        progressbar.fillAmount = _val;
        FillPercentage.text = Mathf.RoundToInt((progressbar.fillAmount *100)).ToString() + "%"; 
    }

}
