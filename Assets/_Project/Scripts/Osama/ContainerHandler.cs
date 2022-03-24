using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerHandler : MonoBehaviour
{
    public bool giveResourcee = false;
    public GameObject effectResource;
    private Transform player;
    private float distance;
    public float resourceDistance = 4;
    public Transform[] pointofMatrial;
    public int resourceVal;
    public int resourceNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resourceVal = transform.gameObject.GetComponent<ResourceAreaHandler>().resourcesValue;
        if (!giveResourcee)
        {
            PlayerDistanceCheck();
        }
        distance = Vector3.Distance(player.position, this.transform.position);
        if (distance > resourceDistance)
        {
            giveResourcee = false;
            
        }
    }

    private void PlayerDistanceCheck()
    {
       
        
        if (!player)
            player = Toolbox.GameplayScript.player.transform;

        distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= resourceDistance)
        {
            if (resourceVal > 0)
            {
                transform.gameObject.GetComponent<ResourceAreaHandler>().amountTxt.ToString();
                giveResourcee = true;
                GiveResource();
                ResourceToDB();
                DestroyResourceFromPile();
            }
        }
        else
        {
            giveResourcee = false;

        }
      
    }
    public void GiveResource()
    {
        Instantiate(effectResource, transform.position, Quaternion.identity);

    }


    public void ResourceToDB()
    {
        Toolbox.DB.prefs.ResourceAmount[resourceNumber].value += resourceVal;
        transform.gameObject.GetComponent<ResourceAreaHandler>().resourcesValue = 0;
    }


    public void DestroyResourceFromPile()
    {
        for(int i = 0; i < resourceVal; i++)
        {
            if(pointofMatrial[i].childCount > 0)
            {
                Destroy(pointofMatrial[i].GetChild(0).gameObject);
            }
        }
    }
    
}
