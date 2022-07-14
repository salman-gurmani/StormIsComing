﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[Header("Audio Sources")]
	public AudioSource audioo; 
	public AudioSource bgMusicSource;
	public AudioSource vehicleRadio;
	public AudioSource Running;

	[Header("BG Clips")]
	public AudioClip menuBG;
	public AudioClip gameBG;
	public AudioClip[] weatherBG;

	[Header("Sound Clips")]
    public AudioClip Select;
	public AudioClip Play;
	public AudioClip gatherIronPick;
	public AudioClip gatherCement;
	public AudioClip gatherBrick;
	public AudioClip gatherStone;
	public AudioClip gatherLogs;
	public AudioClip running;
	public AudioClip BrickFactory;
    public AudioClip rightLeftObj;
	public AudioClip buttonPressYes;
	public AudioClip buttonPressNo;
    public AudioClip back;
    public AudioClip paint;
	public AudioClip fail;
	public AudioClip complete;
	public AudioClip Loading;
	public AudioClip coinsSound;
	public AudioClip chestSound;
	//public AudioClip [] Female;
 //   public AudioClip[] Male;
	//public AudioClip [] Male_Shouting;
	//public AudioClip [] Female_Shouting;
	//public AudioClip DoorOpen, DoorClose;
	//public AudioClip horn, WaterExplosion;
	//public AudioClip M_TaxiCall, F_TaxiCall;
	//public AudioClip Repair, DipperFlash, RadioMusic;
	//public AudioClip ThankYou_F, ThankYou_M;
	//public AudioClip Seatbelt_ON, Seatbelt_OFF;
	public AudioClip CollectReward;
	public AudioClip checkpoint;
	public AudioClip vehicleExplosion;
	public AudioClip EarthQuake;
	public AudioClip Tornado;
	public AudioClip Storm;
	public AudioClip fire;
	public AudioClip siren;
	public AudioClip panicSound;
	public AudioClip vehicleUpgrade;
	public AudioClip vehicleUnlock;
	//public AudioClip[] Sportsmode;
	//public AudioClip[] normalmode;

	void Start () {

		PlayBGSound(menuBG);
	
		UpdateSoundStatus();
		UpdateMusicStatus();

		//vehicleRadio.clip = RadioMusic; 
	}

	public void UpdateSoundStatus()
	{
		audioo.mute = !Toolbox.DB.prefs.GameAudio;
		vehicleRadio.mute = !Toolbox.DB.prefs.GameAudio;
		Running.mute = !Toolbox.DB.prefs.GameAudio;
		if (audioo.mute && bgMusicSource.mute)
		{
			AudioListener.volume = 0.0f;
		}
		else {
			AudioListener.volume = 1f;
		}
	}

	public void UpdateMusicStatus() {

		bgMusicSource.mute = !Toolbox.DB.prefs.GameMusic;

		if (audioo.mute && bgMusicSource.mute)
		{
			AudioListener.volume = 0.0f;
		}
		else
		{
			AudioListener.volume = 1f;
		}
	}

	public void Pause_All(){
		this.vehicleRadio.Pause();
		this.Running.Pause();
		this.audioo.Pause ();
		this.bgMusicSource.Pause ();
	}

	public void UnPause_All(){

		this.audioo.UnPause ();
		this.bgMusicSource.UnPause ();
		this.Running.UnPause();
		this.vehicleRadio.UnPause();

	}
    public void Pause_Sound()
    {
		this.vehicleRadio.Pause();
		this.Running.Pause();

		this.audioo.Pause();
    }

    public void PlayBGSound(AudioClip _clip) {

        this.bgMusicSource.clip = _clip;

        this.bgMusicSource.Play();

        this.bgMusicSource.loop = true;
    } 

	public void PlaySound(AudioClip _clip){

		if (_clip != null) 
			audioo.PlayOneShot (_clip);
		vehicleRadio.Play();
		Running.Play();
	} 


	public void Stop_PlayingSound(){
		audioo.Stop (); 
	}

	public void Stop_PlayingBGSound()
	{
		bgMusicSource.Stop();
	}

	public void ToggleRadio() {

		vehicleRadio.mute = !vehicleRadio.mute;

		vehicleRadio.Play();
	}
}
