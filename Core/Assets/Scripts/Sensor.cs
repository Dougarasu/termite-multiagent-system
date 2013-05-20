using UnityEngine;
using System.Collections;

public class Sensor : MonoBehaviour 
{
	private Termite _agent;		// Cupim mais acima da hierarquia
	private Transform _bundle;
	private bool _ready = false;
	
	void Start()
	{
		_agent = transform.parent.GetComponent<Termite>();	
	}
	
	void OnTriggerEnter(Collider col)
	{
		//Encontrou um monte, então envia mensagem para o Cupim
		if (col.CompareTag("bundle"))
		{
			StartCoroutine("SendWood", col.transform);	
		}
	}
	
	//Receptor de mensagens do monte
	public void Receiver(Bundle.InfoMsg msg)
	{		
		_agent.bundleCount = msg.Amount;
		_agent.bundlePos = msg.Pos;
		
		_ready = true;
	}
	
	//TODO: arrumar a segunda tentativa de dropar madeira.
	private IEnumerator SendWood(Transform t)
	{
		while (!_ready) yield return null;
		
		Transform w = _agent.DropWood();
		
		t.SendMessage("Receiver", w);
		
		_ready = false;
		
		StopCoroutine("SendWood");
	}
}
