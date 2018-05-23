using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenController : MonoBehaviour {

	[SerializeField]
	private LoadSceneManager _scene_manager;

    [SerializeField]
    private GameObject[] _characters = new GameObject [4];


    [SerializeField]
    private GameObject[] _text_lines = new GameObject[4];

	// Use this for initialization
	void Start () {
        if (GameManager._winner_id == 1)
        {
            _characters[0].SetActive(true);
        }
        else if (GameManager._winner_id == 2)
            {
            _characters[1].SetActive(true);
        }
       else if (GameManager._winner_id == 3)
            {
            _characters[2].SetActive(true);
        }
        else if (GameManager._winner_id == 4)
            {
            _characters[3].SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            if (GameManager.registarted_player_controllernumber[i] ==  GameManager._winner_controller_number)
            {
                _text_lines[i].SetActive(true);
                _text_lines[i].GetComponentInChildren<SpriteRenderer>().color = GameManager.player_colors[i];
                break;
            }
        }
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
