using UnityEngine;
using System.Collections;

public class MyPDisc
{
	private Collider[] colliderList;

	public bool nearObjects(Vector2 xy, float radius)
	{
		colliderList = Physics.OverlapSphere (xy, radius);

		if (colliderList.Length == 0)
			return true;
		else
			return false;

/*		foreach (Collider unitCollide in colliderList)
		{
			Debug.Log (unitCollide.gameObject.GetInstanceID());
		}*/
	}

	public Vector2 getTrans()
	{
		Vector2 v2 = Random.insideUnitCircle * 15;
//		Vector3 v3 = new Vector3 (v2.x, 0, v2.y);
//		Debug.Log (v2.x.ToString()+"  XX  "+v2.y.ToString());
		return v2;
	}

	public Vector2 findPosition(Vector2 xy)
	{
		Vector2 newPos = xy + getTrans ();

		if (nearObjects (newPos, 5))
		{
			return newPos;
		}
		else
			return xy;
	}
}