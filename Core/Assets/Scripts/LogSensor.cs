using UnityEngine;
using System.Collections;

public class LogSensor : MonoBehaviour 
{
	private TermiteBehaviour agent;
	
	void Awake()
	{
		agent = transform.parent.GetComponent<TermiteBehaviour>();	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("wood"))
		{
			agent.WoodFound(col.transform);
		}
	}
}
