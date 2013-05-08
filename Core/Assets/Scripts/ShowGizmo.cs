using UnityEngine;
using System.Collections;

public class ShowGizmo : MonoBehaviour 
{
	public bool showWireframe = false;
	public float radius = 0.1f;
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.grey;
		
		if (showWireframe)
			Gizmos.DrawWireSphere(transform.position, radius);	
		else 
			Gizmos.DrawSphere(transform.position, radius);	
	}
}
