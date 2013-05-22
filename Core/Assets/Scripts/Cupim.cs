using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Cupim : MonoBehaviour
{
	public Transform hand;
	public bool isCarryingWood = false;
	public bool wandering = false;
	
	public float rotationDamping = 40;
	public float directionChangeInterval = 1f;
	public float maxHeadingChange = 90;
	
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
		hand = transform.Find("hand");
	}
	
	void OnEnable()
	{
		wandering = true;	
		StartCoroutine("NewHeading");
	}
	
	void OnDisable()
	{
		wandering = false;	
		StopAllCoroutines();
	}
	
//	void Start()
//	{
//		wandering = true;
//		StartCoroutine("NewHeading");
//	}
	
	void FixedUpdate()
	{
		if (wandering)
		{
			_transform.eulerAngles = Vector3.Slerp(_transform.eulerAngles, _targetRotation, Time.deltaTime * directionChangeInterval);
			Vector3 forward = _transform.TransformDirection(Vector3.forward);
			_controller.SimpleMove(forward * Controlador.MoveSpeed);	
		}
		else
		{
			wandering = true;
			
			StartCoroutine("NewHeading");
		}
	}	
	
	private IEnumerator NewHeading ()
	{
		while (wandering)
		{
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
	
	private void NewHeadingRoutine ()
	{
		int floor = (int)Mathf.Clamp(_heading - maxHeadingChange, 0, 360);
		int ceil  = (int)Mathf.Clamp(_heading + maxHeadingChange, 0, 360);
//		float floor = _heading - maxHeadingChange;
//		float ceil = _heading + maxHeadingChange;
		_heading = Random.Range(floor, ceil);
		_targetRotation = new Vector3(0, _heading, 0);
	}
	
	public void WallHit(Vector3 newDir)
	{
		_transform.rotation = Quaternion.Euler(newDir);
		_heading = _transform.eulerAngles.y;
		
		wandering = false;
			
		StopCoroutine("NewHeading");
	}
	
	public void WoodFound(Transform wood)
	{
		if (hand.GetChildCount() == 0)
		{
			wood.position = hand.position;
			wood.rotation = hand.rotation;
			wood.rigidbody.isKinematic = true;
			wood.rigidbody.useGravity = false;
			wood.collider.enabled = false;
			
			wood.parent = hand;
		}
		else
		{
			Transform w = hand.GetChild(0);
		
			w.rigidbody.isKinematic = false;
			w.rigidbody.useGravity = true;
			w.collider.enabled = true;
			
			w.parent = null;
			
			_transform.Rotate(0, 180, 0);
			_heading = _transform.eulerAngles.y;
			
			wandering = false;
			
			StopCoroutine("NewHeading");
		}
	}
}
