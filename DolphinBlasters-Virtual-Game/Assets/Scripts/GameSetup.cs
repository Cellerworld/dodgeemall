using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

	[SerializeField]
	private GameObject[] _character = new GameObject[4]; 

	// Acticates the robots from character selection and gives them a controller number to act on
	void Awake () {
//		Debug.Log (GameManager.registarted_player_controllernumber [0]+ " player 1");
//		Debug.Log (GameManager.registarted_player_controllernumber [1]+ " player 2");
//		Debug.Log (GameManager.registarted_player_controllernumber [2]+ " player 3");
//		Debug.Log (GameManager.registarted_player_controllernumber [3]+ " player 4");


//		if (GameManager.used_character[0] != 0) {
//			_character [GameManager.used_character[0]].SetActive (true);
//		}
//		else if (GameManager.used_character[1] != 0) {
//			_character [GameManager.used_character[1]].SetActive (true);
//		}
//		else if (GameManager.used_character[2] != 0) {
//			_character [GameManager.used_character[2]].SetActive (true);
//		}
//		else if (GameManager.used_character[3] != 0) {
//			_character [GameManager.used_character[3]].SetActive (true);
//		}

		GameManager.active_characters = new List<CharacterBehaviour>();

		for (int i = 0; i < 4; i++)
		{
			if (GameManager.used_character[i] == 1) {
				_character [0].GetComponent<PlayerBehaviour> ()._player_number = GameManager.registarted_player_controllernumber [i];
				_character [0].SetActive (true);
				GameManager.active_characters.Add (_character[0].GetComponent<PlayerBehaviour>());
				Debug.Log (GameManager.registarted_player_controllernumber [i]+ "Player"+i);

				continue;
			}
			if (GameManager.used_character[i] == 2) {
				_character [1].GetComponent<PlayerBehaviour> ()._player_number = GameManager.registarted_player_controllernumber [i];
				_character [1].SetActive (true);
				GameManager.active_characters.Add (_character[1].GetComponent<PlayerBehaviour>());
				Debug.Log (GameManager.registarted_player_controllernumber [i]+ "Player"+i);

				continue;
			}
			if (GameManager.used_character[i] == 3) {
				_character [2].GetComponent<PlayerBehaviour> ()._player_number = GameManager.registarted_player_controllernumber [i];
				_character [2].SetActive (true);
				GameManager.active_characters.Add (_character[2].GetComponent<PlayerBehaviour>());
				Debug.Log (GameManager.registarted_player_controllernumber [i]+ "Player"+i);

				continue;
			}
			if (GameManager.used_character[i] == 4) {
				_character [3].GetComponent<PlayerBehaviour> ()._player_number = GameManager.registarted_player_controllernumber [i];
				_character [3].SetActive (true);
				GameManager.active_characters.Add (_character[3].GetComponent<PlayerBehaviour>());
				Debug.Log (GameManager.registarted_player_controllernumber [i]+ "Player"+i);

				continue;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
