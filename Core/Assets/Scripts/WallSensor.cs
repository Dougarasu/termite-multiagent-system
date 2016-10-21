using UnityEngine;
using System.Collections;

public class WallSensor : MonoBehaviour 
{
	public float maxHeadingChange = 60;
	private TermiteBehaviour agent;
	
	void Awake()
	{
		agent = transform.parent.GetComponent<TermiteBehaviour>();	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("wall"))
		{
			Vector3 v = new Vector3(0, Random.Range(col.transform.eulerAngles.y - maxHeadingChange, col.transform.eulerAngles.y + maxHeadingChange), 0);
			agent.WallHit(v);
		}
	}
}
