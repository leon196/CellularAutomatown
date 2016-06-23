using UnityEngine;
using System.Collections;

public class City: Unit
{
	public City(Vector2 sample) : base(sample)
	{
		setType("city");
	}
}