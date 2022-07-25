using UnityEngine;
using UnityEngine.UI;

public class LevelFailListner : MonoBehaviour {

	public GameObject skipBtn;
	public Text lvlTxt;
	public Text coinTxt;
	private void OnDestroy()
	{
		//AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL);
	}
	private void Start()
    {
		Time.timeScale = 1;

		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.fail);
		string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
		LevelData curLevelData = (LevelData)Resources.Load(path);
		lvlTxt.text = curLevelData.LevelTxt;
		coinTxt.text = Toolbox.DB.prefs.GoldCoins.ToString();
		Toolbox.GameManager.Analytics_LevelFail();
		Toolbox.GameManager.curLevelFailed++;

		//if (AdsManager.instance.isRewardedAdAvailable())
		//{
		//	skipBtn.SetActive(true);
		//}
		//else {

		//	skipBtn.SetActive(false);
		//}

	}

	public void Press_SkipLevel()
	{
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		AdsManager.instance.SetNShowRewardedAd(AdsManager.RewardType.SKIPLEVEL, 0);
	}

	public void Press_Restart()
	{
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		Toolbox.GameManager.LoadScene(Toolbox.GameManager.GetCurrentLevelGameScene(), false, 0); 
		AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL); 

		Destroy(this.gameObject);
	}

	public void Press_Home()
	{
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL); 
		Toolbox.GameManager.LoadScene(Constants.sceneIndex_Menu, true, 0);

		Destroy(this.gameObject);
	}
	public void UnlockAndPlayNextLevel()
    {
		if (Toolbox.DB.prefs.LastSelectedLevel < Toolbox.DB.prefs.GameMode[Toolbox.DB.prefs.LastSelectedMode].GetLastUnlockedLevel())
			Toolbox.DB.prefs.LastSelectedLevel++;

		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL); 
		Toolbox.GameManager.LoadScene(Toolbox.GameManager.GetCurrentLevelGameScene(), true, 0);
		//AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL);

		//Toolbox.GameManager.directShowVehicleSel = true;
		//Toolbox.GameManager.LoadScene(Constants.sceneIndex_Menu, true, 0);

		Destroy(this.gameObject);
	}
}
