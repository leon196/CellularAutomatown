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

	public Vector2 getTrans(float radius)
	{
		Vector2 v2 = Random.insideUnitCircle * radius;
//		Vector3 v3 = new Vector3 (v2.x, 0, v2.y);
//		Debug.Log (v2.x.ToString()+"  XX  "+v2.y.ToString());
		return v2;
	}

	public Vector2 findPosition(Vector2 xy)
	{
		Vector2 newPos = xy + getTrans (15);

		if (nearObjects (newPos, 5))
		{
			return newPos;
		}
		else
			return xy;
	}

	public Vector2 findPosition(Vector2 xy, float transRadius)
	{
		Vector2 newPos = xy + getTrans (transRadius);
		float dist = Vector2.Distance (xy, newPos);
/*		float tmp = dist;
		dist *= dist * dist;
		dist /= tmp;
		dist /= 100; */
//		dist = dist * dist / 100;
		dist = convert(dist/100);
		if (nearObjects (newPos, dist))
		{
			Debug.Log ("distance = "+dist);
			return newPos;
		}
		else
			return xy;
	}

	public float convert(float v)
	{
		float mult = (Mathf.Pow ((float)2, (float)v));
		return 0.125f*mult;
	}

	public Vector2 findPosition(Vector2 xy, float collisionRadius, float transRadius)
	{
		Vector2 newPos = xy + getTrans (transRadius);

		if (nearObjects (newPos, collisionRadius))
		{
			return newPos;
		}
		else
			return xy;
	}
}