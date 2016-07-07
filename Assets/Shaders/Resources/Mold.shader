Shader "Hidden/Mold"
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
			
			sampler2D _MainTex;
			sampler2D _Background;
			float2 _Resolution;
			float _ShouldPaint;
			float _PaintSize;
			float2 _PaintPosition;
			float4 _PaintColor;

			float HowManyAround (sampler2D bitmap, float2 p, float2 resolution)
			{
				int count = 0;
				count += step(0.2, Luminance(tex2D(bitmap, p + float2(-1.0, -1.0) / resolution)));
				count += step(0.2, Luminance(tex2D(bitmap, p + float2( 0.0, -1.0) / resolution)));
				count += step(0.2, Luminance(tex2D(bitmap, p + float2( 1.0, -1.0) / resolution)));
				count += step(0.2, Luminance(tex2D(bitmap, p + float2(-1.0,  0.0) / resolution)));
				count += step(0.2, Luminance(tex2D(bitmap, p + float2( 1.0,  0.0) / resolution)));
				count += step(0.2, Luminance(tex2D(bitmap, p + float2(-1.0,  1.0) / resolution)));
				count += step(0.2, Luminance(tex2D(bitmap, p + float2( 0.0,  1.0) / resolution)));
				count += step(0.2, Luminance(tex2D(bitmap, p + float2( 1.0,  1.0) / resolution)));
				return count;
			}

			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float howManyAround = HowManyAround(_MainTex, i.uv, _Resolution);
				float isAlive = step(0.5, col.r + col.g + col.b);
				if (isAlive) 
				{
					if (howManyAround < 3) 
					{
						// col.rgb = float3(0,0,0);
						col.g = clamp(col.g - 0.05, 0.0, 1.0);
					}
					else
					{
						col.g = clamp(col.g + 0.05, 0.0, 1.0);
					}
				} 
				else 
				{
					if (howManyAround > 3) 
					{
						col.g = clamp(col.g + 0.05, 0.0, 1.0);
					}
					else
					{
						col.g = clamp(col.g - 0.05, 0.0, 1.0);
					}
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
