using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{

	private Ray ray;
	private RaycastHit ryacastHit;

	// Use this for initialization

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)
		{
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out ryacastHit)) 
			{
				if (ryacastHit.transform.name == "Spelen")
				{
					SceneManager.LoadScene ("Level1");
				}
			}
		}
	
	}
}
