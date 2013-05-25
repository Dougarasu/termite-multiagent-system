using UnityEngine;
using System.Collections;

//
//Gerador de madeiras
//
public class GeradorMadeira : MonoBehaviour 
{
	public GameObject prefab;			//Prefab da madeira
	public float spawnTime = 1.0f;		//Tempo entre cada criação de madeira
	public int maxSpawns = 10;			//Quantidade máxima de madeiras a ser criadas
	
	private bool onOff = true;
	private float spawnWidth;
	private float spawnLength;
	
	void Awake()
	{
		spawnWidth = transform.FindChild("Spawn Area").collider.bounds.size.x / 2;
		spawnLength = transform.FindChild("Spawn Area").collider.bounds.size.z / 2;
	}
	
	IEnumerator StartGenerator()
	{	
		//Se não for para randomizar a criação das madeiras, lê as posições do arquivo
		if (Controlador.TestMode)
		{
			LoadWoodPositions();
		}
		//Se for para randomizar a posição de criação de todas as madeiras
		else
		{
			Vector3 pos;
			int spawneds = 0;
			
			//Enquanto estiver ligado o gerador de madeiras, cria 'maxSpawns' madeiras em posições aleatórias no cenário
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
					onOff = false;
					
					StopAllCoroutines();
					
					Debug.Log("Gerador de madeiras parou.");
					
					break;
				}
				
				pos = new Vector3(Random.Range(-spawnWidth, spawnWidth), 1, Random.Range(-spawnLength, spawnLength));
				Instantiate(prefab, pos, Quaternion.identity);	
				spawneds++;
				
				yield return new WaitForSeconds(spawnTime);	
			}
		}
	}
	
	//Liga o gerador
	public void TurnOn()
	{
		onOff = true;
		
		StartCoroutine("StartGenerator");
	}
	
	//Cria as madeiras nas posições salvas no arquivo 'wood_positions.txt'
	private void LoadWoodPositions()
	{
		string text = System.IO.File.ReadAllText("Test Files/wood_positions.txt");
		string []splitted = text.Split('\n');
		Vector3 pos;
		string []vals = {"", "", ""};
		
		maxSpawns = splitted.Length - 1;
		
		//Se não foi criada madeira alguma
		if (GameObject.Find("Madeira(Clone)") == null)
		{
			for (int i = 0; i < maxSpawns; i++)
			{
				vals = splitted[i].Split(',');
				
				pos = new Vector3(float.Parse(vals[0]), float.Parse(vals[1]), float.Parse(vals[2]));
					
				Instantiate(prefab, pos, Quaternion.identity);
			}		
		}
		//Senão, apenas reseta suas posições e rotações
		else
		{
			GameObject []temp = GameObject.FindGameObjectsWithTag("wood");
			
			for (int i = 0; i < maxSpawns; i++)
			{
				vals = splitted[i].Split(',');
				
				pos = new Vector3(float.Parse(vals[0]), float.Parse(vals[1]), float.Parse(vals[2]));
				
				temp[i].transform.position = pos;
				temp[i].transform.rotation = Quaternion.identity;
			}
		}
		
		Debug.Log("Criacao de madeiras acabou.");
	}
	
	//Salva as posições de todas as madeiras que estiverem em tela
	public void SaveWoodPositions()
	{
		GameObject []woods = GameObject.FindGameObjectsWithTag("wood");		
		string vals = "";
		
		if (woods == null) return;
		
		foreach (GameObject item in woods)
			vals += item.transform.position.ToString().Trim('(', ')') + "\r\n";
		
		System.IO.File.WriteAllText("Test Files/wood_positions.txt", vals);
		
		Debug.Log("Posicao das madeiras salvas.");
	}
}
