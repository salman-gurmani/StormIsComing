using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopScript : MonoBehaviour
{

    public GameObject[] AllPlayers;
    public float[] StaminaAmt;
    public int[] CapacityAmt;
    public float[] SpeedAmt;
    public int[] StrengthAmt;
    public int[] Price;
    public Text StaminaTxt;
    public Text CapacityTxt;
    public Text SpeedTxt;
    public Text StrengthTxt;
    public Text GoldCoins;
    public Text ExpPoints;
    public Button BuyBtnCoins;
    public Button BuyBtnXP;
    public Button SelectedBtn;
    public Button ApplyBtn;
    public Text pricetxtCoins;
    public Text pricetxtXP;
   // public Image LockImg;
    public int Currentindex;
    public GameObject parentob;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Toolbox.DB.prefs.LastSelectedPlayerObj);

        SetCoinsXP();
        SetCharacterStart();
    }

    public void SetCoinsXP()
    {
        GoldCoins.text = Toolbox.DB.prefs.GoldCoins.ToString();
        ExpPoints.text = Toolbox.DB.prefs.ExpPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacterStart()
    {
        AllPlayers[0].SetActive(true);
        StaminaTxt.text = StaminaAmt[0].ToString();
        CapacityTxt.text = CapacityAmt[0].ToString();
        SpeedTxt.text = ((SpeedAmt[0])*10).ToString();
        StrengthTxt.text = "Lvl. " + StrengthAmt[0].ToString();
        BuyBtnCoins.gameObject.SetActive(false);
        if(Toolbox.DB.prefs.LastSelectedPlayerObj==0)
        {
            SelectedBtn.gameObject.SetActive(true);
            ApplyBtn.gameObject.SetActive(false);
        }
        else
        {
            SelectedBtn.gameObject.SetActive(false);
            ApplyBtn.gameObject.SetActive(true);
        }

        //for(int i =0;i<Toolbox.DB.prefs.CharactersUnlocked.Length;i++)
        //{

        //}
    }

    public void NextBtn()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

        AllPlayers[Currentindex].SetActive(false);

        Currentindex++;

        if (Currentindex > 9)
        {
            Currentindex = 0;
        }
        
        AllPlayers[Currentindex].SetActive(true);
        StaminaTxt.text = StaminaAmt[Currentindex].ToString();
        CapacityTxt.text = CapacityAmt[Currentindex].ToString();
        SpeedTxt.text = (SpeedAmt[Currentindex] * 10).ToString();
        StrengthTxt.text = "Lvl. " + StrengthAmt[Currentindex].ToString();

        if(Toolbox.DB.prefs.CharactersUnlocked[Currentindex]==true)
        {
            BuyBtnCoins.gameObject.SetActive(false);
            BuyBtnXP.gameObject.SetActive(false);

            if (Toolbox.DB.prefs.LastSelectedPlayerObj == Currentindex)
            {
                SelectedBtn.gameObject.SetActive(true);
                ApplyBtn.gameObject.SetActive(false);
            }
            else
            {
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(true);
            }
        }
        else
        {
            //if (Toolbox.DB.prefs.CharactersUnlocked[3] || Toolbox.DB.prefs.CharactersUnlocked[6] || Toolbox.DB.prefs.CharactersUnlocked[9])
            if (Currentindex==3 || Currentindex==6 || Currentindex==9)
            {
                BuyBtnCoins.gameObject.SetActive(false);
                BuyBtnXP.gameObject.SetActive(true);
                pricetxtCoins.text = Price[Currentindex].ToString();
                pricetxtXP.text = Price[Currentindex].ToString();
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(false);
            }
            else
            {
                BuyBtnCoins.gameObject.SetActive(true);
                BuyBtnXP.gameObject.SetActive(false);
                pricetxtCoins.text = Price[Currentindex].ToString();
                pricetxtXP.text = Price[Currentindex].ToString();
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(false);
            }
        }
    }

    public void PreviousBtn()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

        AllPlayers[Currentindex].SetActive(false);

        Currentindex--;

        if (Currentindex < 0)
        {
            Currentindex = 9;
        }
        
        AllPlayers[Currentindex].SetActive(true);
        StaminaTxt.text = StaminaAmt[Currentindex].ToString();
        CapacityTxt.text = CapacityAmt[Currentindex].ToString();
        SpeedTxt.text = (SpeedAmt[Currentindex] * 10).ToString();
        StrengthTxt.text = "Lvl. " + StrengthAmt[Currentindex].ToString();

        if (Toolbox.DB.prefs.CharactersUnlocked[Currentindex] == true)
        {
            BuyBtnCoins.gameObject.SetActive(false);
            BuyBtnXP.gameObject.SetActive(false);

            if (Toolbox.DB.prefs.LastSelectedPlayerObj == Currentindex)
            {
                SelectedBtn.gameObject.SetActive(true);
                ApplyBtn.gameObject.SetActive(false);
            }
            else
            {
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Currentindex == 3 || Currentindex == 6 || Currentindex == 9)
            {
                BuyBtnCoins.gameObject.SetActive(false);
                BuyBtnXP.gameObject.SetActive(true);
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(false);

                pricetxtCoins.text = Price[Currentindex].ToString();
                pricetxtXP.text = Price[Currentindex].ToString();
            }
            else
            {
                BuyBtnCoins.gameObject.SetActive(true);
                BuyBtnXP.gameObject.SetActive(false);
                pricetxtCoins.text = Price[Currentindex].ToString();
                pricetxtXP.text = Price[Currentindex].ToString();
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(false);
            }
        }

        Debug.Log(Toolbox.DB.prefs.LastSelectedPlayerObj);
    }

    public void BuyCoinsCharacter()
    {

        if(Price[Currentindex] <= Toolbox.DB.prefs.GoldCoins)
        {
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

            Toolbox.DB.prefs.GoldCoins -= Price[Currentindex];
            SetCoinsXP();
            Toolbox.DB.prefs.CharactersUnlocked[Currentindex] = true;
            Toolbox.DB.prefs.LastSelectedPlayerObj = Currentindex;
            BuyBtnCoins.gameObject.SetActive(false);
            BuyBtnXP.gameObject.SetActive(false);
            SelectedBtn.gameObject.SetActive(true);
            ApplyBtn.gameObject.SetActive(false);
            Toolbox.DB.prefs.Speed = SpeedAmt[Currentindex];
            Toolbox.DB.prefs.Stamina = StaminaAmt[Currentindex];
            Toolbox.DB.prefs.MaxCarryLimit = CapacityAmt[Currentindex];
            Toolbox.DB.prefs.Strength = StrengthAmt[Currentindex];
        }
        else
        {
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressNo);

            Toolbox.GameManager.InstantiatePopup_Message("You don't have enough money.");
        }
        Debug.Log(Toolbox.DB.prefs.LastSelectedPlayerObj);

    }

    public void BuyExpCharacter()
    {

        if (Price[Currentindex] <= Toolbox.DB.prefs.ExpPoints)
        {
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

            Toolbox.DB.prefs.CharactersUnlocked[Currentindex] = true;
            Toolbox.DB.prefs.LastSelectedPlayerObj = Currentindex;
            SetCoinsXP();

            BuyBtnCoins.gameObject.SetActive(false);
            BuyBtnXP.gameObject.SetActive(false);
            SelectedBtn.gameObject.SetActive(true);
            ApplyBtn.gameObject.SetActive(false);
            Toolbox.DB.prefs.Speed = SpeedAmt[Currentindex];
            Toolbox.DB.prefs.Stamina = StaminaAmt[Currentindex];
            Toolbox.DB.prefs.MaxCarryLimit = CapacityAmt[Currentindex];
            Toolbox.DB.prefs.Strength = StrengthAmt[Currentindex];
        }
        else
        {
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressNo);

            Toolbox.GameManager.InstantiatePopup_Message("You don't have enough XP.");
        }

    }

    public void ApplyCharacter()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

       
        Toolbox.DB.prefs.LastSelectedPlayerObj = Currentindex;
        BuyBtnCoins.gameObject.SetActive(false);
        BuyBtnXP.gameObject.SetActive(false);
        SelectedBtn.gameObject.SetActive(true);
        ApplyBtn.gameObject.SetActive(false);
        Toolbox.DB.prefs.Speed = SpeedAmt[Currentindex];
        Toolbox.DB.prefs.Stamina = StaminaAmt[Currentindex];
        Toolbox.DB.prefs.MaxCarryLimit = CapacityAmt[Currentindex];
        Toolbox.DB.prefs.Strength = StrengthAmt[Currentindex];

        FindObjectOfType<PlayerController>().playerSpeed= Toolbox.DB.prefs.Speed;
        FindObjectOfType<PlayerController>().stamina = Toolbox.DB.prefs.Stamina;

        Debug.Log(Currentindex);
        Debug.Log(Toolbox.DB.prefs.LastSelectedPlayerObj);

    }

    public void OnPress_Back()
    {
        FindObjectOfType<PlayerController>().shutdown = false;
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressNo);
        FindObjectOfType<PlayerController>().EnableCharacter(Toolbox.DB.prefs.LastSelectedPlayerObj);
        FindObjectOfType<PlayerController>().HUDSH.SetActive(true);
       // Toolbox.GameplayScript.player.GetComponent<PlayerController>().EnableCharacter(Toolbox.DB.prefs.LastSelectedPlayerObj);
        Destroy(parentob);
    }

}
