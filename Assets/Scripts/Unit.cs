using UnityEngine;
using System.Collections;

public class Unit
{
	private float x;
	private float y;
	private Vector2 v;
	private bool instantiated;
	private string type;
	private Object instance;
	private float lifespan;


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
}