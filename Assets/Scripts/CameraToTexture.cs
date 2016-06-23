using UnityEngine;
using System.Collections;

public class CameraToTexture : MonoBehaviour
{
	public string textureName = "_CameraTexture";
	public Material material;
	RenderTexture buffer;
	
	void Start ()
	{
		buffer = new RenderTexture(Engine.width, Engine.height, 24, RenderTextureFormat.ARGB32);
		buffer.antiAliasing = 2;
		buffer.Create();
		GetComponent<Camera>().targetTexture = buffer;
		Shader.SetGlobalTexture(textureName, buffer);
		if (material != null) {
			material.mainTexture = buffer;
		}
	}

	void OnEnable ()
	{
		Shader.SetGlobalTexture(textureName, buffer);
	}
}