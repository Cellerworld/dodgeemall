using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameButtonBehaviour : MonoBehaviour {

	[SerializeField]
	private PauseMenuBehaviour _menu_manager;

	[SerializeField]
	private LoadSceneManager _scene_manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Resume()
	{
		_menu_manager.Resume ();
	}

	public void Options()
	{
		
	}

	public void Quit()
	{
		_scene_manager.LoadMenu ();
	}
}
