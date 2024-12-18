﻿using UnityEngine;

public class PauseListner : MonoBehaviour {

	void OnDestroy(){
		Toolbox.Soundmanager.UnPause_All ();
		Time.timeScale = 1;
		AudioListener.pause = false;
	}
    private void OnEnable()
    {
		Toolbox.Soundmanager.Pause_All();
		AudioListener.pause = true;
	}
    private void Start()
    {
		Invoke("TimePause", 2f);
		//Time.timeScale = 0;
	}

	public void TimePause()
    {
		Time.timeScale = 0;

	}
	void Update(){

		if (Input.GetKeyDown (KeyCode.Escape)) {

			Press_Play ();
		}
	}

	public void Press_Play(){
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		Toolbox.HUDListner.EnableHUD();


		Destroy(this.gameObject,2f);
		Time.timeScale = 1;

	}

	public void Press_Restart()
	{
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		Toolbox.GameManager.LoadScene(2, true, 0);
		for (int i = 0; i < 9; i++)
		{
			Toolbox.DB.prefs.ResourceAmount[i].value = 0;
		}

		//--
		AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL);

        Destroy(this.gameObject);
	}

	public void Press_Home(){
		Toolbox.DB.prefs.JobAccepted = false;
		Toolbox.DB.prefs.StorageAccepted = false;
		Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
		Toolbox.GameManager.LoadScene(Constants.sceneIndex_Menu, true, 0);

        //--
        AdsManager.instance.ShowAd(AdsManager.AdType.INTERSTITIAL);

        Destroy(this.gameObject);
	}

}
