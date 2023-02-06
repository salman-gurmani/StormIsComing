using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnergyDrink()
    {
        if (Toolbox.DB.prefs.GoldCoins >= 10)
        {
          //  tradeshop.pnl.SetActive(false);
            Toolbox.GameplayScript.player.drunk = false;
            Toolbox.GameplayScript.player.playerSpeed = 4f;
            Toolbox.GameManager.InstantiatePopup_Message("You have bought Energy Drink");
            Toolbox.DB.prefs.GoldCoins = Toolbox.DB.prefs.GoldCoins - 10;
            FindObjectOfType<HUDListner>().UpdateTxt();
        }
        else
        {
            Toolbox.GameManager.InstantiatePopup_Message("You dont have enough coins");
        }
    }
    public void RewardedBtn()
    {
        AdsManager.instance.SetNShowRewardedAd(AdsManager.RewardType.EnergyDrink, 10);
    }
    public void GiveReward()
    {
       // tradeshop.pnl.SetActive(false);
        Toolbox.GameplayScript.player.drunk = false;
        Toolbox.GameplayScript.player.playerSpeed = 4f;
        Toolbox.GameManager.InstantiatePopup_Message("You have bought Energy Drink");
    }
    public void OnPress_CloseSC()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);
        Destroy(this.gameObject);
        Time.timeScale = 1f;
        //AudioListener.pause = false; 
    }
}
