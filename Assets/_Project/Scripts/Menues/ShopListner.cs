﻿using UnityEngine;
using UnityEngine.UI;

public class ShopListner : MonoBehaviour
{
    public Text goldTxt;

    private void OnEnable()
    {
        UpdateTxt();
    }

    void UpdateTxt()
    {
        goldTxt.text = Toolbox.DB.prefs.GoldCoins.ToString();
    }

    public void OnPress_Close()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressNo);
        Destroy(this.gameObject);
    }

    public void OnPress_FreeCoins()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        //--
        AdsManager.instance.SetNShowRewardedAd(AdsManager.RewardType.DOUBLEREWARD, 100);
    }
    public void OnPress_FreeCoins2()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        //--
        AdsManager.instance.SetNShowRewardedAd(AdsManager.RewardType.DOUBLEREWARD, 200);
    }
    public void OnPress_FreeCoins3()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        //--
        AdsManager.instance.SetNShowRewardedAd(AdsManager.RewardType.DOUBLEREWARD, 300);
    }
    public void PurchaseProduct(int _val)
    {

        switch (_val) {


            case 0:

                //Toolbox.InAppHandler.BuyProductID(Constants.coins_1);

                break;

            case 1:

                //Toolbox.InAppHandler.BuyProductID(Constants.coins_2);
                break;

            case 2:

                //Toolbox.InAppHandler.BuyProductID(Constants.unlockPlayerObj);
                break;

            case 3:

                //Toolbox.InAppHandler.BuyProductID(Constants.unlockLevels);
                break;
            case 4:

                //Toolbox.InAppHandler.BuyProductID(Constants.unlockLevels);
                break;

        }

    }
}
