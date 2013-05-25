using UnityEngine;
using System.Collections;

//
//Gerador de cupim, que cria todos os cupins no cenário.
//
public class GeradorCupim : MonoBehaviour 
{
	public GameObject prefab;				//Prefab do cupim
	public float spawnTime = 1.0f;			//Intervalo de tempo entre cada criação de cupim (em segundos)
	public int maxSpawns = 10;				//Quantidade máxima de cupins a serem criados
	
	private bool onOff = false;
	private Transform spawnPoint;
	private int spawneds = 0;
	
	void Awake()
	{
		spawnPoint = transform.FindChild("Spawn Point");
	}
	
	IEnumerator StartGenerator()
	{
		//Enquanto estiver ligado o gerador, cria os cupins na posição 'spawnPoint' até que todos os 'maxSpawns' cupins forem criados
		while (onOff)
		{
			if (spawneds >= maxSpawns)
			{
				spawneds = 0;
				onOff = false;
				
				StopAllCoroutines();
				
				Debug.Log("Gerador de cupim parou.");
				
				break;
			}
			
			Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);	
			spawneds++;
			
			yield return new WaitForSeconds(spawnTime);	
		}
	}
	
	//Liga o gerador
	public void TurnOn()
	{
		onOff = true;
		
		StartCoroutine("StartGenerator");
	}
}
