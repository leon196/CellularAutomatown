using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellularAutomaton : MonoBehaviour
{
	// shader used to apply cellular automaton
	public Shader shaderCellularAutomaton;

	// material used to store cellular automaton
	[HideInInspector] public Material materialCellularAutomaton;

	// refresh rate
	[Range(1, 60)] public int frameRate = 60;
	private float delay = 0f;
	private float last = 0f;

	public bool startWithNoise = false;

	// internal render process
	private Texture2D input;
	private FrameBuffer frameBuffer;
	private Material materialOutput;
	[HideInInspector] public RenderTexture result;
	[HideInInspector] public RenderTexture output;

	Blur blur;

	void Start ()
	{
		Camera.onPreRender += onPreRender;
		
		input = new Texture2D(Engine.width, Engine.height);
		
		Color[] colorArray = new Color[Engine.width * Engine.height];
		if (startWithNoise)
		{
			for (int i = 0; i < colorArray.Length; ++i) {
				colorArray[i] = Random.Range(0f, 1f) > 0.5f ? Color.white : Color.black;
			}
		}
		else
		{
			for (int i = 0; i < colorArray.Length; ++i) {
				colorArray[i] = Color.black;
			}
		}
		
		input.SetPixels(colorArray);
		input.Apply();

		materialOutput = GetComponent<Renderer>().sharedMaterial;

		blur = GameObject.FindObjectOfType<Blur>();

		if (materialOutput)
		{
			materialOutput.mainTexture = input;
		}

		frameBuffer = new FrameBuffer();
		frameBuffer.Create(Engine.width, Engine.height);
		Graphics.Blit(input, frameBuffer.Get());
		frameBuffer.Swap();
		output = frameBuffer.Get();

		result = new RenderTexture(Engine.width, Engine.height, 24, RenderTextureFormat.ARGB32);
	}

	public void onPreRender (Camera camera)
	{
		Shader.SetGlobalVector("_Resolution", new Vector2(Engine.width, Engine.height));

		if (materialCellularAutomaton == null)
		{
			materialCellularAutomaton = new Material(shaderCellularAutomaton);
		}

		// if (last + delay <= Time.time)
		// {
		// 	last = Time.time;
		// 	delay = 1f / (float)frameRate;

			Graphics.Blit(frameBuffer.Get(), output, materialCellularAutomaton);

			output = frameBuffer.Get();
			frameBuffer.Swap();

			blur.Blit(output, result);

			if (materialOutput)
			{
				materialOutput.mainTexture = result;
			}
		// }
	}

	public void Print (Texture texture)
	{
		Graphics.Blit(texture, frameBuffer.Get());
		frameBuffer.Swap();
		Graphics.Blit(texture, frameBuffer.Get());
	}
}
