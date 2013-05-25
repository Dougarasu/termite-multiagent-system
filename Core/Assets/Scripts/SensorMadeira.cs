using UnityEngine;
using System.Collections;

//
//Sensor de madeira, para comunicar ao cupim quando encontrar uma madeira no cenário.
//
public class SensorMadeira : MonoBehaviour 
{
	private Cupim agent;
	
	void Awake()
	{
		agent = transform.parent.GetComponent<Cupim>();	
	}
	
	void OnTriggerEnter(Collider col)
	{
		//Se encontrou uma madeira, envia mensagem para o cupim com o 'transform' da madeira
		if (col.CompareTag("wood"))
		{
			agent.WoodFound(col.transform);
		}
	}
}
