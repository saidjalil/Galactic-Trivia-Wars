using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [SerializeField]private GameObject cam;

    private bool stopAnim = true;

    public bool stopRaycast = false;

    public UIManager uIManager;

    public UIDisplay uIDisplay;

    public PlanetSO planet;

    void Update()
    {
        if(stopRaycast)
        {
        highlight.gameObject.GetComponent<Outline>().enabled = false;
        return;
        }
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0))
        {

            if (highlight)
            {
                // if (selection != null)
                // {
                //     selection.gameObject.GetComponent<Outline>().enabled = false;
                // }
                selection = raycastHit.transform.GetChild(0).transform;
                // // selection.gameObject.GetComponent<Outline>().enabled = true;
                // highlight = null;
                planet = selection.GetComponent<PlanetSlot>().SOplanet;
                Debug.Log(planet);
                uIDisplay.PlanetInfo(planet);
                uIManager.PlanetSelected();

                StartCoroutine("TowardsPlanet");
                StartCoroutine("StopMoving");
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }
    }
     IEnumerator TowardsPlanet()
    {
        stopRaycast = true;
            while(stopAnim){
            cam.transform.position = Vector3.Lerp(cam.transform.position, 
            new Vector3(selection.transform.position.x, selection.transform.position.y, selection.transform.position.z), 0.03f);
            // cam.transform.position = Vector3.MoveTowards(transform.position, selection.transform.position, 2f);
            yield return new WaitForEndOfFrame();
        }
        
    }
    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(5f);
        // cam.transform.position = selection.transform.position;
        // cam.transform.position = new Vector3(selection.transform.position.x, selection.transform.position.y, selection.transform.position.z);
        stopAnim = false;
    }

    public void ChangeScene()
    {
        uIManager.GameplayScene();
        cam.transform.position = new Vector3(cam.transform.position.x , cam.transform.position.y, -22.4f);
    }
}

