using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAudio : MonoBehaviour {

	public  AudioClip[] Effeckts;
	public  AudioClip[] Mocking;
	public  AudioClip[] Encouraging;
	public  AudioClip[] Anouncing;
	public  AudioClip[] Cheering;
	public  AudioClip[] Beginning;

	public static float cd;
	//private Random _rnd;
	// Use this for initialization
	void Start () {
		//_rnd = new Random();
		StartCoroutine ("startline");
	}

	// Update is called once per frame
	void Update () {
		if (cd > 0) {
			cd -= Time.deltaTime;
		}
	}

	IEnumerator startline()
	{
		
		yield return new WaitForSeconds (1f);
		GetComponent<AudioSource> ().PlayOneShot (Beginning[Random.Range(0, Beginning.Length)]);
		StopCoroutine ("startline");

	}
}
