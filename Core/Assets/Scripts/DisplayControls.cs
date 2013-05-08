using UnityEngine;
using System.Collections;

public class DisplayControls : MonoBehaviour 
{
	void OnGUI()
	{
		//Lateral Esquerda
		GUI.Box(new Rect(10, 10, 100, 25), "Trabalho de AI");
		GUI.Box(new Rect(10, 40, 100, 50), "Controles");
		GUI.Label(new Rect(20, 65, 100, 25), "Iniciar = A");
		
		//Mensagens de bugs ou outras (superior)
		GUI.Box(new Rect(Screen.width * 0.5f - 150 + 20, 10, 300, 50), "");
		GUI.Label(new Rect(Screen.width * 0.5f - 145 + 20, 10, 290, 20), "DEBUG: esta com erro na segunda tentativa de dropar a madeira.");
	}
}
