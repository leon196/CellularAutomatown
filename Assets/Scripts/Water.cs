using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : CellularAutomaton
{
	public Color colorA;
	public Color colorB;

	void Update ()
	{
		if (materialCellularAutomaton != null)
		{
			materialCellularAutomaton.SetColor("_ColorA", colorA);
			materialCellularAutomaton.SetColor("_ColorB", colorB);
		}
	}
}