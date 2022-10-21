using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    public GameObject targ;
    public float a = 15, b = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targ.transform.position, a * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targ.transform.rotation, b * Time.deltaTime);
    }
}
