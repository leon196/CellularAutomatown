using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {

	public GameObject _default;
	public GameObject _city;

	PoissonDiscSampler sampler;
	List<Unit> unitList;

	private bool flag = true;

	// Use this for initialization
	void Start ()
	{
		sampler = new PoissonDiscSampler(10, 10, 0.3f);
		unitList = new List<Unit>();

		foreach (Vector2 sample in sampler.Samples())
		{
			if (flag)
			{
				unitList.Add (new Unit (sample));
				flag = false;
			}
			else
			{
				unitList.Add (new City (sample));
				flag = true;
			}
		}
	}
	
	void Update ()
	{
		foreach (Unit unit in unitList)
		{
			unit.modelize (this.getBody(unit.getType()));
		}
	}

	public GameObject getBody(string type)
	{
		if (type == "city")
			return _city;
		else
			return _default;
	}
}
