using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this class is attached o objects that trigger loading a new scene
public class LoadSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadPLayerSelect()
	{
		SceneManager.LoadScene ("Player_selection");
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
			SceneManager.LoadScene ("Main");
		}
	}
}
