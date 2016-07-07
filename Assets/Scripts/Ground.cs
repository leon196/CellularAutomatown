using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : CellularAutomaton
{
	public Color colorA;
	public Color colorB;

	public Water water;

	public Texture2D heightTexture;

	public Material materialHeightMap;
	public Material materialAlpha;

	void Update ()
	{
		if (water != null && materialCellularAutomaton != null)
		{
			materialCellularAutomaton.SetColor("_ColorA", colorA);
			materialCellularAutomaton.SetColor("_ColorB", colorB);
			materialCellularAutomaton.SetTexture("_WaterTex", water.output);
			materialCellularAutomaton.SetTexture("_HeightTex", heightTexture);
			
			Shader.SetGlobalTexture("_WaterTex", water.output);

			if (materialAlpha)
			{
				materialAlpha.mainTexture = water.output;
			}

			if (materialHeightMap)
			{
				materialHeightMap.mainTexture = result;
				materialHeightMap.SetTexture("_HeightTex", result);
			}
		}
		else
		{
			water = GameObject.FindObjectOfType<Water>();
			Print(heightTexture);
		}
	}
}