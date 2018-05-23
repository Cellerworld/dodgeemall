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
		
	}

	public void LoadInGame()
	{
		level_name = "Arena_test";
		StartCoroutine("loadScene");
	}

	public void LoadPLayerSelect()
	{
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
			//SceneManager.LoadScene ("Main");
			GameManager.amount_of_player = pSetPlayers;
			level_name = "Character_Selection";

			StartCoroutine("loadScene");
		}
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
