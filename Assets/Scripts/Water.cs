using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : CellularAutomaton
{
	public Color color;
	public Ground ground;

	private Texture2D texture2D;

	void Update ()
	{
		// if (texture2D == null)
		// {
			// texture2D = new Texture2D(Engine.width, Engine.height);
			// Color[] colorArray = new Color[texture2D.width * texture2D.height];
			// for (int i = 0; i < colorArray.Length; ++i)
			// {
			// 	colorArray[i] = color;
			// }
			// texture2D.SetPixels(colorArray);
			// texture2D.Apply();
			// Print(texture2D);
		// } 

		if (materialCellularAutomaton != null)
		{
			materialCellularAutomaton.SetTexture("_GroundTex", ground.output);
			materialCellularAutomaton.SetColor("_WaterColor", color);
		}
	}
}