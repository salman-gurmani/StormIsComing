using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TradeShopCanvus : MonoBehaviour
{
    public TradeShopController tradeshop;


    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Sprite[] resourcesImgs;
    Transform player;
    string type1;
    string type2;


    // Start is called before the first frame update
    void Start()
    {
        player = Toolbox.GameplayScript.player.gameObject.transform;
        text1.text = tradeshop.amount.ToString();
        text2.text = tradeshop.amount.ToString();
        text3.text = tradeshop.amount.ToString();
        text4.text = tradeshop.amount.ToString();
        type1 = tradeshop.type1.ToString();
        type2 = tradeshop.type2.ToString();
        switch(type1)
        {
            case "WOOD_LOG":
                img1.sprite = resourcesImgs[0];
                img4.sprite = resourcesImgs[0];
                break;
            case "STONE_BLOCK":
                img1.sprite = resourcesImgs[1];
                img4.sprite = resourcesImgs[1];
                break;
            case "MUD_BLOCK":
                img1.sprite = resourcesImgs[2];
                img4.sprite = resourcesImgs[2];
                break;
            case "CEMENT_BLOCK":
                img1.sprite = resourcesImgs[3];
                img4.sprite = resourcesImgs[3];
                break;
            case "STEEL_ROD":
                img1.sprite = resourcesImgs[4];
                img4.sprite = resourcesImgs[4];
                break;
        }
        switch (type2)
        {
            case "WOOD_LOG":
                img2.sprite = resourcesImgs[0];
                img3.sprite = resourcesImgs[0];
                break;
            case "STONE_BLOCK":
                img2.sprite = resourcesImgs[1];
                img3.sprite = resourcesImgs[1];
                break;
            case "MUD_BLOCK":
                img2.sprite = resourcesImgs[2];
                img3.sprite = resourcesImgs[2];
                break;
            case "CEMENT_BLOCK":
                img2.sprite = resourcesImgs[3];
                img3.sprite = resourcesImgs[3];
                break;
            case "STEEL_ROD":
                img2.sprite = resourcesImgs[4];
                img3.sprite = resourcesImgs[4];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Tranfer1()
    {
        if (Toolbox.DB.prefs.ResourceAmount[tradeshop.Type2Int].value >= tradeshop.amount)
        {
            switch (type1)
            {
                case "WOOD_LOG":
                    Instantiate(tradeshop.effects[0], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value + tradeshop.amount;
                    break;
                case "STONE_BLOCK":
                    Instantiate(tradeshop.effects[1], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value + tradeshop.amount;
                    break;
                case "MUD_BLOCK":
                    Instantiate(tradeshop.effects[2], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value + tradeshop.amount;
                    break;
                case "CEMENT_BLOCK":
                    Instantiate(tradeshop.effects[3], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value + tradeshop.amount;
                    break;
                case "STEEL_ROD":
                    Instantiate(tradeshop.effects[4], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value + tradeshop.amount;
                    break;
            }
            switch (type2)
            {
                case "WOOD_LOG":
                    Instantiate(tradeshop.effects[0], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value - tradeshop.amount;
                    break;
                case "STONE_BLOCK":
                    Instantiate(tradeshop.effects[1], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value - tradeshop.amount;
                    break;
                case "MUD_BLOCK":
                    Instantiate(tradeshop.effects[2], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value - tradeshop.amount;
                    break;
                case "CEMENT_BLOCK":
                    Instantiate(tradeshop.effects[3], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value - tradeshop.amount;
                    break;
                case "STEEL_ROD":
                    Instantiate(tradeshop.effects[4], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value - tradeshop.amount;
                    break;

            }
            tradeshop.pnl.SetActive(false);




            foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
            {
            
                Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);
                
            }







        }
        else
        {
            Debug.Log("Lowwwww Resource");
            tradeshop.pnl.SetActive(false);
        }
    }
    public void Tranfer2()
    {
        if (Toolbox.DB.prefs.ResourceAmount[tradeshop.Type1Int].value >= tradeshop.amount)
        {
            switch (type2)
            {
                case "WOOD_LOG":
                    Instantiate(tradeshop.effects[0], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value + tradeshop.amount;
                    break;
                case "STONE_BLOCK":
                    Instantiate(tradeshop.effects[1], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value + tradeshop.amount;
                    break;
                case "MUD_BLOCK":
                    Instantiate(tradeshop.effects[2], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value + tradeshop.amount;
                    break;
                case "CEMENT_BLOCK":
                    Instantiate(tradeshop.effects[3], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value + tradeshop.amount;
                    break;
                case "STEEL_ROD":
                    Instantiate(tradeshop.effects[4], transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value + tradeshop.amount;
                    break;
            }
            switch (type1)
            {
                case "WOOD_LOG":
                    Instantiate(tradeshop.effects[0], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value - tradeshop.amount;
                    break;
                case "STONE_BLOCK":
                    Instantiate(tradeshop.effects[1], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value - tradeshop.amount;
                    break;
                case "MUD_BLOCK":
                    Instantiate(tradeshop.effects[2], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value - tradeshop.amount;
                    break;
                case "CEMENT_BLOCK":
                    Instantiate(tradeshop.effects[3], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value - tradeshop.amount;
                    break;
                case "STEEL_ROD":
                    Instantiate(tradeshop.effects[4], player.transform.position, Quaternion.identity);
                    Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value - tradeshop.amount;
                    break;
            }
            tradeshop.pnl.SetActive(false);
            foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
            {

                Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);

            }
        }
        else
        {
            Debug.Log("Lowwwww Resource");
            tradeshop.pnl.SetActive(false);
        }
    }
}