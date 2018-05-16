using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchModeButtonBehavior : MonoBehaviour {

	private bool _is_Selected;
	private TextMeshProUGUI _current_Text;

	[SerializeField]
	private string[] _texts;

	private TextMesh _text;

	private int i;

	// Use this for initialization
	void Start () {
		i = 0;
		_current_Text = this.GetComponent<TextMeshProUGUI> ();
		_current_Text.text = _texts[i];
	}

	// Update is called once per frame
	void Update () {
		if (_is_Selected) {
			if (Input.GetAxis ("Horizontal") < -0.1f) {
				i--;
				_is_Selected = false;
				StartCoroutine ("waitWithInput" );
			}
			if (Input.GetAxis ("Horizontal") > 0.1f) {
				i++;
				_is_Selected = false;
				StartCoroutine ("waitWithInput");
			}
			if (i < 0) 
			{
				i = _texts.Length-1;
			}
			i %= _texts.Length;
			_current_Text.text = _texts[i];


		}
	}

	//On hover over activate the control of what macthmode we select
	public void IsSelected()
	{
		_is_Selected = true;
	}
	//On hover away deactivate the control of what macthmode we select
	public void IsUNseclected()
	{
		StopCoroutine ("waitWithInput");
		_is_Selected = false;
	}

	public void printProof()
	{
		Debug.Log ("PROOOOOOOFED!");
	}

	IEnumerator waitWithInput()
	{
		yield return new WaitForSeconds (0.3f);
		_is_Selected = true;
		StopCoroutine ("waitWithInput");
	}
}
