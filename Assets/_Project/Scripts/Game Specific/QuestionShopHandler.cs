using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject[] answerButtons;

    private int currentQnAIndex = 0;
    private bool isShopOpen = false;
    private bool onCoolDown = false;

    private void Awake()
    {
        shopCanvas.worldCamera = Camera.main;
    }

    public void TryToOpenShop()
    {
        if (isShopOpen)
            return;
        if (onCoolDown)
            return;

        onCoolDown = true;
        shopCanvas.gameObject.SetActive(true);
        GenerateNextQuestion();
        isShopOpen = true;
    }

    public void CorrectAnswer()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.questionSuccess);
        questionsAndAnswers.Remove(questionsAndAnswers[currentQnAIndex]);
        questionPanel.SetActive(false);
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Correct!\nCome back in 30 seconds and try again!";
        GiveRewards();
        isShopOpen = false;
    }

    public void WrongAnswer()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.questionFailure);
        questionsAndAnswers.Remove(questionsAndAnswers[currentQnAIndex]);
        questionPanel.SetActive(false);
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Wrong!\nCome back in 30 seconds and try again!";
        isShopOpen = false;
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
        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {
            Toolbox.DB.prefs.ResourceAmount[(int)resourceType].value += rewardAmountOfEachResource;
            Toolbox.HUDListner.UpdateResourceTxt((int)resourceType);
            for (int i = 0; i < rewardAmountOfEachResource; i++)
            {
                Toolbox.GameplayScript.player.AddResourceOnBack(resourceType);
            }
        }
    }
}