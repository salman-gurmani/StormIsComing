using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question and Answer", menuName = ("Question Shop/Question and Answer"))]
public class QuestionAndAnswer : ScriptableObject
{
    [TextArea(5, 10)]
    public string question;
    public string[] answers;
    [Range(1, 4)]
    public int correctAnswerIndex;
}