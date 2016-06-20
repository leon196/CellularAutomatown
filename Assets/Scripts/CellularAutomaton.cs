using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellularAutomaton : MonoBehaviour
{
	// shader used to apply cellular automaton
	public Shader shaderCellularAutomaton;

	// material used to store cellular automaton
	[HideInInspector] public Material materialCellularAutomaton;

	// material to show texture
	public Material materialOutput;

	// grid size
	[Range(32, 2048)] public int width = 128;
	[Range(32, 2048)] public int height = 128;

	// refresh rate
	[Range(0.0f, 1.0f)] public float delay = 0.1f;
	private float last = 0f;

	// internal render process
	private Texture2D input;
	private RenderTexture output;
	private FrameBuffer frameBuffer;

	void Start ()
	{
		Camera.onPreRender += onPreRender;
		
		input = new Texture2D(width, height);
		
		Color[] colorArray = new Color[width * height];
		for (int i = 0; i < colorArray.Length; ++i) 
		{
			colorArray[i] = Random.Range(0f, 1f) > 0.5f ? Color.white : Color.black;
		}
		
		input.SetPixels(colorArray);
		input.Apply();

		if (materialOutput)
		{
			materialOutput.mainTexture = input;
		}

		frameBuffer = new FrameBuffer();
		frameBuffer.Create(width, height);
		output = frameBuffer.Get();
		frameBuffer.Swap();
		Graphics.Blit(input, frameBuffer.Get());
	}

	public void onPreRender (Camera camera)
	{
		if (materialCellularAutomaton == null)
		{
			materialCellularAutomaton = new Material(shaderCellularAutomaton);
		}
		else
		{
			materialCellularAutomaton.SetVector("_Resolution", new Vector2(width, height));
		}

		if (last + delay <= Time.time)
		{
			last = Time.time;

			Graphics.Blit(frameBuffer.Get(), output, materialCellularAutomaton);

			output = frameBuffer.Get();
			frameBuffer.Swap();

			if (materialOutput)
			{
				materialOutput.mainTexture = output;
			}
		}
	}
}
