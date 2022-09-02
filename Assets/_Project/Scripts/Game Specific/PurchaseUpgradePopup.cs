using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseUpgradePopup : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Image buyButtonCoinImage;
    [SerializeField] private Text buyButtonText;

    private int upgradePrice = 200;

    public void Start()
    {
        CheckForCoins();
    }

    public void CheckForCoins()
    {
        if (Toolbox.DB.prefs.GoldCoins < upgradePrice)
        {
            DisableBuyButton();
        }
    }

    public void PurchaseCarryLimit()
    {
        Toolbox.DB.prefs.MaxCarryLimit += 5;
        Toolbox.HUDListner.UpdateAllResourceText();
    }

    private void DisableBuyButton()
    {
        buyButton.interactable = false;
        buyButton.GetComponent<Image>().color = Color.gray;
        buyButtonCoinImage.color = Color.gray;
        buyButtonText.color = Color.gray;
    }
}