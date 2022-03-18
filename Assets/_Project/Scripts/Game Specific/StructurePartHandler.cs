using UnityEngine;

public class StructurePartHandler : MonoBehaviour
{
    public ResourceType requireType;
    public int resourceRequired = 5;
    private Animator anim;

    private float time = 0;
    private float convertResourceDelay = 0.05f;

    private bool startProcessing = false;
    private int requirementResourceVal = 0;

    private Transform player;
    public float buildDistance = 4;
    private float distance = 0;

    private bool built = false;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        requirementResourceVal = (int)requireType;
    }

    private void Update()
    {
        if (built)
            return;

        PlayerDistanceCheck();
        ProcessHandling();
    }

    private void ProcessHandling()
    {
        if (startProcessing)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                TransferResource();
                time = convertResourceDelay;
            }
        }
    }

    private void PlayerDistanceCheck()
    {
        if (!player)
            player = Toolbox.GameplayScript.player.transform;

        distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= buildDistance)
        {
            startProcessing = true;
        }
        else {

            startProcessing = false;
        }
    }

    public void InitProcessing()
    {
        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value > 0)
        {
            startProcessing = true;
        }
    }

    private void TransferResource()
    {

        int resourceAmount = 0;

        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value <= 0)
            return;

        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value > (Toolbox.DB.prefs.ResourceGatherLevel + 1))
        {
            resourceAmount = (Toolbox.DB.prefs.ResourceGatherLevel + 1);
        }
        else
        {
            resourceAmount = Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value;
        }

        if (resourceAmount > resourceRequired)
            resourceAmount = resourceRequired;

        Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value -= resourceAmount;
        Toolbox.HUDListner.UpdateResourceTxt(requirementResourceVal);

        resourceRequired -= resourceAmount;

        if (resourceRequired <= 0) {

            Build();
        }
    }

    void Build() {

        if (built)
            return;

        built = true;

        anim.SetTrigger("Build");
        //this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponentInParent<StructureHandler>().HousePartComplete();
        this.enabled = false;
    }
}
