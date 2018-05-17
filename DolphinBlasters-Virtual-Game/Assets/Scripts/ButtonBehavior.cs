using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour {

	[SerializeField]
	private Button[] _buttons;

	[SerializeField]
	private EventSystem eventSystem;

	[SerializeField]
	private GameObject _goBackObject;

	private int i = 0;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel")  ) 
		{
			if (_goBackObject.transform.parent.gameObject.name == this.transform.parent.gameObject.name)
			{
				return;
			}
			i++;
			Debug.Log (i);
			if (_goBackObject.transform.parent.tag == "lores") {
				Debug.Log (_goBackObject.transform.parent.gameObject.name);
				_goBackObject.transform.parent.gameObject.SetActive (true);
				eventSystem.SetSelectedGameObject (_goBackObject);
				gameObject.transform.parent.gameObject.SetActive (false);
				return;
			} 
			else {
				Debug.Log (_goBackObject.transform.parent.gameObject.name);
				_goBackObject.transform.parent.gameObject.SetActive (true);
				eventSystem.SetSelectedGameObject (_goBackObject);
				gameObject.transform.parent.gameObject.SetActive (false);
			}

			//StartCoroutine ("goBack");

		}
		
	}

	public void increaseOnHover(Button other)
	{
		other.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
	}

	public void decreaseOnHoverOff(Button other)
	{
		other.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void OnMenuChange(GameObject newHighlightedObject)
	{
		eventSystem.SetSelectedGameObject (newHighlightedObject);
	}

//	IEnumerator goBack()
//	{
//		
//		yield return new WaitForSeconds(2);
//	}
}
