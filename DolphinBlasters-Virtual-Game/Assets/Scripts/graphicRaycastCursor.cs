using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class graphicRaycastCursor : MonoBehaviour {
	

	private int _controller_number;

	[SerializeField]
	private GameObject[] _controller = new GameObject[4];

	GraphicRaycaster m_Raycaster;
	PointerEventData m_PointerEventData;
	EventSystem m_EventSystem;

	void Start()
	{
		//Fetch the Raycaster from the GameObject (the Canvas)
		m_Raycaster = GetComponent<GraphicRaycaster>();
		//Fetch the Event System from the Scene
		m_EventSystem = GetComponent<EventSystem>();
	}

	void Update()
	{
		//Check if the left Mouse button is clicked

		if (Input.GetKey ("Fire1"))
		{
			startRaycast (0);
		}
		if (Input.GetKey ("Accept2"))
		{
			startRaycast (1);
		}
		if (Input.GetKey ("Accept3"))
		{
			startRaycast (2);
		}
		if (Input.GetKey ("Accept4"))
		{
			startRaycast (3);
		}
	}

	private void startRaycast(int pNumber)
	{
			//Set up the new Pointer Event
			m_PointerEventData = new PointerEventData(m_EventSystem);
			//Set the Pointer Event Position to that of the mouse position
			m_PointerEventData.position = _controller[pNumber].transform.position;

			//Create a list of Raycast Results
			List<RaycastResult> results = new List<RaycastResult>();

			//Raycast using the Graphics Raycaster and mouse click position
			m_Raycaster.Raycast(m_PointerEventData, results);

			//For every result returned, output the name of the GameObject on the Canvas hit by the Ray
			foreach (RaycastResult result in results)
			{
				Debug.Log("Hit " + result.gameObject.name);
			}
	}
}
