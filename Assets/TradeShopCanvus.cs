using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DialogueEditor;

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
    public int resourceVal = 0;
    public int resourceLimit;
    // Start is called before the first frame update
    void Start()
    {
        player = Toolbox.GameplayScript.player.gameObject.transform;
        text1.text = tradeshop.amountExchange.ToString();
        text2.text = tradeshop.amountExchange.ToString();
        text3.text = tradeshop.amountExchange.ToString();
        text4.text = tradeshop.amountExchange.ToString();
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
            case "IRON_BLOCK":
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
            case "IRON_BLOCK":
                img2.sprite = resourcesImgs[4];
                img3.sprite = resourcesImgs[4];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Toolbox.DB.prefs.ResourceAmount[0].value >= 5)
        {
            tradeshop.amountExchange = Toolbox.DB.prefs.ResourceAmount[0].value;
            text1.text = tradeshop.amountExchange.ToString();
            text2.text = tradeshop.amountExchange.ToString();
            text3.text = tradeshop.amountExchange.ToString();
            text4.text = tradeshop.amountExchange.ToString();
        }
        else if (Toolbox.DB.prefs.ResourceAmount[1].value >= 5)
        {
            tradeshop.amountExchange = Toolbox.DB.prefs.ResourceAmount[1].value;
            text1.text = tradeshop.amountExchange.ToString();
            text2.text = tradeshop.amountExchange.ToString();
            text3.text = tradeshop.amountExchange.ToString();
            text4.text = tradeshop.amountExchange.ToString();
        }
        else if (Toolbox.DB.prefs.ResourceAmount[2].value >= 5)
        {
            tradeshop.amountExchange = Toolbox.DB.prefs.ResourceAmount[2].value;
            text1.text = tradeshop.amountExchange.ToString();
            text2.text = tradeshop.amountExchange.ToString();
            text3.text = tradeshop.amountExchange.ToString();
            text4.text = tradeshop.amountExchange.ToString();
        }
        else if (Toolbox.DB.prefs.ResourceAmount[4].value >= 5)
        {
            tradeshop.amountExchange = Toolbox.DB.prefs.ResourceAmount[4].value;
            text1.text = tradeshop.amountExchange.ToString();
            text2.text = tradeshop.amountExchange.ToString();
            text3.text = tradeshop.amountExchange.ToString();
            text4.text = tradeshop.amountExchange.ToString();
        }
        else if (Toolbox.DB.prefs.ResourceAmount[6].value >= 5)
        {
            tradeshop.amountExchange = Toolbox.DB.prefs.ResourceAmount[6].value;
            text1.text = tradeshop.amountExchange.ToString();
            text2.text = tradeshop.amountExchange.ToString();
            text3.text = tradeshop.amountExchange.ToString();
            text4.text = tradeshop.amountExchange.ToString();
        }
    }
    public void Tranfer2()
    {
        if (Toolbox.DB.prefs.ResourceAmount[tradeshop.Type2Int].value >= tradeshop.amountExchange)
        {
            if (Toolbox.DB.prefs.ResourceAmount[tradeshop.Type1Int].value + tradeshop.amountExchange <= Toolbox.DB.prefs.MaxCarryLimit)
            {
                switch (type1)
                {
                    case "WOOD_LOG":
                        Instantiate(tradeshop.effects[0], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value + tradeshop.amountExchange;
                        break;
                    case "STONE_BLOCK":
                        Instantiate(tradeshop.effects[1], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value + tradeshop.amountExchange;
                        break;
                    case "MUD_BLOCK":
                        Instantiate(tradeshop.effects[2], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value + tradeshop.amountExchange;
                        break;
                    case "CEMENT_BLOCK":
                        Instantiate(tradeshop.effects[3], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value + tradeshop.amountExchange;
                        break;
                    case "IRON_BLOCK":
                        Instantiate(tradeshop.effects[4], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value + tradeshop.amountExchange;
                        break;
                }
                switch (type2)
                {
                    case "WOOD_LOG":
                        Instantiate(tradeshop.effects[0], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value - tradeshop.amountExchange;
                        break;
                    case "STONE_BLOCK":
                        Instantiate(tradeshop.effects[1], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value - tradeshop.amountExchange;
                        break;
                    case "MUD_BLOCK":
                        Instantiate(tradeshop.effects[2], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value - tradeshop.amountExchange;
                        break;
                    case "CEMENT_BLOCK":
                        Instantiate(tradeshop.effects[3], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value - tradeshop.amountExchange;
                        break;
                    case "IRON_BLOCK":
                        Instantiate(tradeshop.effects[4], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value - tradeshop.amountExchange;
                        break;

                }
                for (int i = 0; i < tradeshop.amountExchange; i++)
                {
                    Toolbox.GameplayScript.player.AddResourceOnBack(tradeshop.type1);
                }
                for (int i = 0; i < tradeshop.amountExchange; i++)
                {
                    Toolbox.GameplayScript.player.RemoveResourceOnBack(tradeshop.type2);
                }
                tradeshop.pnl.SetActive(false);




                foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
                {

                    Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);

                }


            }
            else
            {
                Toolbox.GameManager.InstantiatePopup_Message("Your Pack Is Full.");
                Debug.Log("Your Pack Is Full");
                tradeshop.pnl.SetActive(false);
            }
        }
        else
        {
            Toolbox.GameManager.InstantiatePopup_Message("You dont have enough Resource to trade.");
            Debug.Log("Lowwwww Resource");
            tradeshop.pnl.SetActive(false);
        }
    }
    public void Tranfer1()
    {
        if (Toolbox.DB.prefs.ResourceAmount[tradeshop.Type1Int].value >= tradeshop.amountExchange)
        {
            if (Toolbox.DB.prefs.ResourceAmount[tradeshop.Type2Int].value + tradeshop.amountExchange <= Toolbox.DB.prefs.MaxCarryLimit)
            {
                switch (type2)
                {
                    case "WOOD_LOG":
                        Instantiate(tradeshop.effects[0], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value + tradeshop.amountExchange;
                        break;
                    case "STONE_BLOCK":
                        Instantiate(tradeshop.effects[1], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value + tradeshop.amountExchange;
                        break;
                    case "MUD_BLOCK":
                        Instantiate(tradeshop.effects[2], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value + tradeshop.amountExchange;
                        break;
                    case "CEMENT_BLOCK":
                        Instantiate(tradeshop.effects[3], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value + tradeshop.amountExchange;
                        break;
                    case "IRON_BLOCK":
                        Instantiate(tradeshop.effects[4], transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value + tradeshop.amountExchange;
                        break;
                }
                switch (type1)
                {
                    case "WOOD_LOG":
                        Instantiate(tradeshop.effects[0], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[0].value = Toolbox.DB.prefs.ResourceAmount[0].value - tradeshop.amountExchange;
                        break;
                    case "STONE_BLOCK":
                        Instantiate(tradeshop.effects[1], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[1].value = Toolbox.DB.prefs.ResourceAmount[1].value - tradeshop.amountExchange;
                        break;
                    case "MUD_BLOCK":
                        Instantiate(tradeshop.effects[2], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[2].value = Toolbox.DB.prefs.ResourceAmount[2].value - tradeshop.amountExchange;
                        break;
                    case "CEMENT_BLOCK":
                        Instantiate(tradeshop.effects[3], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[4].value = Toolbox.DB.prefs.ResourceAmount[4].value - tradeshop.amountExchange;
                        break;
                    case "IRON_BLOCK":
                        Instantiate(tradeshop.effects[4], player.transform.position, Quaternion.identity);
                        Toolbox.DB.prefs.ResourceAmount[6].value = Toolbox.DB.prefs.ResourceAmount[6].value - tradeshop.amountExchange;
                        break;
                }
                for (int i = 0; i < tradeshop.amountExchange; i++)
                {
                    Toolbox.GameplayScript.player.AddResourceOnBack(tradeshop.type2);
                }
                for (int i = 0; i < tradeshop.amountExchange; i++)
                {
                    Toolbox.GameplayScript.player.RemoveResourceOnBack(tradeshop.type1);
                }
                tradeshop.pnl.SetActive(false);
                foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
                {

                    Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);

                }
            }
            else
            {
                Toolbox.GameManager.InstantiatePopup_Message("Your Pack Is Full.");
                Debug.Log("Your Pack Is Full");
                tradeshop.pnl.SetActive(false);
            }

            if (Toolbox.DB.prefs.LastSelectedLevel == 4)
            {
                ConversationManager.Instance.PressSelectedOption();
            }
        }
        else
        {
            Toolbox.GameManager.InstantiatePopup_Message("You dont have enough Resource to trade.");
            Debug.Log("Lowwwww Resource");
            tradeshop.pnl.SetActive(false);
        }
    }
}
