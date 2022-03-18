using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveResouce : MonoBehaviour
{
    PileHandler pileHandler;
    public TruckHandler truckHander;
    public ContainerHandler containerHandler;
    bool a = true;
    bool b = true;
    bool move = true;
    bool move2 = true;
    public float speed;
    float distance;
    public Transform[] materialArray;
    // Start is called before the first frame update
    private void OnEnable()
    {
       



        truckHander= transform.parent.transform.GetComponentInParent<TruckHandler>();
        pileHandler = truckHander.pilehandler;

        
        
        
        if (truckHander.hasSteel)
        {
            containerHandler = truckHander.steelContainer;
            materialArray = truckHander.pointofPileSteel;

        }
        if(truckHander.hasCement)
        {
            containerHandler = truckHander.cementContainer;
            materialArray = truckHander.pointofPileCement;
        }
        if (truckHander.hasBrick)
        {
            containerHandler = truckHander.brickContainer;
            materialArray = truckHander.pointofPileBrick;
        }
        if (truckHander.hasStone)
        {
            containerHandler = truckHander.stoneContainer;
            materialArray = truckHander.pointofPileStone;
        }
        if (truckHander.hasWoodLog)
        {
            containerHandler = truckHander.woodContainer;
            materialArray = truckHander.pointofPileLog;
        }




    }
    void Enable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       




        distance = Vector3.Distance(containerHandler.pointofMatrial[truckHander.counter2].position, transform.position);
        if (distance < 0.1f)
        {
            distance = -1f;
        }
        if (move)
        {


            transform.position = Vector3.MoveTowards(transform.position, containerHandler.pointofMatrial[truckHander.counter2].position, speed * Time.deltaTime);
        }
        if (truckHander.counter2 > materialArray.Length - 1)
        {
            
        }
        else
        {
            Debug.Log(distance);

            if (distance <= 0)
            {

                move = false;
                if (a)
                {
                    MaterialMovement();
                    a = false;
                }

            }

        }
        if(!move2)
        {
            truckHander.speed = 5f;
        }
        if (b)
        {
            CheckIfFUll();
            b = false;
        }
        if (pileHandler.CheckifStock())
        {

            move2 = false;
        }

    }
       

    public void MaterialMovement()
    {
        
        truckHander.counter2++;
        transform.parent = containerHandler.pointofMatrial[truckHander.counter2-1];
        if (truckHander.counter2 > materialArray.Length - 1)
        {

        }
        else
        {
            if (move2)
            {
                materialArray[truckHander.counter2].GetComponent<MoveResouce>().enabled = true;
            }

        }
      
        transform.GetComponent<MoveResouce>().enabled = false;
        
    }
    public void CheckIfFUll()
    {
        if (truckHander.hasSteel)
        {
            pileHandler.amountSteel++;
        }
        if (truckHander.hasCement)
        {
            pileHandler.amountCement++;
        }
        if (truckHander.hasBrick)
        {
            pileHandler.amountBrick++;
        }
        if (truckHander.hasStone)
        {
            pileHandler.amountStone++;
        }
        if (truckHander.hasWoodLog)
        {
            pileHandler.amountWoodLog++;
        }
    }
    
}
