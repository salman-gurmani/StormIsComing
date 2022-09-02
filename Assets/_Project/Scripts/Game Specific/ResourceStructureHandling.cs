using DialogueEditor;
using UnityEngine;

public class ResourceStructureHandling : MonoBehaviour
{
    public AudioClip factory;
    public ResourceType requireType;
    public ResourceType productionType;

    public EffectHandler effectHandler;
    public GameObject effectPrefab;

    private float time = 0;
    private float convertResourceDelay = 0.3f;

    private LevelData curLevelData;
    private bool startProcessing = false;
    private int requirementResourceVal = 0;
    private int productionResourceVal = 0;

    private Transform player;
    public float gatherDistance = 2;

    public GameObject effects;
    private void Start()
    {
        requirementResourceVal = (int)requireType;
        productionResourceVal = (int)productionType;
        GetLevelData();

    }
    private void Update()
    {
        if (startProcessing) {

            time -= Time.deltaTime;

            if (time <= 0) {

                TransferResource();

                time = convertResourceDelay;
            }

            //effects.SetActive(true);

            //if (productionType == ResourceType.CEMENT_SACK)
            //{
            //    CementMachine();
            //}


            //effectHandler.OnEffects();
            ////effects.SetActive(false);
            //if (productionType == ResourceType.CEMENT_SACK)
            //{
            //    CementMachine();
            //}

            PlayerDistanceCheck();
        }
    }
    public void CementMachine()
    {
        transform.GetChild(0).GetComponent<Animator>().enabled = true;
        transform.GetChild(1).GetComponent<Animator>().enabled = true; 
    }
    public void CementMachineStop()
    {
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
        transform.GetChild(1).GetComponent<Animator>().enabled = false; 
    }
    private void PlayerDistanceCheck()
    {
        if (!player)
            player = Toolbox.GameplayScript.player.transform;

        if (Vector3.Distance(player.position, this.transform.position) >= gatherDistance)
        {
            StopProcessing();
        }        
    }

    public void GetLevelData()
    {
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
    }

    public void InitProcessing() {

        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value > 0) {

            if (Toolbox.DB.prefs.ResourceAmount[productionResourceVal].value >= Toolbox.DB.prefs.MaxCarryLimit)
            {
                if (!player)
                    player = Toolbox.GameplayScript.player.transform;

                player.GetComponent<PlayerController>().TryToEnableDialogue("Can't carry anymore " + Toolbox.DB.prefs.ResourceAmount[productionResourceVal].name);
                return;
            }

            startProcessing = true;

            effects.SetActive(true);

            if (productionType == ResourceType.CEMENT_SACK)
            {
                CementMachine();
            }


            effectHandler.OnEffects();
            //effects.SetActive(false);
            if (productionType == ResourceType.CEMENT_SACK)
            {
                CementMachine();
            }
        }
    }

    public void StopProcessing() {

        startProcessing = false;
        effectHandler.OffEffects();
        //  effects.SetActive(false);
        if (productionType == ResourceType.CEMENT_SACK)
        {
            CementMachineStop();
        }
    }

    private void TransferResource() {

        int resourceAmount = 1;

        if (Toolbox.DB.prefs.ResourceAmount[productionResourceVal].value >= Toolbox.DB.prefs.MaxCarryLimit)
        {
            if (!player)
                player = Toolbox.GameplayScript.player.transform;

            player.GetComponent<PlayerController>().TryToEnableDialogue("Can't carry anymore " + Toolbox.DB.prefs.ResourceAmount[productionResourceVal].name);
            return;
        }

        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value > (Toolbox.DB.prefs.ResourceGatherLevel + 1))
        {
            resourceAmount = (Toolbox.DB.prefs.ResourceGatherLevel + 1);
        }
        else
        {

            resourceAmount = Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value;
        }

        Toolbox.GameplayScript.player.SendResource(requireType, this.transform);
        InitEffect();

        Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value -= resourceAmount;
        Toolbox.HUDListner.UpdateResourceTxt(requirementResourceVal);

        for (int i = 0; i < resourceAmount; i++)
        {
            Toolbox.GameplayScript.player.RemoveResourceOnBack(requireType);
        }

        Toolbox.DB.prefs.ResourceAmount[productionResourceVal].value += resourceAmount;
        Toolbox.HUDListner.UpdateResourceTxt(productionResourceVal);
        for (int i = 0; i < resourceAmount; i++)
        {
            Toolbox.GameplayScript.player.AddResourceOnBack(productionType);
        }

        if (Toolbox.DB.prefs.ResourceAmount[requirementResourceVal].value <= 0) {

            StopProcessing();
        }

        if (Toolbox.DB.prefs.LastSelectedLevel == 1)
        {
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.WOOD_PLANK].value == 10)
            {
                ConversationManager.Instance.PressSelectedOption();
            }
        }
    }

    public void InitEffect()
    {
        if (effectPrefab)
        {
            Toolbox.Soundmanager.PlaySound(factory);
            GameObject obj = Instantiate(effectPrefab, this.transform.position, Quaternion.identity);
        }
    }
}