
using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
	public static float MoveSpeed = 2;
	public static bool TermitesOn = false;
	public float speedAdd = 0.2f;
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
			MoveSpeed += speedAdd;
		
		if (Input.GetKeyDown(KeyCode.W))
			MoveSpeed -= speedAdd;
		
		if (Input.GetKeyDown(KeyCode.A))
			GameObject.Find("TermiteGenerator").GetComponent<TermiteGenerator>().TurnOn();	
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			GameObject []termites = GameObject.FindGameObjectsWithTag("termite");	
			
			foreach (GameObject item in termites)
				item.GetComponent<TermiteBehaviour>().enabled = false;
		}
		
		if (Input.GetKeyDown(KeyCode.D))
		{
			GameObject []termites = GameObject.FindGameObjectsWithTag("termite");	
			
			foreach (GameObject item in termites)
				item.GetComponent<TermiteBehaviour>().enabled = true;
		}
	}
}
