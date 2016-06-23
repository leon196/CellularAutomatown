using UnityEngine;
using System.Collections;

public class Unit
{
	private float x;
	private float y;
	private Vector2 v;
	private bool instantiated;
	private string type;
	public GameObject instance;
	public float lifeTime;

	public Unit(Vector2 sample)
	{
		x = sample.x;
		y = sample.y;
		v = sample;
		instantiated = false;
		type = "default";
	}

	public void modelize(GameObject body)
	{
		if (!instantiated)
		{
			instance = GameObject.Instantiate (body, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
			instance.AddComponent<UnitScale>();
			instance.GetComponent<UnitScale>().unit = this;
			instance.layer = 5;
			instantiated = true;
		}
	}

	public void demodelize(float lifespan)
	{
		lifeTime = lifespan;
		GameObject.Destroy (instance, lifespan);
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