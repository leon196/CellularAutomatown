using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Terrain : CellularAutomaton
{
	public Color colorA;
	public Color colorB;

	public Water water;

	void Update ()
	{
		if (water != null && materialCellularAutomaton != null)
		{
			materialCellularAutomaton.SetColor("_ColorA", colorA);
			materialCellularAutomaton.SetColor("_ColorB", colorB);
			materialCellularAutomaton.SetTexture("_WaterTex", water.output);
		}
		else
		{
			water = GameObject.FindObjectOfType<Water>();
		}
	}
}