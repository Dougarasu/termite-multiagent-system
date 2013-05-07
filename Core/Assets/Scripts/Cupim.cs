using UnityEngine;
using System.Collections;

public class Cupim : MonoBehaviour
{
	public enum State
	{
		Idle,
		Initialize,
		Setup,
		//Actions
		WakeUp
	}
	
	public State _state;
	
	void Awake()
	{
		_state = State.Initialize;	
	}
	
	IEnumerator Start () 
	{
		//Finite State Engine
		while (true) 
		{
			switch (_state) 
			{
				case State.Initialize:
					Initialize();
					break;
					
				case State.Setup:
					Setup();
					break;
					
				case State.WakeUp:
					WakeUp();
					break;
				
				default:
					break;
			}
			
			//Ao invés de repetir a cada instante, repete a cada frame
			yield return 0;
		}
	}
	
	private void Initialize()
	{
		//lógica
		
		_state = State.Setup;
	}
	
	private void Setup()
	{
		//lógica
		GetComponent<Wander>().enabled = false;
		_state = State.WakeUp;
		
	}
	
	private IEnumerator WakeUp()
	{
		//lógica
		yield return new WaitForSeconds(3);
		
		Debug.Log("foi");
		GetComponent<Wander>().enabled = true;
		
		_state = State.Idle;
	}
}

