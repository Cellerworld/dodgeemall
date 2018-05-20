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

    private void Start()
    {
        active_characters = new List<CharacterBehaviour>();
        CharacterBehaviour[] chars = FindObjectsOfType<CharacterBehaviour>();
        foreach(CharacterBehaviour character in chars)
        {
            active_characters.Add(character);
        }
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
        if(active_characters.Count <= 1)
        {
            is_game_over = true;
            Debug.Log("The game is over");
        }
    }
}
