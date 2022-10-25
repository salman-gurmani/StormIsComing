using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartShopController : MonoBehaviour
{

    public GameObject[] AllPlayers;
    public int[] StrengthAmt;
    public int[] Price;
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
        Toolbox.DB.prefs.StrengthCart = StrengthAmt[Currentindex];
        Debug.Log(Toolbox.DB.prefs.LastSelectedCartObj);

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
        //AllPlayers[0].SetActive(true);
        StrengthTxt.text = "Lvl. " + StrengthAmt[0].ToString();
        BuyBtnCoins.gameObject.SetActive(false);
        if (Toolbox.DB.prefs.LastSelectedCartObj == 0)
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

        if (Currentindex > AllPlayers.Length - 1)
        {
            Currentindex = 0;
        }
        Toolbox.DB.prefs.StrengthCart = StrengthAmt[Currentindex];
        AllPlayers[Currentindex].SetActive(true);
        StrengthTxt.text = "Lvl. " + StrengthAmt[Currentindex].ToString();

        if (Toolbox.DB.prefs.CartsUnlocked[Currentindex] == true)
        {
            BuyBtnCoins.gameObject.SetActive(false);
            BuyBtnXP.gameObject.SetActive(false);

            if (Toolbox.DB.prefs.LastSelectedCartObj == Currentindex)
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
            ////if (Toolbox.DB.prefs.CharactersUnlocked[3] || Toolbox.DB.prefs.CharactersUnlocked[6] || Toolbox.DB.prefs.CharactersUnlocked[9])
            //if (Currentindex == 3 || Currentindex == 6 || Currentindex == 9)
            //{
            //    BuyBtnCoins.gameObject.SetActive(false);
            //    BuyBtnXP.gameObject.SetActive(true);
            //    pricetxtCoins.text = Price[Currentindex].ToString();
            //    pricetxtXP.text = Price[Currentindex].ToString();
            //    SelectedBtn.gameObject.SetActive(false);
            //    ApplyBtn.gameObject.SetActive(false);
            //}
            //else
            //{
                BuyBtnCoins.gameObject.SetActive(true);
                BuyBtnXP.gameObject.SetActive(false);
                pricetxtCoins.text = Price[Currentindex].ToString();
               pricetxtXP.text = Price[Currentindex].ToString();
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(false);
            //}
        }
    }

    public void PreviousBtn()
    {
       
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

        AllPlayers[Currentindex].SetActive(false);

        Currentindex--;

        if (Currentindex < 0)
        {
            Currentindex = AllPlayers.Length - 1;
             
        }
        Toolbox.DB.prefs.StrengthCart = StrengthAmt[Currentindex];
        AllPlayers[Currentindex].SetActive(true);
        StrengthTxt.text = "Lvl. " + StrengthAmt[Currentindex].ToString();

        if (Toolbox.DB.prefs.CartsUnlocked[Currentindex] == true)
        {
            BuyBtnCoins.gameObject.SetActive(false);
            BuyBtnXP.gameObject.SetActive(false);

            if (Toolbox.DB.prefs.LastSelectedCartObj == Currentindex)
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
            //if (Currentindex == 3 || Currentindex == 6 || Currentindex == 9)
            //{
            //    BuyBtnCoins.gameObject.SetActive(false);
            //    BuyBtnXP.gameObject.SetActive(true);
            //    SelectedBtn.gameObject.SetActive(false);
            //    ApplyBtn.gameObject.SetActive(false);

            //    pricetxtCoins.text = Price[Currentindex].ToString();
            //    pricetxtXP.text = Price[Currentindex].ToString();
            //}
            //else
            //{
                BuyBtnCoins.gameObject.SetActive(true);
                BuyBtnXP.gameObject.SetActive(false);
                pricetxtCoins.text = Price[Currentindex].ToString();
                pricetxtXP.text = Price[Currentindex].ToString();
                SelectedBtn.gameObject.SetActive(false);
                ApplyBtn.gameObject.SetActive(false);
            //}
        }

        Debug.Log(Toolbox.DB.prefs.LastSelectedCartObj);
    }

    public void BuyCoinsCharacter()
    {

        if (Toolbox.DB.prefs.StrengthCart <= Toolbox.DB.prefs.Strength)
        {

            if (Price[Currentindex] <= Toolbox.DB.prefs.GoldCoins)
            {
                Toolbox.DB.prefs.CartAvailable = true;
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

                Toolbox.DB.prefs.GoldCoins -= Price[Currentindex];
                SetCoinsXP();
                Toolbox.DB.prefs.CartsUnlocked[Currentindex] = true;
                Toolbox.DB.prefs.LastSelectedCartObj = Currentindex;
                BuyBtnCoins.gameObject.SetActive(false);
                BuyBtnXP.gameObject.SetActive(false);
                SelectedBtn.gameObject.SetActive(true);
                ApplyBtn.gameObject.SetActive(false);
                Toolbox.DB.prefs.StrengthCart = StrengthAmt[Currentindex];
            }
            else
            {
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressNo);

                Toolbox.GameManager.InstantiatePopup_Message("You don't have enough money.");
            }
            Debug.Log(Toolbox.DB.prefs.LastSelectedCartObj);
        }
        else
        {
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressNo);

            Toolbox.GameManager.InstantiatePopup_Message("Please select Higher Strength player.");
        }
    }

    public void BuyExpCharacter()
    {

      
            if (Price[Currentindex] <= Toolbox.DB.prefs.ExpPoints)
            {
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);

                Toolbox.DB.prefs.CartsUnlocked[Currentindex] = true;
                Toolbox.DB.prefs.LastSelectedCartObj = Currentindex;
                SetCoinsXP();

                BuyBtnCoins.gameObject.SetActive(false);
                BuyBtnXP.gameObject.SetActive(false);
                SelectedBtn.gameObject.SetActive(true);
                ApplyBtn.gameObject.SetActive(false);

                Toolbox.DB.prefs.StrengthCart = StrengthAmt[Currentindex];
            }

            else
            {
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressNo);

                Toolbox.GameManager.InstantiatePopup_Message("You don't have enough XP.");
            }
        
       
    }

    public void ApplyCharacter()
    {
        Toolbox.DB.prefs.CartAvailable = true;
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);


        Toolbox.DB.prefs.LastSelectedCartObj = Currentindex;
        BuyBtnCoins.gameObject.SetActive(false);
        BuyBtnXP.gameObject.SetActive(false);
        SelectedBtn.gameObject.SetActive(true);
        ApplyBtn.gameObject.SetActive(false);
       
        Toolbox.DB.prefs.StrengthCart = StrengthAmt[Currentindex];

        FindObjectOfType<PlayerController>().playerSpeed = Toolbox.DB.prefs.Speed;
        FindObjectOfType<PlayerController>().stamina = Toolbox.DB.prefs.Stamina;

        Debug.Log(Currentindex);
        Debug.Log(Toolbox.DB.prefs.LastSelectedCartObj);

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
