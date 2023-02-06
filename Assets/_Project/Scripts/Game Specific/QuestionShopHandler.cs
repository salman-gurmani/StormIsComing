using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DialogueEditor;

public class QuestionShopHandler : MonoBehaviour
{
    public List<QuestionAndAnswer> questionsAndAnswers;
    [Header("Adjustables")]
    [SerializeField] private int rewardAmountOfEachResource = 0;
    [SerializeField] private float coolDownTime = 30f;
    [Space]
    [Header("Assignables")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Canvas shopCanvas;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] public GameObject resultPanel;
    [SerializeField] private GameObject[] answerButtons;
    [SerializeField] GameObject popupButton;
    public bool[] checkHasResource;
    private int currentQnAIndex = 0;
    private bool isShopOpen = false;
    private bool onCoolDown = false;
    public bool IsFirstTry;

    private void Awake()
    {
        shopCanvas.worldCamera = Camera.main;
        IsFirstTry=true;
    }
    

    public void TryToOpenPopup()
    {
        if (isShopOpen)
            return;
        if (onCoolDown)
            return;

        SetPopupButton(true);
    }
    private void Start()
    {
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        //Debug.Log(curLevelData);
        for (int i = 0; i < curLevelData.hasResources.Length; i++)
        {
            //  checkToTransfer[i] = ((int)curLevelData.hasResources[i]);
            //   Debug.Log(((int)curLevelData.hasResources[i]));


            checkHasResource[(int)curLevelData.hasResources[i]] = true;

        }
    }
    public void OpenShop()
    {
        shopCanvas.gameObject.SetActive(true);
        GenerateNextQuestion();
        isShopOpen = true;
    }

    public void CorrectAnswer()
    {
        onCoolDown = true;
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.questionSuccess);
        questionsAndAnswers.Remove(questionsAndAnswers[currentQnAIndex]);
        questionPanel.SetActive(false);
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Correct!\nCome back in 30 seconds and try again!";
        GiveRewards();
        isShopOpen = false;
        SetPopupButton(false);
        IsFirstTry = true;

        if (Toolbox.DB.prefs.LastSelectedLevel == 3)
        {
            ConversationManager.Instance.PressSelectedOption();
            Toolbox.HUDListner.ConversationPanel.SetActive(true);
        }
    }

    public void WrongAnswer()
    {

        if (IsFirstTry)
        {
            Debug.Log("In Wrong Answer If");
            //Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.questionFailure);

            Toolbox.GameManager.Instantiate_SecondChanceMenu();
        }
        else
        {
            onCoolDown = true;
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.questionFailure);
            questionsAndAnswers.Remove(questionsAndAnswers[currentQnAIndex]);
            questionPanel.SetActive(false);
            resultPanel.SetActive(true);
            resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Wrong!\nCome back in 30 seconds and try again!";
            isShopOpen = false;
            SetPopupButton(false);
            IsFirstTry = true;
            if (Toolbox.DB.prefs.LastSelectedLevel == 3)
            {
                ConversationManager.Instance.PressSelectedOption();
                Toolbox.HUDListner.ConversationPanel.SetActive(true);
            }
        }

        //if (Toolbox.DB.prefs.LastSelectedLevel == 3)
        //{
        //    ConversationManager.Instance.PressSelectedOption();
        //    Toolbox.HUDListner.ConversationPanel.SetActive(true);
        //}
    }

    private void Update()
    {
        if (onCoolDown)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0)
            {
                onCoolDown = false;
                coolDownTime = 30f;
            }
        }
    }

    private void SetAnswers()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<QuestionShopAnswer>().isCorrect = false;
            answerButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questionsAndAnswers[currentQnAIndex].answers[i];  

            if (questionsAndAnswers[currentQnAIndex].correctAnswerIndex == i + 1)
            {
                answerButtons[i].GetComponent<QuestionShopAnswer>().isCorrect = true;
            }
        }
    }

    private void GenerateNextQuestion()
    {
        questionPanel.SetActive(true);
        resultPanel.SetActive(false);

        currentQnAIndex = UnityEngine.Random.Range(0, questionsAndAnswers.Count);
        questionText.text = questionsAndAnswers[currentQnAIndex].question;
        SetAnswers();
    }

    private void GiveRewards()
    {
        int j = 0;
        string path = Constants.PrefabFolderPath + Constants.LevelsScriptablesFolderPath + Toolbox.DB.prefs.LastSelectedMode.ToString() + "/" + Toolbox.DB.prefs.LastSelectedLevel.ToString();
        LevelData curLevelData = (LevelData)Resources.Load(path);
        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {

            if (checkHasResource[j])
            {
                int rewardAmountAllowed = rewardAmountOfEachResource;
                int spaceAvailabe = 5 - Toolbox.DB.prefs.ResourceAmount[(int)resourceType].value;

                if (spaceAvailabe <= rewardAmountAllowed)
                    rewardAmountAllowed = spaceAvailabe;

                Toolbox.DB.prefs.ResourceAmount[(int)resourceType].value += rewardAmountAllowed;
                Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);
                for (int i = 0; i < rewardAmountAllowed; i++)
                {
                    Toolbox.GameplayScript.player.AddResourceOnBack(resourceType);

                }
            }
                j++;
        }
    }
    public void SetPopupButton(bool _isActive)
    {
        popupButton.SetActive(_isActive);
    }
}