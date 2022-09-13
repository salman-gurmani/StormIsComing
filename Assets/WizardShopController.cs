using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShopController : MonoBehaviour
{
    public TradeShopController tradeshop;
    public ResourceType type1;
     string type1s;
    
    public int amount;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowADS()
    {
        type1s = type1.ToString();
        AdsManager.instance.WizardShopAd(AdsManager.RewardType.Wizard);
    }




    public void GiveReward()
    {
        for (int i = 0; i < amount; i++)
        {
            Toolbox.GameplayScript.player.AddResourceOnBack(type1);
        }
       
        switch (type1s)
        {
            case "WOOD_LOG":
                Instantiate(tradeshop.effects[0], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value + amount;
                break;
            case "STONE_BLOCK":
                Instantiate(tradeshop.effects[1], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value + amount;
                break;
            case "MUD_BLOCK":
                Instantiate(tradeshop.effects[2], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value + amount;
                break;
            case "CEMENT_BLOCK":
                Instantiate(tradeshop.effects[3], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value + amount;
                break;
            case "IRON_BLOCK":
                Instantiate(tradeshop.effects[4], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value + amount;
                break;
        }



        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {

            Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);

        }
        tradeshop.DisAblePnl();
    }
}
