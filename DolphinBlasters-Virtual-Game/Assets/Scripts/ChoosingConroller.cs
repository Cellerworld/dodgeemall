using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosingConroller : MonoBehaviour {

	[SerializeField]
	private bool[] _is_taken;
	[SerializeField]
	private Transform[] _position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//takes the next robot available in the direction
	public Vector3 getRobot(int pNumber, int dir)
	{
		
		for(int i = 0; i< 4; i++)
		{
			pNumber += dir;
			pNumber %= 4;
			if (_is_taken[pNumber]) {
				break;
			}
		}
		return _position[pNumber].position;
	}
}
