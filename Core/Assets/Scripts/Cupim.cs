using UnityEngine;
using System.Collections;

//
//Classe do cupim que executa todas as funções e regras impostas no simulador.
//
[RequireComponent(typeof(CharacterController))]
public class Cupim : MonoBehaviour
{
	public float rotationDamping = 40;				//Amortecimento de rotação
	public float directionChangeInterval = 1f;		//Intervalo de tempo entre cada troca de direção (em segundos)
	public float maxHeadingChange = 90;				//Intervalo máximo para troca de direção, positivo e negativo a partir da direção atual do cupim
	
	private bool _wandering = false;
	private Transform _hand;
	private float _heading;
	private Vector3 _targetRotation;
	private Transform _transform;
	private CharacterController _controller;
	
	void Awake()
	{
		_controller = GetComponent<CharacterController>();
		_transform = transform;
		_transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
		_heading = _transform.rotation.eulerAngles.y;
		
		_hand = transform.Find("hand");
	}
	
	void OnEnable()
	{
		_wandering = true;	
		StartCoroutine("NewHeading");
	}
	
	void OnDisable()
	{
		_wandering = false;	
		StopAllCoroutines();
	}
	
	void FixedUpdate()
	{
		//Se cupim estiver vagando pelo cenário, randomiza a direção de movimento 
		if (_wandering)
		{
			_transform.eulerAngles = Vector3.Slerp(_transform.eulerAngles, _targetRotation, Time.deltaTime * directionChangeInterval);
			Vector3 forward = _transform.TransformDirection(Vector3.forward);
			_controller.SimpleMove(forward * Controlador.MoveSpeed);	
		}
		//Senão, começa a andar aleatoriamente pelo cenário
		else
		{
			_wandering = true;
			
			StartCoroutine("NewHeading");
		}
	}	
	
	//Executa a aleatorização do cupim a cada intervalo de troca
	private IEnumerator NewHeading ()
	{
		while (_wandering)
		{
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
	
	//Randomiza a direção do cupim
	private void NewHeadingRoutine ()
	{
		int floor = (int)Mathf.Clamp(_heading - maxHeadingChange, 0, 360);
		int ceil  = (int)Mathf.Clamp(_heading + maxHeadingChange, 0, 360);
		
		_heading = Random.Range(floor, ceil);
		_targetRotation = new Vector3(0, _heading, 0);
	}
	
	//Recebe a nova direção, mensagem vinda do sensor de parede
	public void WallHit(Vector3 newDir)
	{
		_transform.rotation = Quaternion.Euler(newDir);
		_heading = _transform.eulerAngles.y;
		
		_wandering = false;
			
		StopCoroutine("NewHeading");
	}
	
	//Recebe o transform da madeira, mensagem vinda do sensor de mandeira
	public void WoodFound(Transform wood)
	{
		if (_hand.GetChildCount() == 0)
		{
			wood.position = _hand.position;
			wood.rotation = _hand.rotation;
			wood.rigidbody.isKinematic = true;
			wood.rigidbody.useGravity = false;
			wood.collider.enabled = false;
			
			wood.parent = _hand;
		}
		else
		{
			Transform w = _hand.GetChild(0);
		
			w.rigidbody.isKinematic = false;
			w.rigidbody.useGravity = true;
			w.collider.enabled = true;
			
			w.parent = null;
			
			_transform.Rotate(0, 180, 0);
			_heading = _transform.eulerAngles.y;
			
			_wandering = false;
			
			StopCoroutine("NewHeading");
		}
	}
	
	//Deixa madeira no cenário
	public void DropWood()
	{
		_hand.DetachChildren();
	}	
}
