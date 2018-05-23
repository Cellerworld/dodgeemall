using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonAudio : MonoBehaviour {
	
	private AudioSource[] _my_audiosource;

	[SerializeField]
	private AudioClip[] _clips;

	private float[] cooldown;
	// Use this for initialization
	void Start () {
		cooldown = new float[_clips.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Input.GetAxis ("Vertical") > 0.1 || Input.GetAxis ("Vertical") < -0.1) && cooldown [0] < 0) {
			_my_audiosource = GetComponents<AudioSource> ();
			_my_audiosource [0].PlayOneShot (_clips [0]);
			cooldown [0] = 0.5f;
		} else {
			cooldown [0] -= Time.deltaTime;
		}
		if ((Input.GetButtonDown ("Submit")  || Input.GetButtonDown ("Cancel") ) && cooldown [1] < 0) {
			_my_audiosource = GetComponents<AudioSource> ();
			_my_audiosource [0].PlayOneShot (_clips [1]);
			cooldown [1] = 1.5f;
		} else {
			cooldown [1] -= Time.deltaTime;
		}
	}
}
