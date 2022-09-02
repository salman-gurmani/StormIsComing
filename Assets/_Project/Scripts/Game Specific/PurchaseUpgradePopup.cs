using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseUpgradePopup : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Image buyButtonCoinImage;
    [SerializeField] private Text buyButtonText;

    public void Start()
    {
        CheckForCoins();
        buyButtonText.text = Toolbox.DB.prefs.CarryLimitUpgradePrice.ToString();
    }

    public void CheckForCoins()
    {
        if (Toolbox.DB.prefs.GoldCoins < Toolbox.DB.prefs.CarryLimitUpgradePrice)
        {
            DisableBuyButton();
        }
    }

    public void PurchaseCarryLimit()
    {
        Toolbox.DB.prefs.MaxCarryLimit += 5;
        Toolbox.HUDListner.UpdateAllResourceText();
        Toolbox.DB.prefs.CarryLimitUpgradePrice *= 2;
    }

    private void DisableBuyButton()
    {
        buyButton.interactable = false;
        buyButton.GetComponent<Image>().color = Color.gray;
        buyButtonCoinImage.color = Color.gray;
        buyButtonText.color = Color.gray;
    }
}