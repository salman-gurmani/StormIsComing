using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    public GameObject[] effects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OffEffects()
    {
        for(int i = 0; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(true);
            effects[i].GetComponent<Animator>().enabled = true;
            effects[i].GetComponent<Animator>().SetBool("on", false);
        }
    }
    public void OnEffects()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(true);
            effects[i].GetComponent<Animator>().enabled = true;
            effects[i].GetComponent<Animator>().SetBool("on", true);
        }
    }
}
