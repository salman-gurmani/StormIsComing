using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public bool cartAvailable = false;
    bool checkMoved = false;
    public float variableCutting = 0.05f;
    float temp;
    float energy;
    public SpriteRenderer staminaIMG;
    public float stamina = 1000f;
    int coinAndChest = 0;
    float progress;
    public CharacterController controller; 
    public Animator anim;
    private LevelData curLevelData;
    public GameObject[] models;
    private int index = 0;
    private bool run = false;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] public float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;
    public bool drunk = false; 
    public GameObject hitEffect; 
    public GameObject hitEffect2; 
    public GameObject [] resourcesObjs;
    public PlayerResourceHandler [] resourcesHandler;
    public Transform playerParent;
    //public PlayerResources[] resources;
    public GameObject [] resourceSendEffect;
    [Space(10)]
    public List<ResourceHandler> resourceInTrigger;
    [SerializeField] private bool isGathering = false;
    public GameObject[] carts;
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI dialogueText;
    float time = 0;
    float gatherDelay = 0.8f;
    int resourceAvailableInLevel = 0;
    private bool isPlayerDialogueActive = false;
    public GameObject DustEffect;
    private bool movementDisabled = false;
    public GameObject HUDSH;

    private void Start()
    {
        index = Toolbox.DB.prefs.LastSelectedPlayerObj;
        EnableCharacter(index);

        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);

        if (Toolbox.DB.prefs.LastSelectedPlayerObj == 0)
        {
            Toolbox.DB.prefs.Speed = 3f;
            Toolbox.DB.prefs.Stamina = 50f;
            Toolbox.DB.prefs.MaxCarryLimit = 15;
            Toolbox.DB.prefs.Strength = 1;
        }

        playerSpeed = Toolbox.DB.prefs.Speed;
        stamina = Toolbox.DB.prefs.Stamina;
    }

    public void DisablePlayerMovement(bool _isDisabled)
    {
        movementDisabled = _isDisabled;
    }

   public void EnableCharacter(int index) {

        foreach (var item in models)
        {
            item.gameObject.SetActive(false);
        }
        
        models[index].SetActive(true);
        anim = models[index].GetComponent<Animator>();
        if(cartAvailable)
        {
            carts[0].SetActive(true);
        }
    }
    public void UpdateResource()
    {

        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {          
            Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);
            
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.CEMENT_BLOCK].value; i++)
        {
            AddResourceOnBack(ResourceType.CEMENT_BLOCK);
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.CEMENT_SACK].value; i++)
        {
            AddResourceOnBack(ResourceType.CEMENT_SACK);
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.IRON_BLOCK].value; i++)
        {
            AddResourceOnBack(ResourceType.IRON_BLOCK);
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.MUD_BLOCK].value; i++)
        {
            AddResourceOnBack(ResourceType.MUD_BLOCK);
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.MUD_BRICK].value; i++)
        {
            AddResourceOnBack(ResourceType.MUD_BRICK);
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.STEEL_ROD].value; i++)
        {
            AddResourceOnBack(ResourceType.STEEL_ROD);
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.STONE_BLOCK].value; i++)
        {
            AddResourceOnBack(ResourceType.STONE_BLOCK);
        }
        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.WOOD_LOG].value; i++)
        {
            AddResourceOnBack(ResourceType.WOOD_LOG);
        }

        for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount[(int)ResourceType.WOOD_PLANK].value; i++)
        {
            AddResourceOnBack(ResourceType.WOOD_PLANK);
        }
       
        
    }
    public void UpdateResourceMinus()
    {
        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {
           
            Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);
          

        }
        for (int i = resourcesHandler[0].index; i > -1; i--)
        {
            RemoveResourceOnBack(ResourceType.CEMENT_BLOCK);
            RemoveResourceOnBack(ResourceType.CEMENT_SACK);
            RemoveResourceOnBack(ResourceType.IRON_BLOCK);
            RemoveResourceOnBack(ResourceType.MUD_BLOCK);
            RemoveResourceOnBack(ResourceType.MUD_BRICK);
            RemoveResourceOnBack(ResourceType.STEEL_ROD);
            RemoveResourceOnBack(ResourceType.STONE_BLOCK);
            RemoveResourceOnBack(ResourceType.WOOD_LOG);
            RemoveResourceOnBack(ResourceType.WOOD_PLANK);
        }
    }
    void Update()
    {
        PlayerMovement();
        ResourceGatherHandling();
        GatherTimeHandling();
    }

    private void GatherTimeHandling()
    {
        if (time <= 0)
            return;
        

        time -= Time.deltaTime;

        if (time <= 0){

            GatherRequestHandling();
        }
    }

    void PlayerMovement() {
        if (movementDisabled)
            return;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), 0, CnControls.CnInputManager.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            stamina = stamina - variableCutting;
            gameObject.transform.forward = move;
            temp = 10 - stamina;
            if (energy <= 360)
            {
                energy = (360 * temp) / 100;
                staminaIMG.GetComponent<SpriteRenderer>().material.SetFloat("_Arc1", energy);
            }
            if (energy > 0)
            {
                staminaIMG.gameObject.SetActive(true);
            }
            else
            {
                staminaIMG.gameObject.SetActive(false);
            }


            if (energy > 60 && energy < 120 && variableCutting <= 0.09f)
                {

                    variableCutting = 0.09f;
                    playerSpeed = 3.3f;
                }
                if (energy > 120 && energy < 180 && variableCutting <= 0.1f)
                {
                    variableCutting = 0.1f;
                    playerSpeed = 2.8f;
                }
                if (energy > 180 && energy < 240 && variableCutting <= 0.2f)
                {
                    variableCutting = 0.2f;
                    playerSpeed = 2.2f;
                }
                if (energy > 240 && energy < 320 && variableCutting <= 0.3f)
                {
                    variableCutting = 0.3f;
                    playerSpeed = 1.6f;
                }
                if (energy >= 360 && variableCutting <= 0.35f)
                {
                    variableCutting = 0.35f;
                    playerSpeed = 1f;
                    drunk = true;
                
            }
        }
        else
        {


            
            stamina = 100f;
            if (energy > 0)
            {
                energy -= 350 * Time.deltaTime;
                staminaIMG.GetComponent<SpriteRenderer>().material.SetFloat("_Arc1", energy);
            }
        }


        

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        //LevelData curLevelData = (LevelData)Resources.Load(path);
        //if (curLevelData.environmentNumber==3)
        //{
            
        //    DustEffect.GetComponent<ParticleSystem>().Play();
        //}

        UpdateMovement(move);
    }


    private void UpdateMovement(Vector3 _mov) {
        if (!drunk)
        {
            if (anim.GetBool("Walk"))
            {
                anim.SetBool("Walk", false);
            }
            if (_mov.x != 0 || _mov.z != 0)
            {
                run = true;
                CreateDust();
                //Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.running);
            }
            else
            {

                run = false;
                DustEffect.GetComponent<ParticleSystem>().Stop();

            }

            anim.SetBool("Run", run);
        }
        else
        {
            if (anim.GetBool("Run"))
            {
                anim.SetBool("Run", false);
            }
            if (_mov.x != 0 || _mov.z != 0)
            {
                run = true;
                CreateDust();
                //Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.running);
            }
            else
            {

                //anim.SetBool("Run", run);
                run = false;

                DustEffect.GetComponent<ParticleSystem>().Stop();

            }

            anim.SetBool("Walk", run);
        

    }


}

    public void CreateDust()
    {
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        if (curLevelData.environmentNumber == 3)
        {

            DustEffect.GetComponent<ParticleSystem>().Play();
        }
    }
    public void ReturnBackToNormal()
    {
        playerSpeed = 4f;
        drunk = false;
        hitEffect2.SetActive(false);
    }
    private void ResourceGatherHandling()
    {
        if (!isGathering && resourceInTrigger.Count > 0)
        {
            if (Toolbox.DB.prefs.CarryLimit >= Toolbox.DB.prefs.MaxCarryLimit)
            { 
                  TryToEnableDialogue("Can't carry anymore " + Toolbox.DB.prefs.ResourceAmount[resourceInTrigger[0].resourceVal].name);
                  return; 
            }
            

            isGathering = true;
            switch(resourceInTrigger[0].type)
            {
                case ResourceType.WOOD_LOG:
                    FindObjectOfType<PlayerReader>().woodAxe();
                    anim.SetTrigger("Attack");
                    break;

                case ResourceType.STONE_BLOCK:
                case ResourceType.IRON_BLOCK:
                    FindObjectOfType<PlayerReader>().pickAxe();
                    //ChangeTool.SetActive(false);
                    anim.SetTrigger("Attack 3");
                    break;

                case ResourceType.MUD_BLOCK:
                case ResourceType.CEMENT_BLOCK:
                    FindObjectOfType<PlayerReader>().pickShovel();
                    //ChangeTool.SetActive(false);
                    anim.SetTrigger("Attack 2");
                    break;

            }

            time = gatherDelay;
        }
    }

    public void GatherRequestHandling() {
        
        if (resourceInTrigger.Count > 0)
        {
            for (int i = 0; i < resourceInTrigger.Count; i++)
            {
                if (resourceInTrigger[i])
                    resourceInTrigger[i].GetResource();
            }
        }

        isGathering = false;

    }

    public void AddResource(ResourceHandler _handler)
    {
        resourceInTrigger.Add(_handler);
        _handler.DistanceCheckStatus(true);
    }

    public void RemoveResource(ResourceHandler _handler) {

        resourceInTrigger.Remove(_handler);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * 10;

    }

    bool isResourceHandlerAvailable(ResourceHandler _handler) {

        for (int i = 0; i < resourceInTrigger.Count; i++)
        {
            if (resourceInTrigger[i] == _handler) {

                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Resource":
                ResourceHandler handler = other.GetComponent<ResourceHandler>();
                if (!isResourceHandlerAvailable(handler))
                    AddResource(handler);
                break;

            case "Traffic":
                Debug.Log("PlayerCrashed In Car");
                playerSpeed = 0.5f;
                drunk = true;
                hitEffect2.SetActive(true);
                Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.carCrash);
                Invoke("ReturnBackToNormal", 3);
                break;

            case "ResourceStructure":
                other.GetComponent<ResourceStructureHandling>().InitProcessing();
                break;

            case "Lift":
                this.transform.parent = other.transform;
                break;
            case "Boom":
                FindObjectOfType<TravelBooth>().Boom.GetComponent<Animator>().enabled = true;
                break;

            case "Coin":
                other.gameObject.GetComponent<MapMarker>().isActive = false;
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.coinsSound);
                Toolbox.DB.prefs.GoldCoins = Toolbox.DB.prefs.GoldCoins + 1;
                FindObjectOfType<HUDListner>().UpdateTxt();
                other.gameObject.SetActive(false);
                coinAndChest++;
                progress = ((float)coinAndChest / (float)Toolbox.GameplayScript.levelsManager.CurLevelData.noOfBonusThings);
                Toolbox.HUDListner.SetProgressBarFill(progress);


                break;

            case "Chest":
                other.gameObject.GetComponent<MapMarker>().isActive = false;
                Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.chestSound);
                Toolbox.DB.prefs.GoldCoins = Toolbox.DB.prefs.GoldCoins + 10;
                Toolbox.GameManager.Instantiate_RewardAnim();
                FindObjectOfType<HUDListner>().UpdateTxt();
                other.gameObject.SetActive(false);
                coinAndChest++;
                progress = ((float)coinAndChest / (float)Toolbox.GameplayScript.levelsManager.CurLevelData.noOfBonusThings);
                Toolbox.HUDListner.SetProgressBarFill(progress);
                break;

            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.LogError("Trigger = " + other.gameObject.tag.ToString());
        switch (other.tag)
        {
            case "Travel":
                other.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case "WizardShop":
                if (AdsManager.instance.unity_isInitialized || AdsManager.instance.admob_isInitialized)
                {
                    other.GetComponentInParent<TradeShopController>().btn.SetActive(false);
                    FindObjectOfType<WizardShopController>().UpdateIMG();
                    FindObjectOfType<WizardShopController>().aminShopkepper.SetBool("magic", false);
                }
                else
                { 
                    FindObjectOfType<WizardShopController>().btnNOAdsVailable.SetActive(false);
                }
                break;

            case "MarketPlace":

                other.GetComponentInParent<TradeShopController>().btn.SetActive(false);

                break;
            case "Resource":
                RemoveResource(other.GetComponent<ResourceHandler>());
                break;
            case "TradeShop":
                other.GetComponentInParent<TradeShopController>().btn.SetActive(false);
                break;
            case "Lift":
                this.transform.parent = playerParent;
                break;
            case "QuestionShop":
                other.GetComponentInParent<QuestionShopHandler>().SetPopupButton(false);
              
                break;

            case "ResourceStorage":

                other.GetComponentInParent<StorageController>().btn.SetActive(false);
                break;
            case "MissionBase":

                other.GetComponentInParent<MissionController>().btn.SetActive(false);

                break;
                
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Travel":
                other.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "QuestionShop":
                other.GetComponentInParent<QuestionShopHandler>().TryToOpenPopup();
                break;
            case "ResourceStorage":

                other.GetComponentInParent<StorageController>().btn.SetActive(true);

                break;
            case "MissionBase":

                other.GetComponentInParent<MissionController>().btn.SetActive(true);

                break;
            case "WizardShop":
                if (AdsManager.instance.unity_isInitialized || AdsManager.instance.admob_isInitialized)
                {
                    other.GetComponentInParent<TradeShopController>().btn.SetActive(true);
                    FindObjectOfType<WizardShopController>().UpdateIMG();
                    FindObjectOfType<WizardShopController>().aminShopkepper.SetBool("magic", true);
                }
                else
                {
                    FindObjectOfType<WizardShopController>().btnNOAdsVailable.SetActive(true);
                }
                break;
            case "MarketPlace":

                other.GetComponentInParent<TradeShopController>().btn.SetActive(true);

                break;
            case "TradeShop":
                if (Toolbox.DB.prefs.ResourceAmount[0].value >= 5 || Toolbox.DB.prefs.ResourceAmount[1].value >= 5 || Toolbox.DB.prefs.ResourceAmount[2].value >= 5 || Toolbox.DB.prefs.ResourceAmount[4].value >= 5 || Toolbox.DB.prefs.ResourceAmount[6].value >= 5)
                {
                    other.GetComponentInParent<TradeShopController>().btn.SetActive(true);
                } 
                //other.GetComponentInParent<TradeShopController>().btn.SetActive(true);
                break;
        }
        //Debug.LogError("Trigger = " + other.gameObject.tag.ToString());

        //switch (other.tag)
        //{
        //    case "Resource":

        //        Debug.LogError("Trigger = " + other.gameObject.tag.ToString());

        //        break;

        //    default:
        //        break;
        //}

    }

    //public void UpdateResourcesOnBack() {

    //    for (int i = 0; i < Toolbox.DB.prefs.ResourceAmount.Length; i++)
    //    {
    //        if (Toolbox.DB.prefs.ResourceAmount[i].value >= 3) {

    //            for (int j = 0; j < 3; j++)
    //            {
    //                resources[i].part[j].gameObject.SetActive(true);
    //            }
    //        }
    //    }
    //}



    public void InitResourcesValOnBack() {

        resourceAvailableInLevel = Toolbox.GameplayScript.levelsManager.CurLevelData.hasResources.Length;
        for (int i = 0; i < resourceAvailableInLevel; i++)
        {
            ResourceType rType = Toolbox.GameplayScript.levelsManager.CurLevelData.hasResources[i];
            resourcesHandler[i].SetResource(Toolbox.GameplayScript.levelsManager.CurLevelData.hasResources[i], resourcesObjs[(int)rType]);
          
        }
    }
    public void AddResourceOnBack(ResourceType _type)
    {
        for (int i = 0; i < resourceAvailableInLevel; i++)
        {
            if (_type == resourcesHandler[i].type) {
                resourcesHandler[i].Add();
            }
        }
    }

    public void RemoveResourceOnBack(ResourceType _type)
    {
        for (int i = 0; i < resourceAvailableInLevel; i++)
        {
            if (_type == resourcesHandler[i].type)
            {
                resourcesHandler[i].Remove();
            }
        }
    }

    public void SendResource(ResourceType _type, Transform _point) {

        GameObject sendEffect = Instantiate(resourceSendEffect[(int)_type], this.transform.position /*+ new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z)*/, Quaternion.identity);
        sendEffect.GetComponent<MoveTO>().EnableMovement(_point);
    }

    public void TryToEnableDialogue(string dialogueString)
    {
        if (isPlayerDialogueActive)
            return;

        isPlayerDialogueActive = true;
        dialogueCanvas.gameObject.SetActive(true);
        dialogueText.text = dialogueString;
        Invoke(nameof(DisableDialogue), 4f);
    }

    private void DisableDialogue()
    {
        isPlayerDialogueActive = false;
        dialogueCanvas.gameObject.SetActive(false);
        dialogueText.text = "";
    }
    public void OnPress_Store()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.Select);
        Toolbox.GameManager.Instantiate_StoreSkin();
    }
}