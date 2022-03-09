using System.Collections.Generic;
using UnityEngine;
using System.Collections;
/// <summary>
/// This script will hold everything that is needed to be global only in Game scene
/// </summary>
public class GameplayScript : MonoBehaviour {
    public bool testMode = true;
    public bool isLevelsScene = true;

    public bool levelCompleted = false;
    public bool levelFailed = false;

    public GameObject playerPrefab; 

    public GameObject [] environments;

    [HideInInspector]
    private int levelCompleteTime = 0;

    private bool doubleRewardBought = false;
    
    [Header("Components")]
    public AudioListener camListner;
    public bool canShowReviewMenu = false;
    public LevelsManager levelsManager;

    [Header("Colors")]
    public Color[] randomColors;


    //screenshot requirement
    int screenShotPicName = 0;

    public int LevelCompleteTime { get => levelCompleteTime; set => levelCompleteTime = value; }
    public bool DoubleRewardBought { get => doubleRewardBought; set => doubleRewardBought = value; }

    void Awake() {

        //if (!FindObjectOfType<Toolbox>())
        //    Instantiate(Resources.Load("Toolbox"), Vector3.zero, Quaternion.identity);

        Toolbox.Set_GameplayScript(this.GetComponent<GameplayScript>());
    }

    void Start()
    {
        levelCompleted = false;

        //EnableEnvHandling();
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            string name = "Pic_" + screenShotPicName + ".png";
            Toolbox.GameManager.Log("Screenshot Taked!");
            ScreenCapture.CaptureScreenshot(name);
            screenShotPicName++;
        }
#endif
    }

    public void StartGame()
    {
        Toolbox.HUDListner.EnableHUD();
        Toolbox.Soundmanager.PlayBGSound(Toolbox.Soundmanager.gameBG);
    }

    private void EnableEnvHandling()
    {
        environments[Toolbox.DB.prefs.LastSelectedEnv].gameObject.SetActive(true);
    }

    public void IncrementGoldCoins(int cost)
    {
        Toolbox.DB.prefs.GoldCoins += cost;
        Toolbox.GameManager.Instantiate_RewardAnim();

        if (FindObjectOfType<MainMenuListner>())
        {
            FindObjectOfType<MainMenuListner>().UpdateTxt();
        }
    }

    public void DeductGoldCoins(int cost) {

        Toolbox.DB.prefs.GoldCoins -= cost;

        if (FindObjectOfType<MainMenuListner>()) {

            FindObjectOfType<MainMenuListner>().UpdateTxt();
        }
    }

    public void InitEffectOnpoint(GameObject effect, Vector3 pos) {

        Instantiate(effect, pos, Quaternion.identity);
    }
    
    public Color Get_RandomColor() {

        return randomColors[Random.Range(0, randomColors.Length - 1)];
    }

    public void LevelCompleteHandling() {

        if (levelFailed || levelCompleted)
            return;

        levelCompleted = true;

        Toolbox.GameManager.Instantiate_LevelComplete(1);
        Toolbox.HUDListner.DisableHUD();

    }

    public void LevelFailHandling() {

        if (levelFailed || levelCompleted)
            return;

        levelFailed = true;

        Toolbox.GameManager.Instantiate_LevelFail(1);
        Toolbox.HUDListner.DisableHUD();
    }

    public void SetSkybox(Material _mat) { 
    
        RenderSettings.skybox = _mat;
    }
}
