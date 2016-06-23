using UnityEngine;
using System.Collections;

public class UnitScale : MonoBehaviour
{
	public Unit unit;
	float scale;
	float initScale;

	void Start ()
	{
		// GetComponent<Renderer>().material.color = Color.Lerp(Color.gray, Color.white, Mathf.Clamp(0.1f * unit.lifeTime, 0f, 1f));
		initScale = scale = 5f;
	}
	
	void Update ()
	{
		scale = Mathf.Lerp(scale, 0.1f * unit.lifeTime, Time.deltaTime);
		transform.localScale = new Vector3(0.1f, scale, 5f);
		unit.lifeTime = Mathf.Max(unit.lifeTime - Time.deltaTime, 0f);
	}
}