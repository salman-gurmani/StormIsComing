using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShopController : MonoBehaviour
{
    public Animator aminShopkepper;
    public TradeShopController tradeshop;
    public ResourceType type1;
     string type1s;
    public Sprite[] imgs;
    public int amount;
    public GameObject btnNOAdsVailable;
    public SpriteRenderer img;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateIMG()
    {
        type1s = type1.ToString();
        switch (type1s)
        {
            case "WOOD_LOG":
                
                img.sprite = imgs[0];
              
                break;
            case "STONE_BLOCK":
               
                 img.sprite = imgs[1];
                break;
            case "MUD_BLOCK":
               
                img.sprite = imgs[2];
                break;
            case "CEMENT_BLOCK":
                
                img.sprite = imgs[3];
                break;
            case "IRON_BLOCK":
               
                img.sprite = imgs[4];
                break;
        }
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
                img.sprite = imgs[0];
                Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value + amount;
                break;
            case "STONE_BLOCK":
                Instantiate(tradeshop.effects[1], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value + amount;
                img.sprite = imgs[1];
                break;
            case "MUD_BLOCK":
                Instantiate(tradeshop.effects[2], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value + amount;
                img.sprite = imgs[2];
                break;
            case "CEMENT_BLOCK":
                Instantiate(tradeshop.effects[3], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value + amount;
                img.sprite = imgs[3];
                break;
            case "IRON_BLOCK":
                Instantiate(tradeshop.effects[4], transform.position, Quaternion.identity);
                Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value + amount;
                img.sprite = imgs[4];
                break;
        }



        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {

            Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);

        }
        tradeshop.DisAblePnl();
    }
}
