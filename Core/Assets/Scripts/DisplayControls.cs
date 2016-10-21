using UnityEngine;
using System.Collections;

public class DisplayControls : MonoBehaviour 
{
	void OnGUI()
	{
		// Left side info
		GUI.Box(new Rect(10, 10, 150, 25), "Termite System");
		GUI.Box(new Rect(10, 40, 150, 100), "Inputs:");
		GUI.Label(new Rect(20, 65, 150, 25), "Create termites = A");
		GUI.Label(new Rect(20, 90, 150, 25), "Pause termites = S");
		GUI.Label(new Rect(20, 115, 150, 25), "Resume termites = D");
		
		// Bug messages
		GUI.Box(new Rect(Screen.width * 0.5f - 150 + 20, 10, 300, 50), "");
		GUI.Label(new Rect(Screen.width * 0.5f - 145 + 20, 10, 290, 20), "TODO: improve states of action for the termites.");
	}
}
