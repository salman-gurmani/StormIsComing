using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public GameObject btn;
    public SafeHouseController safeHouse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AcceptJob()
    {
        Toolbox.DB.prefs.JobAccepted = true;
        Toolbox.GameManager.InstantiatePopup_Message("Good Job You have accepted the Contract Now go to Storage to get the resources you need");
    }
    public void RejectJob()
    {
        
    }
}
