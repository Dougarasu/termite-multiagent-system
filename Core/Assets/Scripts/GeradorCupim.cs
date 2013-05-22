using UnityEngine;
using System.Collections;

public class GeradorCupim : MonoBehaviour 
{
	public GameObject prefab;
	public float spawnTime = 1.0f;
	public int maxSpawns = 10;
	
	private bool onOff = false;
	private Transform spawnPoint;
	private int spawneds = 0;
	
	void Awake()
	{
		spawnPoint = transform.FindChild("Spawn Point");
	}
	
	IEnumerator Start ()
	{
		while (true)
		{
			while (onOff)
			{
				if (spawneds >= maxSpawns)
				{
					spawneds = 0;
					onOff = false;
					break;
				}
				
				Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);	
				spawneds++;
				
				yield return new WaitForSeconds(spawnTime);	
			}
			
			yield return null;
		}
	}
	
	public void TurnOn()
	{
		onOff = true;
	}
}
