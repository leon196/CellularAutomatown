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
			float4 _GreenColor;
			sampler2D _CameraTexture;

			fixed4 frag (v2f_img i) : SV_Target
			{
				// float intensity = lightIntensityAround(_GroundTex, i.uv, _Resolution);
				// fixed4 col = lerp(tex2D(_MainTex, i.uv), _WaterColor, clamp(intensity * 0.1, 0.0, 1.0));

				float2 unit = 1.0 / _Resolution;

				float2 offset = lightDirectionUnit(_GroundTex, i.uv, _Resolution);
				// fixed4 col = filter(_MainTex, i.uv, _Resolution);
				fixed4 col = tex2D(_MainTex, i.uv);
				// fixed4 col = tex2D(_MainTex, i.uv + normalize(offset) * unit);

				// float howManyAround = HowManyAround(_MainTex, i.uv, _Resolution);
				// if (howManyAround < 1) 
				// {
				// 	col = lerp(col, _ColorA, 0.1);
				// }
				// else if (howManyAround < 3) 
				// {
				// 	col = lerp(col, _ColorB, 0.1);
				// }

				// if (_ShouldPaint) 
				// {
				// 	float paint = step(distance(i.uv, _PaintPosition), _PaintSize / _Resolution);
				// 	col.rgb = lerp(col.rgb, _PaintColor, paint);
				// }

				// col.rgb = lerp(col.rgb, col.rgb * 0.99, 0.1);

				// col.rgb = lerp(col.rgb * 0.9, _GreenColor, 0.1);

				col *= 0.99;
				col.rg += lerp(float2(0.1,-0.01), float2(-0.001, 0.01), tex2D(_CameraTexture, i.uv).r);
				float os = sin(_Time.y * 0.2) * 0.5 + 0.5;
				col.rgb = lerp(col.rgb, _WaterColor, smoothstep(0.4, 1.1, Luminance(tex2D(_GroundTex, i.uv))));
				// col.b *= 0.9;
				// col.rgb = lerp(col.rgb * 0.99, _WaterColor, step(0.5, Luminance(tex2D(_GroundTex, i.uv))));

				// col.rgb *= 0.99;
				// col.a = smoothstep(0.0, 0.5, Luminance(col.rgb));

				return col;
			}
			ENDCG
		}
	}
}
