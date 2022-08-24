using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReader : MonoBehaviour
{
    public GameObject[] changeTool;
    public void playerReader()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.running);
    }
    public void woodAxe()
    {
        changeTool[0].SetActive(true);
        changeTool[1].SetActive(false);
        changeTool[2].SetActive(false);
    }
    public void pickAxe()
    {
        changeTool[0].SetActive(false);
        changeTool[1].SetActive(false);
        changeTool[2].SetActive(true);
    }
    public void pickShovel()
    {
        changeTool[0].SetActive(false);
        changeTool[1].SetActive(true);
        changeTool[2].SetActive(false);
    }
}
