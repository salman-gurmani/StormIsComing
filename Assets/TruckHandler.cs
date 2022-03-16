using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckHandler : MonoBehaviour
{
    public SteelRodHandler steelHandler;
    public Transform[] pointofPile;
    public GameObject steelRod;
    public float speed = 5f;
    public Transform[] pointsToStop;
    public bool hasWoodLog;
    public bool hasStone;
    public bool hasBrick;
    public bool hasSteel;
    public bool hasCement;
    bool isSteel = false;
    // Start is called before the first frame update
    void Start()
    {
        TruckMovementCheck();
        StockUp();
    }
    public void StockUp()
    {
       
    }

    public void StockOnPile()
    {
        for(int i = 0; i<pointofPile.Length; i++)
        {
            pointofPile[i].parent = steelHandler.pointofSteelRods[i];
            //  pointofPile[i].position = steelHandler.pointofSteelRods[i].position * Time.deltaTime;
            pointofPile[i].position = Vector3.MoveTowards(pointofPile[i].position, steelHandler.pointofSteelRods[i].position,3.5f * Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isSteel)
        {
            StockOnPile();
        }



        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        
     
    }
    public void TruckMovementCheck()
    {
        if (hasSteel)
        {
           
            steelRod.gameObject.SetActive(true);
           // transform.GetComponent<Animator>().SetBool("hasSteel", true);
            pointsToStop[4].gameObject.SetActive(true);
        //    Invoke("DoneResourcing1", 5f);
        }
        if (hasCement)
        {
           // transform.GetComponent<Animator>().SetBool("hasCement", true);
            pointsToStop[3].gameObject.SetActive(true);
            //  Invoke("DoneResourcing2", 5f);
        }
        if (hasBrick)
        {
          //  transform.GetComponent<Animator>().SetBool("hasBrick", true);
            pointsToStop[2].gameObject.SetActive(true);
            //    Invoke("DoneResourcing3", 5f);
        }
        if (hasStone)
        {
          //  transform.GetComponent<Animator>().SetBool("hasStone", true);
            pointsToStop[1].gameObject.SetActive(true);
            //    Invoke("DoneResourcing4", 5f);
        }
        if (hasWoodLog)
        {
           // transform.GetComponent<Animator>().SetBool("hasWood", true);
            pointsToStop[0].gameObject.SetActive(true);
            //    Invoke("DoneResourcing5", 5f);
        }


    }

    //public void DoneResourcing1()
    //{
    //    transform.GetComponent<Animator>().SetBool("hasSteel", false);
    //} 
    //public void DoneResourcing2()
    //{
    //    transform.GetComponent<Animator>().SetBool("hasCement", false);
    //} 
    //public void DoneResourcing3()
    //{
    //    transform.GetComponent<Animator>().SetBool("hasBrick", false);
    //} 
    //public void DoneResourcing4()
    //{
    //    transform.GetComponent<Animator>().SetBool("hasStone", false);
    //} 
    //public void DoneResourcing5()
    //{
    //    transform.GetComponent<Animator>().SetBool("hasWood", false);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("stop"))
        {
            isSteel = true;
            other.gameObject.SetActive(false);
            speed = 0f;
            Invoke("RetuenSpeed", 5f);
        }
    }
    public void RetuenSpeed()
    {
        speed = 5f;
    }
}
