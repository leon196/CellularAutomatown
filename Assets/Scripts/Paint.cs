using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paint : MonoBehaviour
{
	[Range (1, 10)] public int size = 1;
	// public Texture background;
	Color color = Color.white;

	Ray ray;
	RaycastHit hitInfo;
	Material material;
	Texture2D texture2d;

	CellularAutomaton cellularAutomaton;

	void Start ()
	{
		cellularAutomaton = GetComponent<CellularAutomaton>();
		texture2d = new Texture2D(Engine.width, Engine.height);
	}

	void Update ()
	{
		if (material)
		{
			UpdatePaint(hitInfo.textureCoord);

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hitInfo, 100)) 
			{
				if (hitInfo.transform.gameObject == this.gameObject)
				{
					if (Input.GetMouseButtonDown(0))
					{
						RenderTexture.active = cellularAutomaton.output;
						texture2d.ReadPixels(new Rect(0, 0, Engine.width, Engine.height), 0, 0);
						texture2d.Apply();
						color = texture2d.GetPixel((int)(hitInfo.textureCoord.x * Engine.width), (int)(hitInfo.textureCoord.y * Engine.height));
					}

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