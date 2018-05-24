using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this class is attached o objects that trigger loading a new scene
public class LoadSceneManager : MonoBehaviour {

	[SerializeField]
	private Animator[] animator;

	private string level_name;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("joystick button 8"))
		{
			Debug.Log ("select");
			if (GameManager.buidlindex == 1) {
				LoadMenu ();
			}
			if (GameManager.buidlindex == 2) {
				LoadPLayerSelect ();
			}
		}
	}

    public void LoadWinScreen()
    {
		GameManager.buidlindex = 5;
        level_name = "Win_Screen";
        StartCoroutine("loadScene");
    }

	public void LoadInGame()
	{
		GameManager.buidlindex = 4;
		level_name = "Arena_test";
		StartCoroutine("loadScene");
	}

	public void LoadMenu()
	{
		GameManager.buidlindex = 0;
		GameManager.amount_of_player = 0;
		for(int i = 0; i< 4; i++)
		{
			
			GameManager.registarted_player_controllernumber [i] = 0;
		}
		level_name = "Menu";
		StartCoroutine("loadScene");
	}

	public void LoadPLayerSelect()
	{
		GameManager.buidlindex = 1;
		GameManager.amount_of_player = 0;
		for(int i = 0; i< 4; i++)
		{

			GameManager.registarted_player_controllernumber [i] = 0;
		}
		level_name = "Player_selection";
		//SceneManager.LoadScene ("Player_selection");
		StartCoroutine("loadScene");
	}

	public void LoadCharacterSelect(bool[] pReady, int pSetPlayers)
	{
		
		int i = 0;
		foreach (bool a in pReady) {
			if(a)
			{
				i++;
			}
		}
		if (i == pSetPlayers) {
			GameManager.buidlindex = 2;
			//SceneManager.LoadScene ("Main");
			GameManager.amount_of_player = pSetPlayers;
			level_name = "Character_Selection";

			StartCoroutine("loadScene");
		}
	}

	public void LoadCharacterSelect()
	{
		GameManager.buidlindex = 2;
			//SceneManager.LoadScene ("Main");
			
			level_name = "Character_Selection";

			StartCoroutine("loadScene");

	}

	IEnumerator loadScene()
	{
		animator [1].SetTrigger ("fadeOut");

		yield return new WaitForSeconds (1.5f);

		SceneManager.LoadScene (level_name);

		//yield return new WaitForSecondsRealtime (2);

		StopCoroutine ("loadScene");

		yield return new WaitForEndOfFrame();

	}
}
