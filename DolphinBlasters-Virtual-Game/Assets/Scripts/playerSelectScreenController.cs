using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSelectScreenController : MonoBehaviour {

	//bool check on whether a certain playerscreen is already taken
	private bool[] screen = new bool[5];

	private bool[] _ready = new bool[4];

	private int totalPlayerCount = 0;
	private bool[] _registrated = new bool[4];

	[SerializeField]
	private LoadSceneManager sceneManager;

	[SerializeField]
	private Image[] _playerVisual = new Image[4];

	[SerializeField]
	private Sprite[] _visuals = new Sprite[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Start1") && !_registrated[0]) {
			totalPlayerCount++;
			_playerVisual [0].gameObject.SetActive (true);
			_registrated[0] = true;
		}
		if (Input.GetButtonDown ("Start2") && !_registrated[1])
		{
			totalPlayerCount++;
			_playerVisual [1].gameObject.SetActive (true);
			_registrated[1] = true;
		}
		if (Input.GetButtonDown ("Start3") && !_registrated[2])
		{
			totalPlayerCount++;
			_playerVisual [2].gameObject.SetActive (true);
			_registrated[2] = true;
		}
		if (Input.GetButtonDown ("Start4") && !_registrated[3])
		{
			totalPlayerCount++;
			_playerVisual [3].gameObject.SetActive (true);
			_registrated[3] = true;
		}
			
	}

	//returns whether the playerscreen is already taken by someone
	public bool GetTakenScreen (int pScreen_number)
	{
		switch (pScreen_number)
		{
		case 0:
			return screen [pScreen_number];

		case 1:
			return screen [pScreen_number];

		case 2:
			return screen [pScreen_number];

		default:
			return screen [pScreen_number];

		}
	}
		
	public bool GetIsRegistrated(int pPlayerNumb)
	{
		return _registrated[pPlayerNumb];
	}

	public void tryStartCharacterSelect(bool isReady, int pPlayerNumb)
	{
		_ready [pPlayerNumb] = isReady;
		if (totalPlayerCount > 1) {
			sceneManager.LoadCharacterSelect (_ready, totalPlayerCount);
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
