Shader "Hidden/Water"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Cull Off ZWrite Off ZTest Always
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Utils.cginc"
			
			sampler2D _MainTex;
			sampler2D _Background;
			sampler2D _GroundTex;
			float2 _Resolution;
			float _ShouldPaint;
			float _PaintSize;
			float2 _PaintPosition;
			float4 _PaintColor;
			float4 _WaterColor;

			fixed4 frag (v2f_img i) : SV_Target
			{
				// float intensity = lightIntensityAround(_GroundTex, i.uv, _Resolution);
				// fixed4 col = lerp(tex2D(_MainTex, i.uv), _WaterColor, clamp(intensity * 0.1, 0.0, 1.0));

				float2 offset = lightDirectionUnit(_GroundTex, i.uv, _Resolution) / _Resolution;
				fixed4 col = tex2D(_MainTex, i.uv + offset * 0.1);

				// float howManyAround = HowManyAround(_MainTex, i.uv, _Resolution);
				// if (howManyAround < 1) 
				// {
				// 	col = lerp(col, _ColorA, 0.1);
				// }
				// else if (howManyAround < 3) 
				// {
				// 	col = lerp(col, _ColorB, 0.1);
				// }

				if (_ShouldPaint) 
				{
					float paint = step(distance(i.uv, _PaintPosition), _PaintSize / _Resolution);
					col.rgb = lerp(col.rgb, _PaintColor, paint);
				}

				col.rgb *= 0.99;

				col.rgb = lerp(_WaterColor, col.rgb, step(0.35, Luminance(tex2D(_GroundTex, i.uv))));

				col.a = smoothstep(0.0, 0.5, Luminance(col.rgb));

				return col;
			}
			ENDCG
		}
	}
}
