using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionShopAnswer : MonoBehaviour
{
    public bool isCorrect = false;

    [SerializeField] private QuestionShopHandler questionShopHandler;

    public void CheckAnswer()
    {
        if (isCorrect)
        {
            questionShopHandler.CorrectAnswer();
        }
        else
        {
            questionShopHandler.WrongAnswer();
        }
    }
}