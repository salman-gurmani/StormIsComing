using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCuttingTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (TutorialTargetController.Instance.currentTargetIndex<6)
        {
            
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.WOOD_LOG].value < 10) 
            {
                TutorialTargetController.Instance.MoveToNextTarget();
                TutorialAIHandler.Instance.MoveToNextTarget();
            }
        }
    }
}
