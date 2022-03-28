using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReader : MonoBehaviour
{ 
    public void playerReader()
    {
        if (Toolbox.DB.prefs.GameAudio == true)
        {
            Toolbox.Soundmanager.Running.Play();

        }
        else
        {
            Toolbox.Soundmanager.Running.Stop();

        }
    }
}
