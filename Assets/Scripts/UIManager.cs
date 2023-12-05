using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject planetUI;
    [SerializeField] private GameObject planetTxt;
    [SerializeField] private GameObject startButton;

    [SerializeField] private GameObject[] PlayersUI;
    [SerializeField] private GameObject PlayerUI;


    [SerializeField] private GameObject TextBetweenScenes;

    [SerializeField] private GameObject questionUI;

    [SerializeField] private GameplaySceneManager GSManager;

    [SerializeField] private Text scorePlayer;

    [SerializeField] private Text scoreEnemy1;

    [SerializeField] private Text scoreEnemy2;



    private bool stopAnim = true;

    private Animator anim;

    private void Start() {
        anim = cam.GetComponent<Animator>();
    }

    public void PlayerUISet(int uiIndex)
    {
        PlayersUI[uiIndex].SetActive(true);
    }

    public void FirstRound()
    {
        StartCoroutine(DefenseRound());
    }

    public void InGameQuestions()
    {
        StartCoroutine(QuestionPoints());
    }

    public IEnumerator QuestionPoints()
    {
        yield return new WaitForSeconds(1f);
        scorePlayer.text = (int.Parse(scorePlayer.text) + 300).ToString();

    }
    
    public IEnumerator DefenseRound()
    {
        Animator scoresAnim = PlayerUI.GetComponent<Animator>();
        yield return new WaitForSeconds(1f);
        TextBetweenScenes.SetActive(true);
        yield return new WaitForSeconds(2f);
        TextBetweenScenes.SetActive(false);
        yield return new WaitForSeconds(1f);
        questionUI.SetActive(true);
        scoresAnim.SetBool("Slide", true);
        GSManager.SetQuestion();
    }

    public void OnPlayClicked()
    {
        // cam.transform.localPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, -10f);
        StartCoroutine("CamAnim");
        StartCoroutine("CamStop");
        // anim.Play("CamIntro");
        
        menu.SetActive(false);
        gameplayUI.SetActive(true);

        
    }

    public void PlanetSelected()
    {
        gameplayUI.SetActive(false);
        planetTxt.SetActive(true);
        StartCoroutine("planetText");
    }
    
    IEnumerator planetText()
    {
        yield return new WaitForSeconds(2f);
        planetUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);


    } 

    public void GameplayScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator CamAnim()
    {
        while(stopAnim){
        cam.transform.position = Vector3.Lerp(cam.transform.position, 
        new Vector3(cam.transform.position.x, cam.transform.position.y, -10f), 0.01f);
        yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CamStop()
    {
        yield return new WaitForSeconds(2f);
        stopAnim = false;
    }
    

}
