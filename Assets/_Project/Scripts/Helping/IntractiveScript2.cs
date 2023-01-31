using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntractiveScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { }

        Ray ray;
        RaycastHit hit;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.collider.CompareTag("Cart"))
                {
                    Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
                    FindObjectOfType<PlayerController>().OnPress_StoreCart();
                    //  pnl.SetActive(true);
                    // Toolbox.GameManager.Instantiate_StoreCart();
                }
              
            }
        }
    }
}
