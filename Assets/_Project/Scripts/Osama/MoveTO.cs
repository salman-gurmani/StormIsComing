using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTO : MonoBehaviour
{
    public Transform target;
    public bool start = false;
    float distance;
    float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            MoveThere();
        }
    }
    public void MoveThere()
    {
        distance = Vector3.Distance(transform.GetChild(0).position, target.position);
       
       transform.position = Vector3.MoveTowards(transform.GetChild(0).transform.position, target.position, speed * Time.deltaTime);
        


        if(distance < 0.2f)
        {
            start = false;
            Destroy(gameObject);
        }
    }
}
