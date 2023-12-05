using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameplaySceneManager : MonoBehaviour
{
    [SerializeField] private QuestionsSO[] questions;
    [SerializeField] private Text questionText;
    [SerializeField] private Button[] buttons;

    [SerializeField] private UIManager uIManager;

    [SerializeField] private Sprite[] players;


    private int questionIndex = -1;
    public void SetQuestion()
    {
       questionIndex++;
            questionText.text = questions[questionIndex].QuestionInfo;
            for(int i = 0; i < buttons.Length; i++){
            buttons[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = questions[questionIndex].answers[i];
            
            if(questions[questionIndex].correctAnswer == i)
            {
                buttons[i].GetComponent<AnswerScript>().isCorrect = true;
                
                // buttons[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            }
    }
    public void CheckStatus(bool iscorect, Button button)
    {
        button.transform.GetChild(1).GetComponent<Image>().sprite = players[0];
        if(iscorect){
        uIManager.InGameQuestions();
        }
        else{

        }
    }

}
