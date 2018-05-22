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

	[SerializeField]
	private Animator[] animator = new Animator[2];

	private int i = 0;

	// private bool _animation_has_played;
	[SerializeField]
	private GameObject _enable;
	[SerializeField]
	private GameObject _disable;
	[SerializeField]
	private GameObject _select;

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
			//			i++;
			//			Debug.Log (i);
			//			if (_goBackObject.transform.parent.tag == "lores") {
			//				Debug.Log (_goBackObject.transform.parent.gameObject.name);
			//				_goBackObject.transform.parent.gameObject.SetActive (true);
			//				eventSystem.SetSelectedGameObject (_goBackObject);
			//				gameObject.transform.parent.gameObject.SetActive (false);
			//				return;
			//			} 
			//else {
			//Debug.Log (_goBackObject.transform.parent.gameObject.name);

			StartCoroutine ("goBack");
			//				_goBackObject.transform.parent.gameObject.SetActive (true);
			//				eventSystem.SetSelectedGameObject (_goBackObject);
			//				gameObject.transform.parent.gameObject.SetActive (false);
			//}

			//StartCoroutine ("goBack");

		}

	}

	public void goToNext()
	{

		StartCoroutine ("goNext");
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

	IEnumerator goNext()
	{
		
		animator [1].SetTrigger ("fadeOut");
	
		yield return new WaitForSeconds (1.5f);

		eventSystem.SetSelectedGameObject (_select);
		_enable.SetActive (true);


		_disable.SetActive (false);


		yield return new WaitForSecondsRealtime (2);

		StopCoroutine ("goNext");

		yield return new WaitForEndOfFrame();
	}

	IEnumerator goBack()
	{
		
			animator[1].SetTrigger ("fadeOut");
			//animator.Play(0);
			

			yield return new WaitForSeconds (1.5f);
			
			_goBackObject.transform.parent.gameObject.SetActive (true);
			eventSystem.SetSelectedGameObject (_goBackObject);
			gameObject.transform.parent.gameObject.SetActive (false);
			

			yield return new WaitForSeconds (2);
			StopCoroutine ("goBack");

	}
}
