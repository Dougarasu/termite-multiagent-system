using UnityEngine;
using System.Collections;

public class DisplayControls : MonoBehaviour 
{
	void OnGUI()
	{
		//Lateral Esquerda
		GUI.Box(new Rect(10, 10, 150, 25), "Trabalho de AI");
		GUI.Box(new Rect(10, 40, 150, 100), "Controles");
		GUI.Label(new Rect(20, 65, 150, 25), "Cria Cupins = A");
		GUI.Label(new Rect(20, 90, 150, 25), "Pausa Cupins = S");
		GUI.Label(new Rect(20, 115, 150, 25), "Continua Cupins = D");
		
		//Mensagens de bugs ou outras (superior)
		GUI.Box(new Rect(Screen.width * 0.5f - 150 + 20, 10, 300, 50), "");
		GUI.Label(new Rect(Screen.width * 0.5f - 145 + 20, 10, 290, 20), "DEBUG: melhorar o algoritmo de \"caminhar aleatório\".");
	}
}
