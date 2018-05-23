using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private static float _max_restriction_time = 3f;

    private static float _restriction_timer;

    public static CharacterBehaviour restricted_character;
    public static CharacterBehaviour current_ball_owner;
    public static CharacterBehaviour last_ball_owner;
    public static List<CharacterBehaviour> active_characters;
    public static bool is_game_over;
    public static bool is_animation_over;
	//a field that declares the amount of players who registrated for the game
	public static int amount_of_player;
	//fields that function as informations for later scenes.
	//the array index represents the following
	//0 : player 1
	//1 : player 2
	//2 : player 3
	//3 : Player 4
	public static int[] registarted_player_controllernumber = new int[4]; // holds The Controllernumber that controls the player (anything >4 and < 1 is no player)
	public static Color[] player_colors = new Color[4]; // the color the player
	public static int[] used_character = new int[4]; // an int representing the character that the player chose
													// 1 : Zielbot  / 2 : Gee   / 3 Evilbot   / 4 : Elivator
	private GameObject _game_manager;

    public static int _winner_id;
    public static int _winner_controller_number;

    private void Start()
    {
		DontDestroyOnLoad (_game_manager);
        
		CharacterBehaviour[] chars = FindObjectsOfType<CharacterBehaviour>();
        is_animation_over = false;
//        foreach(CharacterBehaviour character in chars)
//        {
//            active_characters.Add(character);
//        }
    }

    public static void SetRestrictedCharacrter(CharacterBehaviour character)
    {
        restricted_character = character;
        _restriction_timer = _max_restriction_time;
    }

    public static void RemovePlayer(CharacterBehaviour character_to_remove)
    {
        if(active_characters.Contains(character_to_remove))
        {
            active_characters.Remove(character_to_remove);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(_restriction_timer <= 0)
        {
            restricted_character = null;
        }
        else
        {
            _restriction_timer -= Time.deltaTime;
        }
        if(active_characters.Count <= 1 && active_characters != null)
        {
            is_game_over = true;

        }
    }
}
