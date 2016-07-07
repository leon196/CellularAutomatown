using UnityEngine;
using System.Collections;

public class Unit
{
	private float x;
	private float y;
	private Vector2 v;
	private bool instantiated;
	private string type;
	public Object instance;
	private float lifespan;
	public float age;


	public Unit(Vector2 sample)
	{
		x = sample.x;
		y = sample.y;
		v = sample;
		instantiated = false;
		type = "default";
	}

	public Unit(Vector2 sample, float ls)
	{
		x = sample.x;
		y = sample.y;
		v = sample;
		instantiated = false;
		type = "default";
		lifespan = ls;
	}

	public void modelize(GameObject body)
	{
		if (!instantiated)
		{
			instance = Object.Instantiate (body, new Vector3 (x, y, 0), Quaternion.identity);
			instantiated = true;
		}
	}

	public void demodelize(float lifespan)
	{
		Object.Destroy (instance, lifespan);
		instantiated = false;
	}

	public bool nearObjects(Vector2 xy, float radius)
	{
		Collider[] colliderList;
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

	public float getX()
	{
		return x;
	}

	private void setX(float _x)
	{
		x = _x;
	}

	public float getLifespan()
	{
		return lifespan;
	}

	private void setLifespan(float _lifespan)
	{
		lifespan = _lifespan;
	}

	public float getY()
	{
		return y;
	}

	private void setY(float _y)
	{
		y = _y;
	}

	public Vector2 getV()
	{
		return v;
	}

	private void setV(Vector2 _v)
	{
		v = _v;
	}

	public bool getInstantiated()
	{
		return instantiated;
	}

	private void setInstantiated(bool _in)
	{
		instantiated = _in;
	}

	public string getType()
	{
		return type;
	}

	public void setType(string _type)
	{
		type = _type;
	}

	public float getAge()
	{
		return age;
	}

	public void setAge(float _age)
	{
		age = _age;
	}
	public Object getInstance()
	{
		return instance;
	}
}