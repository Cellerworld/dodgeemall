using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSelectScreenController : MonoBehaviour {

	//bool check on whether a certain playerscreen is already taken
	private bool[] screen = new bool[5];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//returns whether the playerscreen is already taken by someone
	public bool GetTakenScreen (int pScreen_number)
	{
		switch (pScreen_number)
		{
		case 0:
			return screen [pScreen_number];
			break;
		case 1:
			return screen [pScreen_number];
			break;
		case 2:
			return screen [pScreen_number];
			break;
		default:
			return screen [pScreen_number];
			break;
		}
	}

	//Selects a playerscreen for a controller
	public void SetScreenTaken(int pScreen_number)
	{
		screen [pScreen_number] = true;
	}

	//unselects a player screen for a controller
	public void UnselectScreen(int pScreen_number)
	{
		screen [pScreen_number] = false;
	}
}
