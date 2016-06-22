using UnityEngine;
using System.Collections;

public class MyPDisc
{
	private Collider[] colliderList;

	public bool nearObjects(Vector3 xyz, float radius)
	{
		colliderList = Physics.OverlapSphere (xyz, radius);

		if (colliderList.Length == 0)
			return true;
		else
			return false;

/*		foreach (Collider unitCollide in colliderList)
		{
			Debug.Log (unitCollide.gameObject.GetInstanceID());
		}*/
	}
}