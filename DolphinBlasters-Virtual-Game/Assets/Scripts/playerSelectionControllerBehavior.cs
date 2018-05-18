using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSelectionControllerBehavior : MonoBehaviour {

	[SerializeField]
	private int _controllerNumber;

	[SerializeField]
	private Image[] _imgs = new Image[4];

	[SerializeField]
	private Sprite[] _my_sprites = new Sprite[4];

	[SerializeField]
	private Sprite[] _normal_sprites = new Sprite[4];

	[SerializeField]
	private GameObject _screen_manager;

	private playerSelectScreenController manager;

	private int _taken_player = 5; //5 is default for no player

	private float buffer = 0.015f;
	// Use this for initialization
	void Start () {
		manager = _screen_manager.GetComponent<playerSelectScreenController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//top left
		if(Input.GetAxis("Horizontal"+  _controllerNumber) < -buffer && Input.GetAxis("Vertical"+  _controllerNumber) > buffer && !manager.GetTakenScreen(0))
		{
			manager.UnselectScreen(_taken_player-1);
			_taken_player = 1;
			manager.SetScreenTaken(_taken_player-1);
			_imgs [0].sprite = _my_sprites [0];
			if (!manager.GetTakenScreen (1)) {
				_imgs [1].sprite = _normal_sprites [1];
			}
			if (!manager.GetTakenScreen (2)) {
				_imgs [2].sprite = _normal_sprites [2];
			}
			if (!manager.GetTakenScreen (3)) {
				_imgs [3].sprite = _normal_sprites [3];
			}
			//selectPlayer (4,0,1,3,2);
		}
		//top right
		if(Input.GetAxis("Horizontal"+  _controllerNumber) > buffer && Input.GetAxis("Vertical"+  _controllerNumber) > buffer && !manager.GetTakenScreen(1))
		{
			manager.UnselectScreen(_taken_player-1);
			_taken_player = 2;
			manager.SetScreenTaken(_taken_player-1);
			if (!manager.GetTakenScreen (0)) {
				_imgs [0].sprite = _normal_sprites [0];
			}
			_imgs [1].sprite = _my_sprites [1];
			if (!manager.GetTakenScreen (2)) {
				_imgs [2].sprite = _normal_sprites [2];
			}
			if (!manager.GetTakenScreen (3)) {
				_imgs [3].sprite = _normal_sprites [3];
			}
			//selectPlayer (4,1,0,3,2);
		}
		//bottom left
		if(Input.GetAxis("Horizontal"+  _controllerNumber) < -buffer && Input.GetAxis("Vertical"+  _controllerNumber) < -buffer && !manager.GetTakenScreen(2))
		{
			manager.UnselectScreen(_taken_player-1);
			_taken_player = 3;
			manager.SetScreenTaken(_taken_player-1);
			if (!manager.GetTakenScreen (0)) {
				_imgs [0].sprite = _normal_sprites [0];
			}
			if (!manager.GetTakenScreen (1)) {
				_imgs [1].sprite = _normal_sprites [1];
			}
			_imgs [2].sprite = _my_sprites [2];
			if (!manager.GetTakenScreen (3)) {
				_imgs [3].sprite = _normal_sprites [3];
			}
			//selectPlayer (4,2,0,1,3);
		}
		//bottom right
		if(Input.GetAxis("Horizontal"+  _controllerNumber) > buffer && Input.GetAxis("Vertical"+  _controllerNumber) < -buffer && !manager.GetTakenScreen(3))
		{
			manager.UnselectScreen(_taken_player-1);
			_taken_player = 4;
			manager.SetScreenTaken(_taken_player-1);
			if (!manager.GetTakenScreen (0)) {
				_imgs [0].sprite = _normal_sprites [0];
			}
			if (!manager.GetTakenScreen (1)) {
				_imgs [1].sprite = _normal_sprites [1];
			}
			if (!manager.GetTakenScreen (2)) {
				_imgs [2].sprite = _normal_sprites [2];
			}
			_imgs [3].sprite = _my_sprites [3];
			//selectPlayer (4,3,0,1,2);
		}
	}

	//fuck this method
	private void selectPlayer(int pPlayer_number, int pMy_sprite, int pSprite1,int pSprite2, int pSprite3)
	{
		manager.UnselectScreen(_taken_player-1);
		_taken_player = pPlayer_number;
		manager.SetScreenTaken(_taken_player-1);
		if(!manager.GetTakenScreen(pSprite1))
		{
		_imgs [pSprite1].sprite = _normal_sprites [pSprite1];
		}
		if (!manager.GetTakenScreen(pSprite2)) {
			_imgs [pSprite2].sprite = _normal_sprites [pSprite2];
		}
		if (!manager.GetTakenScreen (pSprite3)) {
			_imgs [pSprite3].sprite = _normal_sprites [pSprite3];
		}
		_imgs [pMy_sprite].sprite = _my_sprites [pMy_sprite];
	}
}
