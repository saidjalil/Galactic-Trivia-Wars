using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject planetUI;
    [SerializeField] private GameObject planetTxt;
    [SerializeField] private GameObject startButton;

    private bool stopAnim = true;

    private Animator anim;

    private void Start() {
        anim = cam.GetComponent<Animator>();
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
