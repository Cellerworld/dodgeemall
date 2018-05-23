using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenController : MonoBehaviour {

	[SerializeField]
	private LoadSceneManager _scene_manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("joystick button 9"))
		{
			for(int i = 0; i< 4; i++)
			{
				GameManager.used_character [i] = 0;
			}
			_scene_manager.LoadCharacterSelect();
		}
			
	}
}
