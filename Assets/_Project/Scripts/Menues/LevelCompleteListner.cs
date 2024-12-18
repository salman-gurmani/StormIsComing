﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteListner : MonoBehaviour {

	public Text lvlTxt;
	public Text goldTxt;
	public Text pointTxt;
	public GameObject lvlComplete;
	[Header("Unlock Car")]
	bool startAnim = false;
	public GameObject carUnlockObj;
	public GameObject unlockEffect;
	public Image blackVehicleImg;
	public Image realVehicleImg;
	public Image fillBar;
	public Text levelEarningTxt;
	public Text lifeBonusTxt;
	public Text netWorthTxt;

	int rewardAmount = 75;
	//public Sprite[] vehicleImages;

	private void Start()
    {
		string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
		LevelData curLevelData = (LevelData)Resources.Load(path);
		lvlTxt.text = curLevelData.LevelTxt;
		Invoke("ShowNoThanksBtb",1f);
		Time.timeScale = 1;
		Toolbox.GameManager.Analytics_LevelComplete();

        EarningsHandling();

		//UnlockNextLevel();
		//NextEnvSetHandling();
		//if ((Toolbox.DB.prefs.LastSelectedLevel + 1) % 5 == 0)
		//	Toolbox.DB.prefs.IsBossLevel = true;
		//else
		//	Toolbox.DB.prefs.IsBossLevel = false;


		//if (Toolbox.DB.prefs.LastSelectedLevel % 2 == 0)
		//{
		//	Toolbox.DB.prefs.StartSpawnPlayersVal++;
		//}

		SetTxt();
	}

	public void SetTxt() {

		goldTxt.text = Toolbox.DB.prefs.GoldCoins.ToString();
	}

	public void NextEnvSetHandling() {

		if (Toolbox.DB.prefs.LastSelectedLevel % 5 == 0) {

			Toolbox.DB.prefs.LastSelectedEnv++;

			if (Toolbox.DB.prefs.LastSelectedEnv > 2) {

				Toolbox.DB.prefs.LastSelectedEnv = 0;
			}
		}


		if (Toolbox.DB.prefs.LastSelectedLevel % 2 == 0)
		{
			Toolbox.DB.prefs.StartSpawnPlayersVal++;
		}
	}

	private void EarningsHandling()
    {
		/*if(Toolbox.HUDListner.progress > 0.5f && Toolbox.HUDListner.progress == 1f)
        {
			rewardAmount = (Toolbox.DB.prefs.LastSelectedLevel + 1) * 200;
		}
		else if(Toolbox.HUDListner.progress > 0.5f && Toolbox.HUDListner.progress < 1f)
        {
			rewardAmount = (Toolbox.DB.prefs.LastSelectedLevel + 1) * 100;
		}
		
		levelEarningTxt.text = "+" + rewardAmount.ToString();
		Toolbox.GameplayScript.IncrementGoldCoins(rewardAmount);*/

		levelEarningTxt.text = " + " + rewardAmount.ToString();
		pointTxt.text = " + 175".ToString();
		Toolbox.GameplayScript.IncrementGoldCoins(rewardAmount);
		Toolbox.DB.prefs.ExpPoints += 175; 
		
	}

    IEnumerator StartAnimation() {

		yield return new WaitForSeconds(1);

		startAnim = true;

	}

    private void UnlockNextLevel()
    {  
		Toolbox.DB.prefs.LastSelectedLevel++;
        if (Toolbox.DB.prefs.LastSelectedLevel < Toolbox.DB.prefs.GameMode[Toolbox.DB.prefs.LastSelectedMode].GetLastUnlockedLevel())
            return;
        
        if (Toolbox.DB.prefs.LastSelectedLevel == Constants.maxLevelsOfMode[Toolbox.DB.prefs.LastSelectedMode] - 1)
        {
			
			//This is the last level of current mode
			//nextButton.SetActive(false);
		}
        else
        {
            Toolbox.DB.prefs.GameMode[Toolbox.DB.prefs.LastSelectedMode].LevelUnlocked[Toolbox.DB.prefs.LastSelectedLevel + 1] = true;
        }

    }

	private void UnlockModeHandling()
	{

	}


	//Handles stars and Reward
	private void StarsHandling()
	{
		//if (Toolbox.GameplayScript.LevelCompleteTime > 40)
		//{
		//	star[0].SetActive(true);
		//	levelReward *= 1;			
		//}
		//else if (Toolbox.GameplayScript.LevelCompleteTime > 20)
		//{
		//	star[0].SetActive(true);
		//	star[1].SetActive(true);
			
		//	levelReward *= 2;
		//}
		//else {

		//	for (int i = 0; i < star.Length; i++)
		//	{
		//		star[i].SetActive(true);
		//	}

		//	levelReward *= 3;
		//}

		//rewardTxt.text = levelReward.ToString();
		//Toolbox.DB.prefs.GoldCoins += levelReward;
	}
	public void ShowNoThanksBtb()
    {
		//nextButton.SetActive(true);
    }
	public void Press_Next()
	{
		Toolbox.DB.prefs.JobAccepted = false;
		Toolbox.DB.prefs.StorageAccepted = false;
		if (Toolbox.DB.prefs.LastSelectedLevel < Constants.maxLevelsOfMode[Toolbox.DB.prefs.LastSelectedMode]-1)
        {
			Toolbox.DB.prefs.LastSelectedLevel++; 
		}
			
		else
		{
			Press_Home();
			return;
		}

		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
        Toolbox.GameManager.LoadScene(Toolbox.GameManager.GetCurrentLevelGameScene(), false, 0);

        //--
        AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL);

        //Toolbox.GameManager.directShowVehicleSel = true;
        //Toolbox.GameManager.LoadScene(Constants.sceneIndex_Menu, true, 0);

        Destroy(this.gameObject);
	}

	public void Press_Restart()
	{
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		Toolbox.GameManager.LoadScene(2, false, 0);
		for (int i = 0; i < 9; i++)
		{
			Toolbox.DB.prefs.ResourceAmount[i].value = 0;
		}
		//--
		AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL);


        Destroy(this.gameObject);
	}

	public void Press_Home()
	{

		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		Toolbox.GameManager.LoadScene(Constants.sceneIndex_Menu, false, 0);

        //--
        AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL);

        Destroy(this.gameObject);
	}

	public void Press_2XReward()
	{
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        //--
        AdsManager.instance.SetNShowRewardedAd(AdsManager.RewardType.DOUBLEREWARD, rewardAmount);

        //doubleRewardButton.SetActive(false);
	} 
	public void Close()
    {
		lvlComplete.SetActive(false);
	}

}
