using UnityEngine;
using UnityEngine.UI;

public class HUDListner : MonoBehaviour {

    public Text timeTxt;

    public GameObject uiParent;

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

    private void Start()
    { 

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    run = true;
        //else if (Input.GetKeyUp(KeyCode.Space))
        //    run = false;
    }

    public void SetLvlTxt(string _str) {

        
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
