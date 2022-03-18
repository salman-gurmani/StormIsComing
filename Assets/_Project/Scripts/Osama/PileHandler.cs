using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileHandler : MonoBehaviour
{
    public GameObject truck;
    public Transform truckSpot;
    public int count = 0;
    int count2 = 0;
    bool checkIfEmpty = false;
    public int amountSteel;
    public int amountCement;
    public int amountBrick;
    public int amountStone;
    public int amountWoodLog;





    // Start is called before the first frame update
    void Awake()
    {
        truck.SetActive(true);
      
        StockUpLoop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(amountSteel < truck.GetComponent<TruckHandler>().amountSteel || amountCement < truck.GetComponent<TruckHandler>().amountCement || amountBrick < truck.GetComponent<TruckHandler>().amountBrick || amountStone < truck.GetComponent<TruckHandler>().amountStone || amountWoodLog < truck.GetComponent<TruckHandler>().amountWoodLog )
        {
            if(checkIfEmpty)
            ReActiveTruck();
           
        }
    }

    public void ReActiveTruck()
    {
        checkIfEmpty = false;
        StockUpLoop();
        truck.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Truck"))
        {
            truck.GetComponent<TruckHandler>().steelRod.SetActive(false);
            truck.GetComponent<TruckHandler>().cementSack.SetActive(false);
            truck.GetComponent<TruckHandler>().brickStack.SetActive(false);
            truck.GetComponent<TruckHandler>().stoneStack.SetActive(false);
            truck.GetComponent<TruckHandler>().logStack.SetActive(false);
            if (amountSteel >= truck.GetComponent<TruckHandler>().amountSteel && amountCement >= truck.GetComponent<TruckHandler>().amountCement && amountBrick >= truck.GetComponent<TruckHandler>().amountBrick && amountStone >= truck.GetComponent<TruckHandler>().amountStone && amountWoodLog >= truck.GetComponent<TruckHandler>().amountWoodLog)
            {
                truck.transform.position = truckSpot.transform.position;
                truck.SetActive(false);
                checkIfEmpty = true;
            }
            else
            {
               
                truck.SetActive(false);
                truck.transform.position = truckSpot.transform.position;
                StockUpLoop();
                truck.SetActive(true);
            }
        }
    }
    public void Stockup2()
    {
        if(amountSteel >= truck.GetComponent<TruckHandler>().amountSteel)
        {
            truck.GetComponent<TruckHandler>().hasSteel = false;
        }
        else
        {
            truck.GetComponent<TruckHandler>().hasSteel = true;
        }
        if (amountCement >= truck.GetComponent<TruckHandler>().amountCement)
        {
            truck.GetComponent<TruckHandler>().hasCement = false;
        }
        else
        {
            truck.GetComponent<TruckHandler>().hasCement = true;
        }
        if (amountBrick >= truck.GetComponent<TruckHandler>().amountBrick)
        {
            truck.GetComponent<TruckHandler>().hasBrick = false;
        }
        else
        {
            truck.GetComponent<TruckHandler>().hasBrick = true;
        }
        if (amountStone >= truck.GetComponent<TruckHandler>().amountStone)
        {
            truck.GetComponent<TruckHandler>().hasStone = false;
        }
        else
        {
            truck.GetComponent<TruckHandler>().hasStone = true;
        }
        if (amountWoodLog >= truck.GetComponent<TruckHandler>().amountWoodLog)
        {
            truck.GetComponent<TruckHandler>().hasWoodLog = false;
        }
        else
        {
            truck.GetComponent<TruckHandler>().hasWoodLog = true;
        }
        truck.SetActive(true);
    }
    public void StockUpLoop()
    {
        truck.GetComponent<TruckHandler>().hasSteel = false;
        truck.GetComponent<TruckHandler>().hasCement = false;
        truck.GetComponent<TruckHandler>().hasBrick = false;
        truck.GetComponent<TruckHandler>().hasStone = false;
        truck.GetComponent<TruckHandler>().hasWoodLog = false;
        if (amountSteel < truck.GetComponent<TruckHandler>().amountSteel)
        {
            
                truck.GetComponent<TruckHandler>().hasSteel = true;
            
        }
        
      else  if (amountCement < truck.GetComponent<TruckHandler>().amountCement)
        {
          
                truck.GetComponent<TruckHandler>().hasCement = true;
            
        }
        
       else if ( amountBrick < truck.GetComponent<TruckHandler>().amountBrick)
        {
            
                truck.GetComponent<TruckHandler>().hasBrick = true;
            
        }
        
      else  if ( amountStone < truck.GetComponent<TruckHandler>().amountStone)
        {
            
               truck.GetComponent<TruckHandler>().hasStone = true;
            
        }
       
       else if ( amountWoodLog < truck.GetComponent<TruckHandler>().amountWoodLog)
        {
           
                truck.GetComponent<TruckHandler>().hasWoodLog = true;
            
        }
        else
        {
            
        }

        if(count == 4)
        {
            count = 0;
        }
        else
        {
            count++;
        }

       



    }

    public bool CheckifStock()
    {
        if (truck.GetComponent<TruckHandler>().hasSteel)
        {
            if (amountSteel >= truck.GetComponent<TruckHandler>().amountSteel)
            {
            //    truck.GetComponent<TruckHandler>().steelRod.SetActive(false);
                return true;
            }
        }
        if (truck.GetComponent<TruckHandler>().hasCement)
        {
            if (amountCement >= truck.GetComponent<TruckHandler>().amountCement)
            {
            //    truck.GetComponent<TruckHandler>().cementSack.SetActive(false);
                return true;
            }
        }
        if (truck.GetComponent<TruckHandler>().hasBrick)
        {
            if (amountBrick >= truck.GetComponent<TruckHandler>().amountBrick)
            {
             //   truck.GetComponent<TruckHandler>().brickStack.SetActive(false);
                return true;
            }
        }
        if (truck.GetComponent<TruckHandler>().hasStone)
        {
            if (amountStone >= truck.GetComponent<TruckHandler>().amountStone )
            {
            //    truck.GetComponent<TruckHandler>().stoneStack.SetActive(false);
                return true;
            }
        }
        if (truck.GetComponent<TruckHandler>().hasWoodLog)
        {
            if (amountWoodLog >= truck.GetComponent<TruckHandler>().amountWoodLog)
            {
            //    truck.GetComponent<TruckHandler>().logStack.SetActive(false);
                return true;
            }
        }

        return false;
    }





}
