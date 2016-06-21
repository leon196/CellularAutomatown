Shader "Hidden/Terrain"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Utils.cginc"
			
			sampler2D _MainTex;
			sampler2D _WaterTex;
			float2 _Resolution;
			float _ShouldPaint;
			float _PaintSize;
			float2 _PaintPosition;
			float4 _PaintColor;
			float4 _ColorA;
			float4 _ColorB;

			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float howManyAround = HowManyAround(_WaterTex, i.uv, _Resolution);
				if (howManyAround < 1) 
				{
					col = lerp(col, _ColorA, 0.1);
				}
				else
				{
					col = lerp(col, _ColorB, 0.1);
				}

				if (_ShouldPaint) 
				{
					float paint = step(distance(i.uv, _PaintPosition), _PaintSize / _Resolution);
					col.rgb = lerp(col.rgb, _PaintColor, paint);
				}

				return col;
			}
			ENDCG
		}
	}
}
