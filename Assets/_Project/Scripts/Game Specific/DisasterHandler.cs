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
        Toolbox.GameplayScript.camShake.ShakeCamera(5, 4);
    }

    public void Tsunami()
    {
        Toolbox.GameplayScript.camShake.ShakeCamera(1, 4);
        Instantiate(tsunamiObj, transform);
    }
    public void Volcano()
    {
        Toolbox.GameplayScript.camShake.ShakeCamera(2, 4);
        Instantiate(volcanoObj, transform);
    }
    public void Tornado()
    {
        Instantiate(tornadoObj, transform);
    }
    public void Storm()
    {
        Toolbox.GameplayScript.camShake.ShakeCamera(1, 4);
        Instantiate(stormObj, transform);
    }
}
