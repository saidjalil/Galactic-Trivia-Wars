using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;

    [SerializeField] private GameplaySceneManager gsManager;

    private Button button;
    

    public void Answer()
    {
        button = this.GetComponent<Button>();
        Debug.Log(button);
        gsManager.CheckStatus(isCorrect, button);

    }
}
