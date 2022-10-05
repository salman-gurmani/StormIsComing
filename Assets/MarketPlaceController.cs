using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketPlaceController : MonoBehaviour
{
    public TradeShopController tradeshop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyBtn(int i)
    {
        if( i == 1 )
        {
            if(Toolbox.DB.prefs.GoldCoins >= 20)
            {
                Instantiate(tradeshop.effects[0], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value + 10;
                Toolbox.GameManager.InstantiatePopup_Message("You have bought Wood Logs");
                Toolbox.DB.prefs.GoldCoins = Toolbox.DB.prefs.GoldCoins - 20;
                for (int j = 0; j < 10; j++)
                {
                    Toolbox.GameplayScript.player.AddResourceOnBack(ResourceType.WOOD_LOG);
                }

                //if(Toolbox.DB.prefs.ResourceAmount[0].value >= 5)
                //{

                //}
            }
            else
            {
                Toolbox.GameManager.InstantiatePopup_Message("You dont have enough coins");
            }
            foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
            {

                Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);

            } 
        }
       
    }


    public void EnergyDrink()
    {
        if (Toolbox.DB.prefs.GoldCoins >= 10)
        {
            tradeshop.pnl.SetActive(false);
            Toolbox.GameplayScript.player.drunk = false;
            Toolbox.GameplayScript.player.playerSpeed = 4f;
            Toolbox.GameManager.InstantiatePopup_Message("You have bought Energy Drink");
            Toolbox.DB.prefs.GoldCoins = Toolbox.DB.prefs.GoldCoins - 10;
        }
        else
        {
            Toolbox.GameManager.InstantiatePopup_Message("You dont have enough coins");
        }
    }
}
