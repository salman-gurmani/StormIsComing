using UnityEngine;
using DialogueEditor;

public class ResourceHandler : MonoBehaviour
{
    public static ResourceHandler Instance;
    public ResourceType type;
    private BoxCollider collider; 
    [Tooltip("This many times user will get resource from this Object")]
    public int defaultResouceQuantity ;
    [SerializeField] private int curResouceQuantity ;
    public int resourceVal = 0;

    [Space(10)]
    public Transform [] effectInitPoints;
    private int curEffectPointIndex = 0;
    public GameObject effectPrefab;
    public Animator anim;

    [SerializeField]private float time = 0;
    public float respawnTime = 5;
    public AudioClip GatherSound;
    public float gatherDistance = 5;
    public GameObject TreeStump;
    public GameObject LeftRocks;
    public GameObject LeftCement;
    public GameObject LeftMud;
    public GameObject LeftIron;
    //public GameObject EmptySandContainer;

    private string resourceName;
    private bool distanceCheck = false;
    private bool isRespawning = false;
    private Transform player;
    public bool IsTutorialDone;

    private void Start()
    {
       Instance = this;
        player = Toolbox.GameplayScript.player.transform;
        resourceVal = (int)type;
        collider = this.GetComponent<BoxCollider>();
        resourceName = type.ToString();
    }

    private void Update()
    {
        if (isRespawning) {

            time -= Time.deltaTime;

            if (time <= 0) {

                ResetResource();
                SetRespawnStatus(false);
            }
        }

        PlayerDistanceCheck();
    }

    private void PlayerDistanceCheck()
    {
        if (distanceCheck) { 
            if (!player)
                player = Toolbox.GameplayScript.player.transform;

            if (Vector3.Distance(player.position, this.transform.position) >= gatherDistance) {

                Toolbox.GameplayScript.player.RemoveResource(this);
                DistanceCheckStatus(false);
            }
        }
    }

    public void ResetResource()
    {
        curResouceQuantity = defaultResouceQuantity;
        UpdateModelLevel(defaultResouceQuantity);
    }

    void SetRespawnStatus(bool _val) {

        collider.enabled = !_val;
        isRespawning = _val;
    }

    public void DistanceCheckStatus(bool _val) {

        distanceCheck = _val;
    }

    public void GetResource() {

        if (isRespawning)
            return;

        Toolbox.DB.prefs.ResourceAmount[resourceVal].value += (Toolbox.DB.prefs.ResourceGatherLevel + 1);
        //Toolbox.DB.prefs.CarryLimit = Toolbox.DB.prefs.CarryLimit + 1;
        if (type == ResourceType.WOOD_LOG || type == ResourceType.STONE_BLOCK || type == ResourceType.MUD_BLOCK || type == ResourceType.IRON_BLOCK || type == ResourceType.CEMENT_BLOCK)
        {
            Toolbox.DB.prefs.GoldCoins += 10;
            Toolbox.GameManager.Instantiate_RewardAnim();
            FindObjectOfType<HUDListner>().UpdateTxt();
        }
        //Toolbox.DB.prefs.MaxCarryLimit = Toolbox.DB.prefs.CarryLimit;
        if (Toolbox.DB.prefs.CarryLimit > 0)
        {
            for (int i = 0; i < (Toolbox.DB.prefs.ResourceGatherLevel + 1); i++)
            {
                Toolbox.GameplayScript.player.AddResourceOnBack(type);
            }

        }

        if (Toolbox.DB.prefs.LastSelectedLevel == 0)
        {
            
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.WOOD_LOG].value == 10)
            {
                
                TutorialTargetController.Instance.currentTargetIndex = 6;
                ConversationManager.Instance.PressSelectedOption();
                Toolbox.HUDListner.ConversationPanel.SetActive(true);
                
            }
        }
        else if (Toolbox.DB.prefs.LastSelectedLevel == 1)
        {
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.WOOD_LOG].value == 10)
            {
                ConversationManager.Instance.PressSelectedOption();
                Toolbox.HUDListner.ConversationPanel.SetActive(true);
            }
        }
        else if (Toolbox.DB.prefs.LastSelectedLevel == 4)
        {
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.WOOD_LOG].value >= 5)
            {
                ConversationManager.Instance.PressSelectedOption();
                Toolbox.HUDListner.ConversationPanel.SetActive(true);
            }
        }
        else if (Toolbox.DB.prefs.LastSelectedLevel == 6)
        {
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.CEMENT_BLOCK].value == 10)
            {

                ConversationManager.Instance.PressSelectedOption();
                Toolbox.HUDListner.ConversationPanel.SetActive(true);
            }
        }
        else if (Toolbox.DB.prefs.LastSelectedLevel == 7)
        {
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.MUD_BLOCK].value == 10)
            {
                ConversationManager.Instance.PressSelectedOption();
                Toolbox.HUDListner.ConversationPanel.SetActive(true);

            }
        }
        else if (Toolbox.DB.prefs.LastSelectedLevel == 12)
        {
            if (Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.IRON_BLOCK].value == 10)
            {
                ConversationManager.Instance.PressSelectedOption();
                Toolbox.HUDListner.ConversationPanel.SetActive(true);

            }
        }

        Toolbox.HUDListner.UpdateResourceTxt(resourceVal);
        curResouceQuantity--;
        UpdateModelLevel(curResouceQuantity);
        //Debug.LogError(Toolbox.DB.prefs.ResourceAmount[resourceVal].name /*+ " " + Toolbox.DB.prefs.ResourceAmount[resourceVal].value*/ + "_Added");
        InitEffect();
        Toolbox.Soundmanager.PlaySound(GatherSound);

        if (curResouceQuantity <= 0) {

            //Debug.LogError(Toolbox.DB.prefs.ResourceAmount[resourceVal].name + " Respawning");
            Toolbox.GameplayScript.player.RemoveResource(this);

            if(resourceName=="WOOD_LOG")
            {
                Instantiate(TreeStump, this.gameObject.transform.position, Quaternion.identity);
            }
            else if (resourceName == "STONE_BLOCK")
            {
                Instantiate(LeftRocks, this.gameObject.transform.position, transform.rotation);
            }
            //else if (resourceName == "CEMENT_BLOCK")
            //{
            //    Instantiate(LeftCement, this.gameObject.transform.position, transform.rotation);
            //} 
            //else if (resourceName == "MUD_BLOCK")
            //{
            //    Instantiate(LeftMud, this.gameObject.transform.position, transform.rotation);
            //} 
            //else if (resourceName == "IRON_BLOCK")
            //{
            //    Instantiate(LeftIron, this.gameObject.transform.position, transform.rotation);
            //}



            Destroy(this.gameObject);


            //time = respawnTime;
            //DistanceCheckStatus(false);

            //SetRespawnStatus(true);
        }
    }

    public void InitEffect() {

        if (effectPrefab) {

            GameObject obj = Instantiate(effectPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void UpdateModelLevel(int _val) {

        anim.SetInteger("State", _val);


        //if(anim.GetInteger("State")==0)
        //{
            
        //    this.gameObject.GetComponent<MapMarker>().isActive = false;
        //}
        //else if(anim.GetInteger("State")>0)
        //{
        //    this.gameObject.GetComponent<MapMarker>().isActive = true;

        //}
    }
}