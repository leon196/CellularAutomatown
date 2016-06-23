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
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "Utils.cginc"
			
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _HeightTex;
			sampler2D _WaterTex;
			float2 _Resolution;
			float _ShouldPaint;
			float _PaintSize;
			float2 _PaintPosition;
			float4 _PaintColor;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 screenUV : TEXCOORD1;
			};

			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				o.screenUV = ComputeScreenPos(o.pos);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float unit = 1.0 / _Resolution;

				float n = noiseIQ(float3(i.uv * 8.0 + _Time.y, _Time.y * 0.2));
				// fixed4 col = fixed4(1,1,1,1);
				// col.rgb *= n;
				// float angle = Luminance(tex2D(_MainTex, i.uv)) * PI * 2.0;
				float angle = 0;
				float2 offset = float2(0,0);

				angle = fmod(_Time.y * 0.1, PI * 2);
				// offset += float2(cos(angle), sin(angle)) * unit;

				angle = n * PI * 2.0;
				// offset += float2(cos(angle), sin(angle)) * unit;

				// offset -= lightDirectionUnit(_MainTex, i.uv, _Resolution) * unit * 0.25;

				angle = rand(i.screenUV + _Time.y) * PI * 2;
				// float osc = sin(_Time.y) * 0.5 + 0.5;
				offset += float2(cos(angle), sin(angle)) * unit;
				
				// fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_HeightTex, i.uv - offset), 0.5);
				fixed4 col = tex2D(_MainTex, fmod(abs(i.uv - offset + 1.0), 1.0));
				col = lerp(col, tex2D(_MainTex, i.uv), 0.8);

				// fixed4 col = tex2D(_MainTex, i.uv);

				// col.rgb = floor(col.rgb * 8.0) / 8.0;
				// col.rgb += (n) * 0.01;


				// col.rgb = lerp(_ColorA, _ColorB, Luminance(col.rgb));

				// col.rgb -= float3(1,1,1) * fwidth( col.r * 64.0 );

				if (_ShouldPaint) 
				{
					float dist = distance(i.uv, _PaintPosition);
					// float d = 1.0 - clamp(dist / _PaintSize, 0.0, 1.0);
					float paint = 1.0 - smoothstep(0.0, _PaintSize / _Resolution, dist);// * d;
					col.rgb = lerp(col.rgb, _PaintColor, paint);// * rand(i.uv + _Time.y));
				}

				// col.rgb = lerp(float3(1,1,1), col.rgb, step(unit, distance(i.uv, float2(0.5, 0.5))));

				return col;
			}
			ENDCG
		}
	}
}
