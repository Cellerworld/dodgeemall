using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnabler : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
		
	}

    public void EnablePlayers()
    {
        GameManager.is_animation_over = true;
    }
}
