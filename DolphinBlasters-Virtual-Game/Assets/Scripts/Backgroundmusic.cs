using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundmusic : MonoBehaviour {
	[SerializeField]
	private AudioClip _clip;
	private bool _plays;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		GetComponent<AudioSource> ().Play();

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.active_characters != null && !_plays) 
		{
			
			GetComponent<AudioSource> ().clip = _clip;
			GetComponent<AudioSource> ().Play(1);
			_plays = true;
		}
	}
}
