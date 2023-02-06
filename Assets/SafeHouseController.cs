using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeHouseController : MonoBehaviour
{
    public GameObject hud;
   // public bool jobAccepted = false;
    // Start is called before the first frame update
    void Start()
    {
        Toolbox.GameManager.InstantiatePopup_MessageBar("Safehouse");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CheckJobAccpeted()
    {
        if (Toolbox.DB.prefs.JobAccepted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
