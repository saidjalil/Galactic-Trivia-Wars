using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planets;

    private PlanetSO selectedPlanet;

    public static GameManager instance;

    private UIManager uiManager;

    public GameObject enemy1;

    public GameObject enemy2;

#region Singleton
void Awake()
{

	if (instance == null)
	{
		instance = this;
	}
	else
	{
		Destroy(gameObject);
		return;
	}

	DontDestroyOnLoad(gameObject);
}

#endregion
    public void Spawn()
    {
        StartCoroutine(SpawnPlanets());
    }
    private IEnumerator SpawnPlanets()
    {
        // for(int i = 0; i < 3; i++){
        // int random = Random.Range(1, 4);
        // }
        yield return new WaitForSeconds(0.3f);
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        Instantiate(planets[selectedPlanet.planetSelection], new Vector3(-5f, 1f ,-15f) ,Quaternion.identity);
        uiManager.PlayerUISet(0);
        yield return new WaitForSeconds(0.7f);
        int random = Random.Range(1,4);
        enemy1 = Instantiate(planets[random], new Vector3(Random.Range(0f,5f),4, -15f), Quaternion.identity);
        uiManager.PlayerUISet(1);
        yield return new WaitForSeconds(0.6f);
        random = Random.Range(1,4);
        enemy2 =Instantiate(planets[random], new Vector3(Random.Range(-0.5f,5.5f),-2, -15f), Quaternion.identity);
        uiManager.PlayerUISet(2);
        uiManager.FirstRound();
    }
        

    public void GetPlanetData(PlanetSO planet)
    {
        selectedPlanet = planet;
        Debug.Log(selectedPlanet);
    }


}
