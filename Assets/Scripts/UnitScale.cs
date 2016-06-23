using UnityEngine;
using System.Collections;

public class UnitScale : MonoBehaviour
{
	public Unit unit;
	float scale;

	void Start ()
	{
		GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.gray, Random.Range(0f, 1f));
		scale = 5f;
	}
	
	void Update ()
	{
		scale = Mathf.Lerp(scale, 5f * unit.lifeTime, Time.deltaTime);
		transform.localScale = new Vector3(1f, scale, 5f);
		unit.lifeTime = Mathf.Max(unit.lifeTime - Time.deltaTime, 0f);
	}
}