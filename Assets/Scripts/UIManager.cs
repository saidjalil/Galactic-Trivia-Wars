using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject planetUI;
    [SerializeField] private GameObject planetTxt;
    [SerializeField] private GameObject startButton;

    [SerializeField] private Text guideText;

    [SerializeField] private GameObject[] PlayersUI;
    [SerializeField] private GameObject PlayerUI;


    [SerializeField] private GameObject TextBetweenScenes;

    [SerializeField] private GameObject questionUI;

    [SerializeField] private GameObject playerActionUI;

    [SerializeField] private GameplaySceneManager GSManager;

    private RaycastHit raycastHit;


    public BattleState state;

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
    
    public void StartActualGame()
    {
        Animator scoresAnim = PlayerUI.GetComponent<Animator>();
        questionUI.SetActive(false);
        TextBetweenScenes.transform.GetChild(0).GetComponent<Text>().text = "BATTLE BEGINS!"; 
        StartCoroutine(TurnBase());
        TextBetweenScenes.SetActive(true);
        scoresAnim.SetBool("Slide", false);
    }

    public IEnumerator TurnBase()
    {
        yield return new WaitForSeconds(1f);
        TextBetweenScenes.SetActive(false);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
	{
		guideText.text = "Choose an action";
        playerActionUI.SetActive(true);

	}

    public void AttackButtonPressed()
    {
        guideText.text = "Select a planet to attack";
        playerActionUI.SetActive(false);
        StartCoroutine(AttackQuestion());
    }

    public void DefenceButtonPressed()
    {
        guideText.text = " ";
        playerActionUI.SetActive(false);
    }
    public IEnumerator AttackQuestion()
    {
        Animator scoresAnim = PlayerUI.GetComponent<Animator>();
        Debug.Log("a");
        bool loopin = true;
        yield return new WaitForSeconds(2f);
        while(loopin)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Input.GetMouseButton(0) && Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.transform.tag == "Selectable")
                {
                    guideText.text = " ";
                    playerActionUI.SetActive(false);
                    questionUI.SetActive(true);
                    GSManager.SetQuestion();
                    loopin = false;
                    scoresAnim.SetBool("Slide", true);
                }
                
            }
            yield return new WaitForEndOfFrame();
        }

        
    }

}
