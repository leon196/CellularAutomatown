using UnityEngine;
using System.Collections;

public class Unit
{
	public GameObject body;
	private float x;
	private float y;
	private bool instantiated;

	public Unit(Vector2 sample, GameObject _body)
	{
		x = sample.x;
		y = sample.y;
		body = _body;
		instantiated = false;
	}

	public void modelize()
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

	public GameObject getBody()
	{
		return body;
	}

	private void setBody(GameObject _body)
	{
		body = _body;
	}

	public bool getInstantiated()
	{
		return instantiated;
	}

	private void setInstantiated(bool _in)
	{
		instantiated = _in;
	}
}