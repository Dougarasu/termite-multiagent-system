using UnityEngine;
using System.Collections;

public class DisplayControls : MonoBehaviour 
{
	void Awake()
	{
		transform.GetChild(0).GetComponent<GUIText>().material.color = Color.yellow;	
	}
	
	public void ShowTestDisplay()
	{
		transform.GetChild(0).GetComponent<GUIText>().text = "Em modo teste"; 
	}
	
	public void ShowNormalDisplay()
	{
		transform.GetChild(0).GetComponent<GUIText>().text = "Em modo livre";
	}
}
