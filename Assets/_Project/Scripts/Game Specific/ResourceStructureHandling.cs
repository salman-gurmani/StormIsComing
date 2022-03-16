using UnityEngine;

public class ResourceStructureHandling : MonoBehaviour
{
    public ResourceType requireType;
    public ResourceType productionType;

    private float time = 0;
    private float convertResourceDelay = 0.5f;

    private bool startProcessing = false;
    private int requirementResourceVal = 0;
    private int productionResourceVal = 0;

    private Transform player;
    public float gatherDistance = 2;

    private void Start()
    {
        requirementResourceVal = (int)requireType;
        productionResourceVal = (int)productionType;

    }
    private void Update()
    {
        if (startProcessing) {

            time -= Time.deltaTime;

            if (time <= 0) {

                TransferResource();

                time = convertResourceDelay;
            }

            PlayerDistanceCheck();
        }
    }

    private void PlayerDistanceCheck()
    {
        if (!player)
            player = Toolbox.GameplayScript.player.transform;

        if (Vector3.Distance(player.position, this.transform.position) >= gatherDistance)
        {
            startProcessing = false;
        }        
    }

    public void InitProcessing() {

        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value > 0) {

            startProcessing = true;
        }
    }

    private void TransferResource() {

        int resourceAmount = 0;

        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value > (Toolbox.DB.prefs.ResourceGatherLevel + 1))
        {
            resourceAmount = (Toolbox.DB.prefs.ResourceGatherLevel + 1);
        }
        else {

            resourceAmount = Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value;
        }


        Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value -= resourceAmount;
        Toolbox.HUDListner.UpdateResourceTxt(requirementResourceVal);

        Toolbox.DB.prefs.ResourceAmount[productionResourceVal].value += resourceAmount;
        Toolbox.HUDListner.UpdateResourceTxt(productionResourceVal);
    }
}
