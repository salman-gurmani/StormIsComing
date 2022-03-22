using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    public GameObject[] effects;

    private void Start()
    {
        OffEffects();
    }
    public void OffEffects()
    {
        for(int i = 0; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(false);
            //effects[i].GetComponent<Animator>().enabled = true;
            //effects[i].GetComponent<Animator>().SetBool("on", false);
        }
    }
    public void OnEffects()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(true);
            //effects[i].GetComponent<Animator>().enabled = true;
            //effects[i].GetComponent<Animator>().SetBool("on", true);
        }
    }
}
