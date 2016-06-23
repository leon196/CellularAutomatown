using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour {

	public GameObject _default;
	public GameObject _city;

	PoissonDiscSampler sampler;
	List<Unit> unitList;
	List<Unit> activeList;

	public Texture2D texture2D;
	private Camera orthoCamera;

	private bool flag = true;

	[Range (1, 50)] public float width = 10;
	[Range (1, 50)] public float height = 10;
	[Range (0f, 3f)] public float radius = 0.3f;
	public float lifespan = 100.0f;

	private MyPDisc mpd;

	private Water water;

	// Use this for initialization
	void Start ()
	{
		mpd = new MyPDisc ();
		Debug.Log ("<-=  Game Launched  =->\n");

		water = GameObject.FindObjectOfType<Water>();
		texture2D = new Texture2D(Engine.width, Engine.height);
		orthoCamera = GetComponent<Camera>();

		//sampler = new PoissonDiscSampler(width, height, 0.3f);
		//unitList = new List<Unit>();
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


		sampler = new PoissonDiscSampler (1000, 1000, 10f);*/
		unitList = new List<Unit> ();
		unitList.Add (new Unit (new Vector2 (12.0f, 12.0f)));
		activeList = unitList;
		/*foreach (Vector2 sample in sampler.Samples())
		{
			unitList.Add (new Unit (sample));
		}*/
	}

	void Update ()
	{
		RenderTexture.active = water.output;
		texture2D.ReadPixels(new Rect(0, 0, water.output.width, water.output.height), 0, 0);
		texture2D.Apply();

		Vector2 randUnit = activeList[Random.Range(0, activeList.Count())].getV();
		int k = 0;
		while (k < 10) {
			Vector2	newPos = mpd.findPosition (randUnit);
//		Debug.Log ("randUnit: "+randUnit.x.ToString()+"  \\  "+randUnit.y.ToString()+"\nnewPos: "+newPos.x.ToString()+"  \\  "+newPos.y.ToString());
//		Debug.Log(unitList.Count());
			Unit dat = new Unit (newPos);

			Vector2 viewportPos = orthoCamera.WorldToViewportPoint(newPos);
			Color textureColor = texture2D.GetPixel((int)(viewportPos.x * texture2D.width),(int)(viewportPos.y * texture2D.height));
			float luminance = Mathf.Clamp(textureColor.g - textureColor.b - textureColor.r, 0f, 1f);

			if (newPos != randUnit) {
				unitList.Add (dat);
				activeList.Add (dat);
				dat.modelize (this.getBody (dat.getType ()));
				dat.demodelize (30f * luminance);
				break;
			} else {
				k += 1;
				if (k >= 10) {
					activeList.Remove (dat);
					randUnit = activeList[Random.Range(0, activeList.Count())].getV();
					k = 0;
					//Debug.Log("Failed to place point after 20 attempts");
				}
			}
		}
//		Debug.Log (Random.Range (0, unitList.Count ()));
		/*foreach (Unit unit in unitList) {
			unit.modelize (this.getBody (unit.getType ()));
			unit.demodelize (lifespan);
			if (unitList.Count () > 1 && ifps == 60) {
				unitList.Remove (unit);
				ifps = 0;
			}
		}
		int i = 0;
		while (i < unitList.Count()) {
			Unit unit = unitList [i++];
			unit.modelize (this.getBody (unit.getType ()));
			unit.demodelize (lifespan);
			if (unitList.Count () > 1) {
				unitList.Remove (unit);
			}
		}
		*/
	}

	public GameObject getBody(string type)
	{
		if (type == "city")
			return _city;
		else
			return _default;
	}
}
