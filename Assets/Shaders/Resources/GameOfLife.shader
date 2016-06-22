Shader "Hidden/GameOfLife"
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
			float _LifeTreshold;
			float _Acceleration;
			float2 _PaintPosition;
			float4 _PaintColor;

			float HowManyAround (sampler2D bitmap, float2 p, float2 resolution)
			{
				int count = 0;
				count += step(_LifeTreshold, tex2D(bitmap, p + float2(-1.0, -1.0) / resolution).r);
				count += step(_LifeTreshold, tex2D(bitmap, p + float2( 0.0, -1.0) / resolution).r);
				count += step(_LifeTreshold, tex2D(bitmap, p + float2( 1.0, -1.0) / resolution).r);
				count += step(_LifeTreshold, tex2D(bitmap, p + float2(-1.0,  0.0) / resolution).r);
				count += step(_LifeTreshold, tex2D(bitmap, p + float2( 1.0,  0.0) / resolution).r);
				count += step(_LifeTreshold, tex2D(bitmap, p + float2(-1.0,  1.0) / resolution).r);
				count += step(_LifeTreshold, tex2D(bitmap, p + float2( 0.0,  1.0) / resolution).r);
				count += step(_LifeTreshold, tex2D(bitmap, p + float2( 1.0,  1.0) / resolution).r);
				return count;
			}

			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float howManyAround = HowManyAround(_MainTex, i.uv, _Resolution);
				float isAlive = step(0.5, col.r);
				if (isAlive) 
				{
					if (howManyAround < 2 || howManyAround > 3) 
					{
						// col.rgb = float3(0,0,0);
						col.rgb = clamp(col.rgb - _Acceleration, 0.0, 1.0);
					}
					else
					{
						col.rgb = clamp(col.rgb + _Acceleration, 0.0, 1.0);
						// col.rgb *= 1.5;
					}
				} 
				else 
				{
					if (howManyAround == 3) 
					{
						// col.rgb = float3(1,1,1);//tex2D(_Background, i.uv);
						// col.rgb *= 1.5;
						col.rgb = clamp(col.rgb + _Acceleration, 0.0, 1.0);
					}
					else
					{
						col.rgb = clamp(col.rgb - _Acceleration, 0.0, 1.0);
					}
				}

				if (_ShouldPaint) 
				{
					float paint = step(distance(i.uv, _PaintPosition), _PaintSize / _Resolution);
					col.rgb = lerp(col.rgb, _PaintColor, paint);
					// float3 paintColor = tex2D(_Background, i.uv).rgb;
					// paintColor.rgb = lerp(paintColor.rgb, float3(1,0,0), step(0.15, floor(paintColor.g * 4) / 4));
					// col.rgb = lerp(col.rgb, paintColor, paint);
				}

				return col;
			}
			ENDCG
		}
	}
}
