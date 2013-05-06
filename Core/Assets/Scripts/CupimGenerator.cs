using UnityEngine;
using System.Collections;

public class CupimGenerator : MonoBehaviour 
{
	public GameObject prefab;
	public float spawnTime = 1.0f;
	public int maxSpawns = 10;
	public bool onOff = false;
	public Transform spawnPoint;
	
	private int spawneds = 0;
	
	IEnumerator Start ()
	{
		while (true)
		{
			while (onOff)
			{
				if (spawneds > maxSpawns)
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
}
