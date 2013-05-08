using UnityEngine;
using System.Collections;

public class ShowGizmo : MonoBehaviour 
{
	void OnDrawGizmos()
	{
		Gizmos.color = Color.grey;
		Gizmos.DrawSphere(transform.position, 0.1f);	
	}
}
