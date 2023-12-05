using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class QuestionsSO : ScriptableObject
{
    public string QuestionInfo;

    public string[] answers;

    public int correctAnswer;
}
