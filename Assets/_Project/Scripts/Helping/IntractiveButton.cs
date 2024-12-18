using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueEditor
{
    public class IntractiveButton : MonoBehaviour
    {
        Vector3 touchPosWorld;
        public GameObject pnl;
        public string LastVisited;
        public static int LastIndex;
        //Change me to change the touch phase used.
        TouchPhase touchPhase = TouchPhase.Ended;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        //void Update()
        //{
        //    if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        //    {
        //        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //        RaycastHit raycastHit;
        //        if (Physics.Raycast(raycast, out raycastHit))
        //        {
        //            Debug.Log("Something Hit");
        //            //if (raycastHit.collider.name == "Soccer")
        //            //{
        //            //    Debug.Log("Soccer Ball clicked");
        //            //}

        //            //OR with Tag

        //            if (raycastHit.collider.CompareTag("TradeShopUI"))
        //            {
        //                Debug.Log("Soccer Ball clicked");
        //            }
        //        }
        //    }
        //}

        Ray ray;
        RaycastHit hit;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("TradeShopUI"))
                    {
                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        FindObjectOfType<HUDListner>().DisableHUD();
                        pnl.SetActive(true);


                       
                    }
                    if (hit.collider.CompareTag("MissionBaseBtn"))
                    {
                        FindObjectOfType<PlayerController>().HUDSH.SetActive(false);
                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        pnl.SetActive(true);

                        if (LastIndex == 0)
                        {
                            ConversationManager.Instance.PressSelectedOption();
                            HUDListner2.instance.ShowConvPanel();
                            LastIndex = 1;
                            // LastVisited = "MissionBase";
                        }
                    }
                    if (hit.collider.CompareTag("Travel"))
                    {
                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        //FindObjectOfType<StorageController>().travelpnl.SetActive(true);
                        //Toolbox.GameManager.Instantiate_Loading();
                        //Toolbox.GameManager.Instantiate_Blackout();
                        if (Toolbox.DB.prefs.JobAccepted && Toolbox.DB.prefs.StorageAccepted)
                        {
                            Toolbox.GameManager.LoadScene(2, true, 0);
                            LastIndex = 0;
                        }
                        else
                            Toolbox.GameManager.InstantiatePopup_Message("You need to select Mission Base and Resource Storage both before Traveling");


                    }
                    //if (hit.collider.CompareTag("Cart"))
                    //{
                    //    Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                    //    FindObjectOfType<PlayerController>().OnPress_StoreCart();
                    //  //  pnl.SetActive(true);
                    //   // Toolbox.GameManager.Instantiate_StoreCart();
                    //}
                    if (hit.collider.CompareTag("Shop"))
                    {
                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        //  pnl.SetActive(true);
                        FindObjectOfType<PlayerController>().OnPress_Store();
                    }
                    if (hit.collider.CompareTag("ResourceStorageBtn"))
                    {

                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        if (Toolbox.DB.prefs.JobAccepted)
                        {
                            FindObjectOfType<PlayerController>().HUDSH.SetActive(false);
                            FindObjectOfType<StorageController>().UpdateStorage();
                            //  Debug.Log(FindObjectOfType<StorageController>());
                            pnl.SetActive(true);

                            if (LastIndex == 1)
                            {
                                ConversationManager.Instance.PressSelectedOption();
                                HUDListner2.instance.ShowConvPanel();
                                LastIndex = 2;
                                //LastVisited = "StorageBase";
                            }
                        }
                        else
                        {
                            Toolbox.GameManager.InstantiatePopup_Message("Kindly Accept the Job first");
                        }
                    }
                    if (hit.collider.CompareTag("QuestionShopUI"))
                    {
                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        QuestionShopHandler riddleShopHandler = FindObjectOfType<QuestionShopHandler>();
                        riddleShopHandler.OpenShop();
                        FindObjectOfType<HUDListner>().DisableHUD();
                    }
                    if (hit.collider.CompareTag("WizardShopUI"))
                    {
                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        FindObjectOfType<WizardShopController>().ShowADS();

                    }
                    if (hit.collider.CompareTag("EnergyBtn"))
                    {
                        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                        Toolbox.GameManager.Instantiate_Energy();
                    }
                }
            }
        }






    }
}
