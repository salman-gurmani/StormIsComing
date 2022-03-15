using UnityEngine;
using UnityEngine.UI;

public class HUDListner : MonoBehaviour {

    public Text timeTxt;

    public GameObject uiParent;
    public RectTransform resourcesParent;
    private int resourceIndex = 0;

    [Tooltip("Should be in the order of resources")]
    public Text[] resourcesTxts;

    void Awake() {
        
        Toolbox.Set_HudListner(this.GetComponent<HUDListner>());
        //AdsManager.instance.RequestBannerWithSpecs( Tapdaq.TDMBannerSize.TDMBannerStandard, Tapdaq.TDBannerPosition.Top);
    }

    public void DisableHUD() 
    {        
        uiParent.SetActive(false);
    }

    public void EnableHUD()
    {
        uiParent.SetActive(true);
    }

    public void EnableResource(int _index) {

        Transform resource = resourcesParent.GetChild(_index).transform;
        resource.gameObject.SetActive(true);
        UpdateResourceTxt(_index);
        resource.GetComponent<RectTransform>().position = resourcesParent.GetChild(resourceIndex).position;
        resourceIndex++;
    }

    public void UpdateResourceTxt(int _index) {

        resourcesTxts[_index].text = Toolbox.DB.prefs.ResourceAmount[_index].value.ToString();    
    }

    public void Press_Pause() {

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
        Toolbox.GameManager.Instantiate_PauseMenu();
    }

    public void Press_Camera()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
    }
    public void Press_ControlChange()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
    }

    public void Press_Settings()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPressYes);

        Toolbox.GameManager.Instantiate_SettingsMenu();
    }


}
