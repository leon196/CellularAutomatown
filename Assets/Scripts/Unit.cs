using UnityEngine;
using System.Collections;

public class Unit
{
	private float x;
	private float y;
	private bool instantiated;
	private string type;

	public Unit(Vector2 sample)
	{
		x = sample.x;
		y = sample.y;
		instantiated = false;
		type = "default";
	}

	public void modelize(GameObject body)
	{
		if (!instantiated)
		{
			Object.Instantiate (body, new Vector3 (x, y, 0), Quaternion.identity);
			instantiated = true;
		}
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