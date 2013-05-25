using UnityEngine;
using System.Collections;
using System.IO;

// 
//Controlador geral do simulador.
//Os controles estão descritos abaixo:
//		Q			Aumenta velocidade geral dos cupins
//		W			Diminui velocidade geral dos cupins
//		A			Liga todos os geradores de cupins em cena
//		S			Para a movimentação de todos os cupins em cena
//		D			Continua a movimentação de todos os cupins em cena
//		Z			Salva as posições das madeiras em cena, num arquivo no disco
//
public class Controlador : MonoBehaviour 
{
	public static float MoveSpeed = 10;		//Velocidade de todos os cupins na tela
	public static bool TestMode;			//Condição do modo de teste
	public static bool TermitesOn = false;	//Condição de ativação dos cupins na tela
	
	public bool testModeOn = false;			//Condição de ativação dos cupins na tela
	public float speedAdd = 5;				//Acréscimo de velocidade para os cupins
	public int testAmount = 10;				//Quantidade de testes que serão feitos
	public int testInterval = 600;			//Tempo entre cada teste (em segundos)
	public GeradorMadeira _woodGen;
	public GeradorCupim []_termiteGen;
	
	void Awake()
	{
		TestMode = testModeOn;
	}
	
	IEnumerator Start()
	{
		if (TestMode)
		{
			GameObject.Find("Display").GetComponent<DisplayControls>().ShowTestDisplay();
			
			int sssCnt = 0;
			
			//Tira uma printscreen a cada intervalo de teste até que todos os testes sejam feitos
			while (sssCnt <= testAmount)
			{
				StartCoroutine("ScreenshotEncode", sssCnt++);
				Debug.Log(">>> ScreenShot " + sssCnt + " tirada.");
				
				yield return new WaitForSeconds(testInterval);
			}		
			
			StopAllCoroutines();
			Debug.Log("Teste finalizado.");
			
			TestMode = false;
			GameObject.Find("Display").GetComponent<DisplayControls>().ShowNormalDisplay();
		}
		else
		{
			GameObject.Find("Display").GetComponent<DisplayControls>().ShowNormalDisplay();
		}
	}
	
	void Update()
	{
		if (!TestMode)
		{
			//Aumenta velocidade geral dos cupins
			if (Input.GetKeyDown(KeyCode.Q))
				MoveSpeed += speedAdd;
			
			//Diminui velocidade geral dos cupins
			if (Input.GetKeyDown(KeyCode.W))
				MoveSpeed -= speedAdd;
			
			//Liga todos os geradores de cupins em cena
			if (Input.GetKeyDown(KeyCode.A))
			{
				foreach (GeradorCupim item in _termiteGen)
					item.TurnOn();
			}
			
			//Cria novas madeiras
			if (Input.GetKeyDown(KeyCode.X))
			{
				_woodGen.TurnOn();
			}
			
			//Para a movimentação de todos os cupins em cena
			if (Input.GetKeyDown(KeyCode.S))
			{
				GameObject []termites = GameObject.FindGameObjectsWithTag("termite");	
				
				foreach (GameObject item in termites)
					item.GetComponent<Cupim>().enabled = false;
			}
			
			//Continua a movimentação de todos os cupins em cena
			if (Input.GetKeyDown(KeyCode.D))
			{
				GameObject []termites = GameObject.FindGameObjectsWithTag("termite");	
				
				foreach (GameObject item in termites)
					item.GetComponent<Cupim>().enabled = true;
			}
			
			//Salva as posições das madeiras em cena, num arquivo no disco
			if (Input.GetKeyDown(KeyCode.Z))
				GameObject.Find("GeradorMadeiras").GetComponent<GeradorMadeira>().SaveWoodPositions();
		}
	}
	
	//Inicia um teste
	private IEnumerator StartTest()
	{
		//Destrói todos os cupins em cena
		GameObject []c = GameObject.FindGameObjectsWithTag("termite");
		
		if (c != null)
		{
			foreach (GameObject item in c)
			{
				item.GetComponent<Cupim>().DropWood();
				
				DestroyImmediate(item);
			}
		}
		
		//Liga os geradores
		_woodGen.TurnOn();
		
		yield return new WaitForSeconds(2);
		foreach (GeradorCupim item in _termiteGen)
			item.TurnOn();		
	}
	
	//Tira o printscreen da tela
	IEnumerator ScreenshotEncode(int count)
    {
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
 
        // create a texture to pass to encoding
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
 
        // put buffer into texture
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
 
        // split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;
 
        byte[] bytes = texture.EncodeToPNG();
 
        // save our test image (could also upload to WWW)
        File.WriteAllBytes("Test Files/ScreenShots/screenshot" + count + ".jpg", bytes);
        count++;
 
        // Added by Karl. - Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        DestroyObject( texture );
 
        //Debug.Log( Application.dataPath + "/../testscreen-" + count + ".png" );
		
		StartCoroutine("StartTest");
    }
}
