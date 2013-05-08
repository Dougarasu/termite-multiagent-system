using UnityEngine;
using System.Collections;

public class Bundle : MonoBehaviour 
{
	//Estrutura para qualquer mensagem entre os cupins
	public struct InfoMsg				
	{
		private int _amount;
		private Vector3 _pos;
		
		public int Amount
		{
			get { return this._amount; }
			set { _amount = value; }
		}

		public Vector3 Pos {
			get { return this._pos; }
			set { _pos = value; }
		}
		
		public InfoMsg(int q, Vector3 p)
		{
			this._amount = q;
			this._pos = p;
		}
	}
	
	public int woodAmount = 0;			//Qtde de madeiras do monte
	
	private Transform _transform;
	private SphereCollider _collider;
	
	void Start()
	{
		_transform = transform;
		_collider = GetComponent<SphereCollider>();
	}
	
	/// <summary>
	/// Função que mostra os gizmos na tab Scene.
	/// </summary>
	void OnDrawGizmos()
	{
		//Gizmo da 'mão' do cupim
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("sensor"))
		{			
			InfoMsg i = new InfoMsg(woodAmount, transform.position);
			
			col.SendMessageUpwards("Receiver", i);
		}
		else if (col.name == "Wood")
		{
			col.transform.parent = transform;	
		}
	}
	
	//Receptor de mensagens do sensor do cupim
	public void Receiver(Transform wood)
	{
		wood.parent = _transform;
		woodAmount++;
		_collider.radius += 0.2f;
	}
}
