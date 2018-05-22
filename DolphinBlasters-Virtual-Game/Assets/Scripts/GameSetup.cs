using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

	[SerializeField]
	private GameObject[] _character = new GameObject[4]; 

	// Use this for initialization
	void Awake () {
		
//		else if (GameManager.active_characters[1]) {
//			_character [1].SetActive (true);
//		}
//		else if (GameManager.active_characters[2]) {
//			_character [2].SetActive (true);
//		}
//		else if (GameManager.active_characters[3]) {
//			_character [3].SetActive (true);
//		}

		for (int i = 0; i < 4; i++)
		{
			if (GameManager.used_character[i] == 1) {
				_character [1].SetActive (true);
			}
			if (GameManager.used_character[i] == 2) {
				_character [2].SetActive (true);
			}
			if (GameManager.used_character[i] == 3) {
				_character [3].SetActive (true);
			}
			if (GameManager.used_character[i] == 4) {
				_character [4].SetActive (true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
