using UnityEngine;
using System.Collections;

public class LogGenerator : MonoBehaviour 
{
	public GameObject prefab;
	public float spawnTime = 1.0f;
	public int maxSpawns = 10;
	
	private bool onOff = true;
	private float spawnWidth;
	private float spawnLength;
	private int spawneds = 0;
	
	void Awake()
	{
		spawnWidth = transform.FindChild("Spawn Area").GetComponent<Collider>().bounds.size.x / 2;
		spawnLength = transform.FindChild("Spawn Area").GetComponent<Collider>().bounds.size.z / 2;
	}
	
	IEnumerator Start ()
	{
		Vector3 pos;
		
		while (true)
		{
			while (onOff)
			{
				if (spawnTime == 0)
				{
					for (int i = 0; i < maxSpawns; i++)
					{
						pos = new Vector3(Random.Range(-spawnWidth, spawnWidth), 1, Random.Range(-spawnLength, spawnLength));
						Instantiate(prefab, pos, Quaternion.identity);	
						spawneds++;
					}
					
					yield return null;
				}
				
				if (spawneds >= maxSpawns)
				{
					spawneds = 0;
					onOff = false;
					break;
				}
				
				pos = new Vector3(Random.Range(-spawnWidth, spawnWidth), 1, Random.Range(-spawnLength, spawnLength));
				Instantiate(prefab, pos, Quaternion.identity);	
				spawneds++;
				
				yield return new WaitForSeconds(spawnTime);	
			}
			
			yield return null;
		}
	}
}
