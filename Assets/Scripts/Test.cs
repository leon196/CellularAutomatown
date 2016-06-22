using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Test : MonoBehaviour {

	public GameObject someObject;
	PoissonDiscSampler sampler;
	List<Unit> unitList;

	// Use this for initialization
	void Start ()
	{
		sampler = new PoissonDiscSampler(10, 10, 0.3f);
		unitList = new List<Unit>();

		foreach (Vector2 sample in sampler.Samples())
		{
			unitList.Add(new Unit(sample, someObject));
		}
	}
	
	void Update ()
	{
		foreach (Unit unit in unitList)
		{
			unit.modelize ();
		}
	}
}
