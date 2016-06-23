using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {

	public GameObject _default;
	public GameObject _city;

	public GameObject _root;
	public GameObject _trunk;

	PoissonDiscSampler sampler;
	List<Unit> unitList;
	List<Unit> activeList;

	List<Unit> trees;


	private bool flag = true;

	[Range (1, 50)] public float width = 10;
	[Range (1, 50)] public float height = 10;
	[Range (0f, 3f)] public float radius = 0.3f;
	public float lifespan = 100.0f;

	private MyPDisc mpd;

	//private Rect rect;

	// Use this for initialization
	void Start ()
	{
		//rect = new Rect (-100, -100, 200, 200);
		mpd = new MyPDisc ();
		Debug.Log ("<-=  Game Launched  =->\n");
		unitList = new List<Unit> ();
		unitList.Add (new Unit (new Vector2 (12.0f, 12.0f)));
		activeList = unitList;

		trees = new List<Unit> ();


	}

	void Update ()
	{
		algo3 ();
	}

	void algo3()
	{
		
		Vector2 center = new Vector2 (0, 0);
		float count = 0;
		//Unit tree = new Unit (new Vector2 ((float)Random.Range (Random.Range (-150, 150), Random.Range (-150, 150)), (float)Random.Range (-150, 150)));

		foreach (Unit house in unitList)
		{
			center += house.getV ();
			count += 1;
		}
		center /= count;

		Vector2	newPos = mpd.findPosition (center, 200.0f);
		//		Debug.Log ("randUnit: "+randUnit.x.ToString()+"  \\  "+randUnit.y.ToString()+"\nnewPos: "+newPos.x.ToString()+"  \\  "+newPos.y.ToString());
		//		Debug.Log(unitList.Count());
		Unit dat = new Unit (newPos);
		if (newPos != center) {
			//if (rect.Contains (newPos)) {
			unitList.Add (dat);
			dat.modelize (this.getBody (dat.getType ()));
			//dat.demodelize ((float)Random.Range (5, 40));
			//}
		//	}
		}
	}

	public float convert(float v)
	{
		float mult = (Mathf.Pow ((float)2, (float)v / 12));
		return 0.125f*mult;
	}

	void algo2()
	{
		Unit tree = new Unit (new Vector2 ((float)Random.Range (Random.Range (-150, 150), Random.Range (-150, 150)), (float)Random.Range (-150, 150)));

		Vector2 randUnit = activeList[Random.Range(0, activeList.Count())].getV();
		int k = 0;
		int c = 0;
		while (k < 10) {
			Vector2	newPos = mpd.findPosition (randUnit);
			//		Debug.Log ("randUnit: "+randUnit.x.ToString()+"  \\  "+randUnit.y.ToString()+"\nnewPos: "+newPos.x.ToString()+"  \\  "+newPos.y.ToString());
			//		Debug.Log(unitList.Count());
			Unit dat = new Unit (newPos);
			if (newPos != randUnit) {
				//if (rect.Contains (newPos)) {
				unitList.Add (dat);
				activeList.Add (dat);
				dat.modelize (this.getBody (dat.getType ()));
				dat.demodelize ((float)Random.Range (5, 40));
				//}
				c += 1;
				if (c > 2)
					return;
			} else {
				k += 1;
				if (k >= 10) {
					activeList.Remove (dat);
					randUnit = activeList[Random.Range(0, activeList.Count())].getV();
					k = 0;
					trees.Add (tree);
					tree.modelize (getBody ("root"));
					//Debug.Log("Failed to place point after 20 attempts");
				}
			}
		}
	}

	void algo1()
	{
		Unit tree = new Unit (new Vector2 ((float)Random.Range (Random.Range (-150, 150), Random.Range (-150, 150)), (float)Random.Range (-150, 150)));

		Vector2 randUnit = activeList[Random.Range(0, activeList.Count())].getV();
		int k = 0;
		while (k < 10) {
			Vector2	newPos = mpd.findPosition (randUnit);
			//		Debug.Log ("randUnit: "+randUnit.x.ToString()+"  \\  "+randUnit.y.ToString()+"\nnewPos: "+newPos.x.ToString()+"  \\  "+newPos.y.ToString());
			//		Debug.Log(unitList.Count());
			Unit dat = new Unit (newPos);
			if (newPos != randUnit) {
				//if (rect.Contains (newPos)) {
				unitList.Add (dat);
				activeList.Add (dat);
				dat.modelize (this.getBody (dat.getType ()));
				dat.demodelize ((float)Random.Range (5, 40));
				//}
				return;
			} else {
				k += 1;
				if (k >= 10) {
					activeList.Remove (dat);
					randUnit = activeList[Random.Range(0, activeList.Count())].getV();
					k = 0;
					trees.Add (tree);
					tree.modelize (getBody ("root"));
					//Debug.Log("Failed to place point after 20 attempts");
				}
			}
		}
	}

	public GameObject getBody(string type)
	{
		if (type == "city")
			return _city;
		else if (type == "root")
			return _root;
		else if (type == "trunk")
			return _trunk;
		else
			return _default;
	}
}
