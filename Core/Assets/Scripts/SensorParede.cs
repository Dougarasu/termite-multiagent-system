using UnityEngine;
using System.Collections;

//
//Sensor da parede, para comunicar ao cupim que colidiu com uma parede.
//
public class SensorParede : MonoBehaviour 
{
	public float maxHeadingChange = 60;		//Intervalo máximo de inversão de direção
	
	private Cupim agent;				
	
	void Awake()
	{
		agent = transform.parent.GetComponent<Cupim>();	
	}
	
	void OnTriggerEnter(Collider col)
	{
		//Se colidiu com uma parede, o sensor comunica o cupim para que ele mude sua direção
		if (col.CompareTag("wall"))
		{
			Vector3 v = new Vector3(0, Random.Range(col.transform.eulerAngles.y - maxHeadingChange, col.transform.eulerAngles.y + maxHeadingChange), 0);
			agent.WallHit(v);
		}
	}
}
