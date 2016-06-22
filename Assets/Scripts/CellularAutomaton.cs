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
	[Range(1, 60)] public int frameRate = 60;
	private float delay = 0f;
	private float last = 0f;

	[Range(0f, 1f)] public float lifeTreshold = 0.5f;
	[Range(0f, 0.1f)] public float acceleration = 0.1f;

	public bool startWithNoise = false;

	// internal render process
	private Texture2D input;
	private RenderTexture output;
	private FrameBuffer frameBuffer;

	void Start ()
	{
		Camera.onPreRender += onPreRender;
		
		input = new Texture2D(width, height);
		
		Color[] colorArray = new Color[width * height];
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

		if (materialOutput)
		{
			materialOutput.mainTexture = input;
		}

		frameBuffer = new FrameBuffer();
		frameBuffer.Create(width, height);
		Graphics.Blit(input, frameBuffer.Get());
		frameBuffer.Swap();
		output = frameBuffer.Get();
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
			materialCellularAutomaton.SetFloat("_LifeTreshold", lifeTreshold);
			materialCellularAutomaton.SetFloat("_Acceleration", acceleration);
		}

		if (last + delay <= Time.time)
		{
			last = Time.time;
			delay = 1f / (float)frameRate;

			Graphics.Blit(frameBuffer.Get(), output, materialCellularAutomaton);

			output = frameBuffer.Get();
			frameBuffer.Swap();

			if (materialOutput)
			{
				materialOutput.mainTexture = output;
			}
		}
	}

	public void Print (Texture texture)
	{
		Graphics.Blit(texture, frameBuffer.Get());
		frameBuffer.Swap();
		Graphics.Blit(texture, frameBuffer.Get());
	}
}
