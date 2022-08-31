using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntractiveButton : MonoBehaviour
{
    Vector3 touchPosWorld;
    public GameObject pnl;
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
                    print(hit.collider.name);
                    pnl.SetActive(true);
                }
                if (hit.collider.CompareTag("QuestionShopUI"))
                {
                    QuestionShopHandler riddleShopHandler = FindObjectOfType<QuestionShopHandler>();
                    riddleShopHandler.TryToOpenShop();
                }
            }
        }
    }






}

