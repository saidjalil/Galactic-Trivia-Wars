using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{

    // public PlanetSO planet;
    public TMP_Text planetTxt;
    public TMP_Text planetSummary;

    public void PlanetInfo(PlanetSO Planet)
    {
        planetTxt.text = Planet.planetNametxt;
        planetSummary.text = Planet.planetSummary;
    }
}
