using UnityEngine;

public class DisasterHandler : MonoBehaviour
{
    public GameObject stormObj;
    public GameObject volcanoObj;
    public GameObject lightningOBJ;
    public GameObject tsunamiObj;
    public GameObject tornadoObj;

    public void EarthQuakes()
    {
        Toolbox.GameplayScript.FinalDecisionHandling(5);
        Toolbox.GameplayScript.camShake.ShakeCamera(5, 4);
        Toolbox.GameplayScript.StartBuildingDistruction();
    }

    public void Tsunami()
    {
        //Toolbox.GameplayScript.camShake.ShakeCamera(3, 4);
        //Instantiate(tsunamiObj, transform);
        Toolbox.GameplayScript.FinalDecisionHandling(14);
        tsunamiObj.SetActive(true);

    }
    public void Volcano()
    {
        //Toolbox.GameplayScript.camShake.ShakeCamera(2, 4);
        //Instantiate(volcanoObj, transform);

        Toolbox.GameplayScript.FinalDecisionHandling(13);
        volcanoObj.SetActive(true);
    }
    public void Tornado()
    {
        Toolbox.GameplayScript.FinalDecisionHandling(15);
        tornadoObj.SetActive(true);
        Toolbox.GameplayScript.camBrain.gameObject.SetActive(false);

    }
    public void Storm()
    {
        Toolbox.GameplayScript.camShake.ShakeCamera(1, 4);
        Instantiate(stormObj, transform);
    }
}