using UnityEngine;
using System.Collections;

public class SensorMadeira : MonoBehaviour 
{
	private Cupim agent;
	
	void Awake()
	{
		agent = transform.parent.GetComponent<Cupim>();	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("wood"))
		{
			agent.WoodFound(col.transform);
		}
	}
}
