using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayerController : MonoBehaviour {

	[SerializeField]
	private int _controllerNumber;

	private static bool[] _character_is_taken = new bool[4];

	[SerializeField]
	private Transform[] _character_positions;

	private bool[] _selected = new bool[4];

	// Use this for initialization
	void Start () {
		//_controllerNumber = ;
	}
	
	// Update is called once per frame
	void Update () {

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
		if (Input.GetButtonDown ("Dodge" + _controllerNumber) ) {
			if (!_character_is_taken [0]) {
				transform.position = new Vector3 (_character_positions [0].position.x, transform.position.y, _character_positions [0].position.z);
				_character_is_taken [0] = true;
				_selected [0] = true;
			}
			else {
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
				transform.position = new Vector3 (_character_positions[1].position.x , transform.position.y , _character_positions[1].position.z);
				_character_is_taken[1] = true;
				_selected [1] = true;
			}
			else {
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
				transform.position = new Vector3 (_character_positions[2].position.x , transform.position.y , _character_positions[2].position.z);
				_character_is_taken[2] = true;
				_selected [2] = true;
			}
			else {
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
				transform.position = new Vector3 (_character_positions[3].position.x , transform.position.y , _character_positions[3].position.z);
				_character_is_taken[3] = true;
				_selected [3] = true;
			}
			else {
				transform.position = new Vector3 (-100, transform.position.y, -100);
				_character_is_taken [3] = false;
				_selected [3] =  false;
			}
		}
	}
}
