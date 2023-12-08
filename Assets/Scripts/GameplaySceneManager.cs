using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class GameplaySceneManager : MonoBehaviour
{
    [SerializeField] private QuestionsSO[] questions;
    [SerializeField] private Text questionText;
    [SerializeField] private Button[] buttons;

    [SerializeField] private UIManager uIManager;

    [SerializeField] private Sprite[] players;

    [SerializeField] private Text TimeText;

    [SerializeField] private Text scorePlayer;

    [SerializeField] private Text scoreEnemy1;

    [SerializeField] private Text scoreEnemy2;

    [SerializeField] private GameObject Rocket;

    [SerializeField] private ParticleSystem ExplosionParticle;

    private int questionIndex = -1;
    private int timer = 15;
    private bool playerAnswered = false;
    private bool enemiesAnswered1 = false;
    private bool enemiesAnswered2 = false;

    private int enemy1score;
    private int enemy2score;

    // private bool playerCorrect =false;
    // private bool enemy1Correct =false;
    // private bool enemy2Correct =false;
    private int answeringTime1;
    private int answeringTime2;
    private int GameStarter = 0;

    // public void Start()
    // {
    //     StartCoroutine(SendNuke());
    // }
    public void SetQuestion()
    {
        GameStarter++;
        Debug.Log(GameStarter);
        playerAnswered = false;
        enemiesAnswered1 = false;
        enemiesAnswered2 = false;
        // enemy1Correct = false;
        // enemy2Correct = false;
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
            buttons[i].GetComponent<AnswerScript>().isCorrect = false;
        }
       questionIndex++;
            questionText.text = questions[questionIndex].QuestionInfo;
            timer = 15;
            StartCoroutine(Countdown());
            // StartCoroutine(EnemyAI());
            for(int i = 0; i < buttons.Length; i++){
            buttons[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = questions[questionIndex].answers[i];
            for(int x = 1; x <= 3; x++){
            buttons[i].transform.GetChild(x).gameObject.SetActive(false);
            }
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
        button.transform.GetChild(1).gameObject.SetActive(true);
        playerAnswered = true;
        if(iscorect && uIManager.state != BattleState.PLAYERTURN && uIManager.state != BattleState.WON){
        // playerCorrect = true;
        scorePlayer.text = (int.Parse(scorePlayer.text) + 300).ToString();  
        }
        else if(iscorect && uIManager.state == BattleState.PLAYERTURN)
        {
            Debug.Log("Attack is successful!");
            StartCoroutine(uIManager.AttackIsSucess());
        }
        else if(uIManager.state == BattleState.PLAYERTURN){
            Debug.Log("unsucessful attack");
            StartCoroutine(uIManager.AttackIsNotSucess());
        }
        else if(iscorect && uIManager.state == BattleState.WON)
        {
            StartCoroutine(uIManager.DefenceIsSucess());
            scorePlayer.text = (int.Parse(scorePlayer.text) + 300).ToString();
            Debug.Log(scorePlayer);
        }
        else if(uIManager.state == BattleState.WON)
        {
            StartCoroutine(uIManager.DefenceIsNotSucess());
        }
        // else{
        //     // playerCorrect =false;
        // }
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }
    public IEnumerator Countdown()
    {
        answeringTime1 = Random.Range(1,16);
        answeringTime2 = Random.Range(1,16);
        while(timer > 0)
        {
            if(playerAnswered && enemiesAnswered1 && enemiesAnswered2)
            {
                break;   
            }
            else if(playerAnswered && (uIManager.state == BattleState.PLAYERTURN || uIManager.state == BattleState.WON))
            {
                break; 
            }
            timer = timer -1;
            if(uIManager.state != BattleState.PLAYERTURN || uIManager.state != BattleState.WON){
            if(answeringTime1 == timer)
            {
                int enemyAnswer = Random.Range(1,4);
                buttons[enemyAnswer].transform.GetChild(2).GetComponent<Image>().sprite = players[1];
                buttons[enemyAnswer].transform.GetChild(2).gameObject.SetActive(true);
                if(buttons[enemyAnswer].GetComponent<AnswerScript>().isCorrect)
                {
                    int enemyscore = int.Parse(scoreEnemy1.text);
                    int actualenemyscore = enemyscore + 300;
                    scoreEnemy1.text = actualenemyscore.ToString();
                }
                enemiesAnswered1 = true;
            }
            if(answeringTime2 == timer)
            {
                int enemyAnswer = Random.Range(1,4);
                buttons[enemyAnswer].transform.GetChild(3).GetComponent<Image>().sprite = players[2];
                buttons[enemyAnswer].transform.GetChild(3).gameObject.SetActive(true);
                if(buttons[enemyAnswer].GetComponent<AnswerScript>().isCorrect)
                {
                    int enemyscore = int.Parse(scoreEnemy1.text);
                    int actualenemyscore = enemyscore +300;
                    scoreEnemy2.text = actualenemyscore.ToString();
                }
                enemiesAnswered2 = true;
            }
            }
            TimeText.text = timer.ToString();
            yield return new WaitForSeconds(1f);
            
        }
        if(GameStarter == 4){
            uIManager.StartActualGame();
        }
        else if(uIManager.state == BattleState.PLAYERTURN || uIManager.state == BattleState.WON )
        {

        }
        else{
        SetQuestion();
        }
        timer = 15;
    }
    public IEnumerator SendNuke(Transform NukePos)
    {
        Transform closerToThis = GameManager.instance.enemy1.transform;
        // SEND THE NUKE
    //    Vector3 Destination = new Vector3(3,4,-15);
       float step = 0.4F;
       GameObject Traveller;
       if(Vector3.Distance(NukePos.position, closerToThis.position) < 1f ){
       Traveller = Instantiate(Rocket, new Vector3(-5f, 1f ,-15f), Quaternion.Euler(0f,0f, -70f));
       enemy1score = int.Parse(scoreEnemy1.text) -300;
       scoreEnemy1.text = enemy1score.ToString();
       }
       else{
        Traveller = Instantiate(Rocket, new Vector3(-5f, 1f ,-15f), Quaternion.Euler(0f,0f, -120f));
        enemy2score = int.Parse(scoreEnemy2.text) -300;
       scoreEnemy2.text = enemy2score.ToString();
       }
        while(Traveller.transform.position != NukePos.position){
        Traveller.transform.position = Vector3.MoveTowards(Traveller.transform.position, NukePos.position, step);
        yield return new WaitForSeconds(0.05f);
        }
        Instantiate(ExplosionParticle,Traveller.transform.position, Quaternion.identity);
        Destroy(Traveller);
        if(int.Parse(scoreEnemy1.text)== 0){
       Destroy(GameManager.instance.enemy1);
       StartCoroutine(uIManager.Enemy1Death());
       }
       if(int.Parse(scoreEnemy2.text) == 0){
       Destroy(GameManager.instance.enemy2);
       StartCoroutine(uIManager.Enemy2Death());
       }
       yield return new WaitForSeconds(1f);

    }

}
