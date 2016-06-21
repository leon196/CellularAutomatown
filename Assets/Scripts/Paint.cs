using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paint : MonoBehaviour
{
	public Color color = Color.white;
	[Range (1, 10)] public int size = 1;
	// public Texture background;

	Ray ray;
	RaycastHit hitInfo;
	Material material;

	CellularAutomaton cellularAutomaton;

	void Start ()
	{
		cellularAutomaton = GetComponent<CellularAutomaton>();
	}

	void Update ()
	{
		if (material)
		{
			UpdatePaint(hitInfo.textureCoord);

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo, 100)) 
			{
				if (hitInfo.transform.gameObject == this.gameObject)
				{
					StartPaint();
				}
				else
				{
					StopPaint();
				}
			}
			else
			{
				StopPaint();
			}
		}
		else
		{
			// cellularAutomaton.Print(background);
			material = cellularAutomaton.materialCellularAutomaton;
		}
	}

	void StartPaint ()
	{
		material.SetFloat("_ShouldPaint", 1f);
	}

	void UpdatePaint (Vector2 point)
	{
		// material.SetTexture("_Background", background);
		material.SetColor("_PaintColor", color);
		material.SetFloat("_PaintSize", size);
		material.SetVector("_PaintPosition", point);
	}

	void StopPaint ()
	{
		material.SetFloat("_ShouldPaint", 0f);
	}
}