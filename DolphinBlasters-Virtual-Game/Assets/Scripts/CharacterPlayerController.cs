using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayerController : MonoBehaviour {

	[SerializeField]
	private int _controllerNumber;

	private int _player_number;

	private int _character_number;

	private static bool[] _character_is_taken = new bool[4];

	[SerializeField]
	private Transform[] _character_positions;

	private bool[] _selected = new bool[4];

	private static  int total_selects;

	[SerializeField]
	private LoadSceneManager scene_manager;

	// Use this for initialization
	void Start () {
		//_controllerNumber = ;
		if (_controllerNumber == GameManager.registarted_player_controllernumber [0]) {
			_player_number = 0;
		}
		else if (_controllerNumber == GameManager.registarted_player_controllernumber [1]) {
			_player_number = 1;
		} 
		else if (_controllerNumber == GameManager.registarted_player_controllernumber [2]) {
			_player_number = 2;
		}
		else if (_controllerNumber == GameManager.registarted_player_controllernumber [3]) {
			_player_number = 3;
		}
		else
		{
			gameObject.SetActive (false);
		}
		total_selects = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(GameManager.amount_of_player);
		if (Input.GetKeyDown ("joystick button 9") && total_selects == GameManager.amount_of_player) 
		{
			GameManager.used_character [_player_number] = _character_number;

			scene_manager.LoadInGame ();
		}

		if(_selected[0])
		{
			Selected1 ();
		}
		else if(_selected[1])
		{
			Selected2 ();
		}
		else if(_selected[2])
		{
			Selected3 ();
		}
		else if(_selected[3])
		{

			Selected4 ();
		}
		else 
		{
			Unselected ();
		}
	}

	private void Unselected()
	{
		Selected1 ();
		Selected2 ();
		Selected3 ();
		Selected4 ();
	}

	private void Selected1()
	{
		if (Input.GetButtonDown ("Dodge" + _controllerNumber)) {
			if (!_character_is_taken [0]) {
				_character_number = 1;
				total_selects++;
				transform.position = new Vector3 (_character_positions [0].position.x, transform.position.y, _character_positions [0].position.z);
				_character_is_taken [0] = true;
				_selected [0] = true;
			}
			else if(_selected[0]){
				total_selects--;
				transform.position = new Vector3 (-100, transform.position.y, -100);
				_character_is_taken [0] = false;
				_selected [0] = false;
			}
		} 

	}
	private void Selected2()
	{
		if (Input.GetKeyDown ("joystick "+ _controllerNumber + " button 1")) {
			if(!_character_is_taken[1]){
				_character_number = 2;
				total_selects++;
				transform.position = new Vector3 (_character_positions[1].position.x , transform.position.y , _character_positions[1].position.z);
				_character_is_taken[1] = true;
				_selected [1] = true;
			}
			else if(_selected[1]){
				total_selects--;
				transform.position = new Vector3 (-100, transform.position.y, -100);
				_character_is_taken [1] = false;
				_selected [1] = false;
			}
		}
	}
	private void Selected3()
	{
		if (Input.GetButtonDown ("Submit"+ _controllerNumber)  ) {
			if(!_character_is_taken[2]){
				_character_number = 3;
				total_selects++;
				transform.position = new Vector3 (_character_positions[2].position.x , transform.position.y , _character_positions[2].position.z);
				_character_is_taken[2] = true;
				_selected [2] = true;
			}
			else if(_selected[2]){
				total_selects--;
				transform.position = new Vector3 (-100, transform.position.y, -100);
				_character_is_taken [2] = false;
				_selected [2] = false;
			}
		}
	}
	private void Selected4()
	{
		if (Input.GetKeyDown ("joystick "+ _controllerNumber + " button 3")  ) {
			if(!_character_is_taken[3]){
				_character_number = 4;
				total_selects++;
				transform.position = new Vector3 (_character_positions[3].position.x , transform.position.y , _character_positions[3].position.z);
				_character_is_taken[3] = true;
				_selected [3] = true;
			}
			else if(_selected[3]){
				total_selects--;
				transform.position = new Vector3 (-100, transform.position.y, -100);
				_character_is_taken [3] = false;
				_selected [3] =  false;
			}
		}
	}
}
