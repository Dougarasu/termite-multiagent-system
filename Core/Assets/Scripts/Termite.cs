using UnityEngine;
using System.Collections;

public class Termite : MonoBehaviour
{
	public enum State
	{
		Sleeping,
		Init,
		Searching,
		Carrying
	}
	
	public State _state;
	public Transform hand;
	public Transform targetWood;
	public Vector3 bundlePos;
	public int bundleCount = 0;

	public float moveSpeed = 2;
	public float rotationDamping = 40;
	public float directionChangeInterval = 1f;
	public float maxHeadingChange = 90;
 
	private CharacterController _controller;
	private float _heading;
	private Vector3 _targetRotation;
	private Transform _transform;
	
	void Awake()
	{
		_state = State.Init;	
	}
	
	IEnumerator Start () 
	{
		//Finite State Engine
		while (true) 
		{
			switch (_state) 
			{
				case State.Init:
					Initialize();
					break;
					
				case State.Sleeping:
					Sleeping();
					break;
				
				case State.Searching:
					Searching();
					break;
				
				case State.Carrying:
					Carrying();
					break;
				
				default:
					break;
			}
			
			//Ao invés de repetir a cada instante, repete a cada frame
			yield return null;
		}
	}
	
	private void Initialize()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (hand == null)
				hand = transform.FindChild("hand");
			
			_controller = GetComponent<CharacterController>();
			_transform = transform;
			
			//TODO: uso provisório da posição do monte.
			bundlePos = transform.position;
			
			_state = State.Sleeping;
		}
	}
	
	private void Searching()
	{
		//Anda aleatório pelo cenário a procura de uma madeira
		if (!targetWood)
		{
			_transform.eulerAngles = Vector3.Slerp(_transform.eulerAngles, _targetRotation, Time.deltaTime * directionChangeInterval);
			Vector3 forward = _transform.TransformDirection(Vector3.forward);
			_controller.SimpleMove(forward * moveSpeed);
		}
		//Vai em direção à madeira alvo
		else 
		{	
			Quaternion targetRotation = Quaternion.LookRotation(targetWood.position - _transform.position, Vector3.up); 
			_transform.LookAt(targetWood.position);
			_controller.SimpleMove(moveSpeed * _transform.forward);
			
			//Verifica se aproximou o suficiente da madeira para poder pegá-la
			if (Vector3.Distance(targetWood.position, _transform.position) < 1.2f)
			{	
				targetWood.position = hand.position;
				targetWood.rotation = _transform.rotation;
				targetWood.parent = hand;
				targetWood = null;
				
				_state = State.Carrying;
			}
		}
	}
	
	private void Sleeping()
	{
		//Configura uma rotação aleatória inicial
		_heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, _heading, 0);
		
		_state = State.Searching;
		
		StartCoroutine(NewHeading());
	}
	
	private IEnumerator NewHeading ()
	{
		while (!targetWood) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
	
	private void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(_heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(_heading + maxHeadingChange, 0, 360);
		_heading = Random.Range(floor, ceil);
		_targetRotation = new Vector3(0, _heading, 0);
	}
	
	private void Carrying()
	{		
		Quaternion targetRotation = Quaternion.LookRotation(bundlePos - _transform.position, Vector3.up); 
		_transform.LookAt(bundlePos);
		_controller.SimpleMove(moveSpeed * _transform.forward);
		
		//Verifica se aproximou o suficiente da madeira para poder pegá-la
		if (Vector3.Distance(bundlePos, _transform.position) < 1f)
		{	
			hand.GetChild(0).rigidbody.isKinematic = false;
			hand.GetChild(0).rigidbody.useGravity = true;
			hand.DetachChildren();
			
			_state = State.Sleeping;
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);	
	}
	
	void OnTriggerEnter(Collider col)
	{
		//Estado de procura por madeira
		if (!targetWood && _state == State.Searching)
		{
			//Se uma madeira foi encontrada, vai em direção a ela
			if (col.CompareTag("wood"))
			{
				targetWood = col.transform;
				
				targetWood.rigidbody.isKinematic = true;
				targetWood.rigidbody.useGravity = false;
				
				StopCoroutine("NewHeading");
			}
		}
	}
}

