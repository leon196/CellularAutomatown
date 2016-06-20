using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paint : MonoBehaviour
{
	public Color color = Color.white;
	[Range (1, 10)] public int size = 1;

	Ray ray;
	RaycastHit hitInfo;
	Material material;

	void Start ()
	{
	}

	void Update ()
	{
		if (material)
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo, 100)) 
			{
					// Debug.DrawLine(ray.origin, hitInfo.point);
					UpdatePaint(hitInfo.textureCoord);
			}
			else
			{
				StopPaint();
			}
		}
		else
		{
			CellularAutomaton cellularAutomaton = GameObject.FindObjectOfType<CellularAutomaton>();
			material = cellularAutomaton.materialCellularAutomaton;
		}
	}

	void UpdatePaint (Vector2 point)
	{
		material.SetFloat("_ShouldPaint", 1f);
		material.SetFloat("_PaintSize", size);
		material.SetColor("_PaintColor", color);
		material.SetVector("_PaintPosition", point);
	}

	void StopPaint ()
	{
		material.SetFloat("_ShouldPaint", 0f);
	}
}