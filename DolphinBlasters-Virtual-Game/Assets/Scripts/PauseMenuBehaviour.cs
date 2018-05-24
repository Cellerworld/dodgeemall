using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour {

	[SerializeField]
	private GameObject[]_game_objects_to_pause;

	[SerializeField]
	private GameObject _hud_to_pause;

	private bool _is_paused;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("joystick button 9")) {
			Resume ();
		}
	}

	public void Resume()
	{
			_is_paused = !_is_paused;

			foreach (GameObject a in _game_objects_to_pause) {
				a.GetComponent<MonoBehaviour>().enabled = !_is_paused;
			}
			_hud_to_pause.SetActive (_is_paused);
		
	}
}
