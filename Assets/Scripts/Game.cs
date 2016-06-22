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

	[Range (1, 50)] public float width = 10;
	[Range (1, 50)] public float height = 10;
	[Range (0f, 3f)] public float radius = 0.3f;
	[Range (0.03f, 1f)] public float lifespan = 0.1f;

	private MyPDisc mpd;

	// Use this for initialization
	void Start ()
	{
		mpd = new MyPDisc ();
		Debug.Log ("<-=  Game Launched  =->\n");

		sampler = new PoissonDiscSampler(width, height, 0.3f);
		unitList = new List<Unit>();
		/*
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

		foreach (Unit unit in unitList)
		{
			//unit.modelize (this.getBody(unit.getType()));
			//unit.demodelize (lifespan);
		/*}
		Instantiate (_city, new Vector3 (10, 10, 0), Quaternion.identity);
		Instantiate (_city, new Vector3 (15,15, 0), Quaternion.identity);
		Instantiate (_city, new Vector3 (20, 20, 0), Quaternion.identity);
		Instantiate (_city, new Vector3 (50, 20, 0), Quaternion.identity);

		float x = Random.Range(-10.0f, 10.0f);
		float y = Random.Range(-10.0f, 10.0f);
		unitList.Add (new City (new Vector2 (x, y)));

		for (int i = 0; i < 100000; i++) {
			x = Random.Range (-10.0f, 10.0f);
			y = Random.Range (-10.0f, 10.0f);
			if (mpd.nearObjects (new Vector3 (x, y, 0), 1.5f))
			{
				unitList.Add (new City (new Vector2 (x, y)));
			}
			else
				i += 100;
		}
		*/

		sampler = new PoissonDiscSampler (1000, 1000, 10f);
		unitList = new List<Unit> ();

		foreach (Vector2 sample in sampler.Samples())
		{
			unitList.Add (new Unit (sample));
		}
	}
	
	void Update ()
	{
		foreach (Unit unit in unitList)
		{
			unit.modelize (this.getBody(unit.getType()));
			//unit.demodelize (lifespan);
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
