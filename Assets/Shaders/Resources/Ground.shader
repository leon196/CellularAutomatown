Shader "Hidden/Ground"
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
			sampler2D _HeightTex;
			sampler2D _WaterTex;
			float2 _Resolution;
			float _ShouldPaint;
			float _PaintSize;
			float2 _PaintPosition;
			float4 _PaintColor;

			fixed4 frag (v2f_img i) : SV_Target
			{
				float unit = 1.0 / _Resolution;

				float n = noiseIQ(float3(i.uv * 8.0, _Time.y * 0.2));
				// fixed4 col = fixed4(1,1,1,1);
				// col.rgb *= n;
				float angle = n * PI * 2.0;
				// float angle = Luminance(tex2D(_MainTex, i.uv)) * PI * 2.0;
				float2 offset = float2(cos(angle), sin(angle)) * unit * 0.1;

				offset += lightDirectionUnit(_MainTex, i.uv, _Resolution) * unit * 0.02;
				
				// fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_HeightTex, i.uv - offset), 0.5);
				fixed4 col = tex2D(_MainTex, i.uv - offset);

				// fixed4 col = tex2D(_MainTex, i.uv);

				// col.rgb = floor(col.rgb * 8.0) / 8.0;
				// col.rgb += (n) * 0.01;


				// col.rgb = lerp(_ColorA, _ColorB, Luminance(col.rgb));

				// col.rgb -= float3(1,1,1) * fwidth( col.r * 64.0 );

				if (_ShouldPaint) 
				{
					float dist = distance(i.uv, _PaintPosition);
					float d = 1.0 - clamp(dist * 20.0, 0.0, 1.0);
					float paint = step(dist, _PaintSize / _Resolution) * d;
					col.rgb = lerp(col.rgb, _PaintColor, paint);
				}

				// col.rgb = lerp(float3(1,1,1), col.rgb, step(unit, distance(i.uv, float2(0.5, 0.5))));

				return col;
			}
			ENDCG
		}
	}
}
