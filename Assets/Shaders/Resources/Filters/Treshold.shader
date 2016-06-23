Shader "Unlit/Treshold"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Range ("Range", Float) = 8
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Name "Treshold"
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "../Utils.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Range;
			float2 _Resolution;
			
			fixed4 frag (v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// fixed4 col = cheesyBlur (_MainTex, i.uv, _Resolution);

				col.rgb = float3(1,1,1) * floor(Luminance(col.rgb) * _Range) / _Range;

				// col.rgb
				// col.rgb -= float3(1,1,1) * fwidth( col.r * 4.0 );

				return col;
			}
			ENDCG
		}

		GrabPass 
		{
			Name "Treshold"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "../Utils.cginc"

			sampler2D _MainTex;
			sampler2D _CameraTexture;
			sampler2D _WaterTex;
			float4 _MainTex_ST;
			float2 _Resolution;

			sampler2D _GrabTexture;
			float4 _GrabTexture_ST;
			float4 _GrabTexture_TexelSize;

			struct v2f 
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				float4 uvgrab : TEXCOORD1;
			};

			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);   
				o.uv = TRANSFORM_TEX(v.texcoord, _GrabTexture);
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				o.uvgrab.xy = (float2(o.pos.x, o.pos.y * scale) + o.pos.w) * 0.5;
				o.uvgrab.zw = o.pos.zw;
				return o;
			}

			half4 filter2 (sampler2D bitmap, float4 uv, float4 dimension)
			{
			  half4 color = half4(0.0, 0.0, 0.0, 0.0);

			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-2, -2, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-2, -1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-2,  0, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-2,  1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-2,  2, 0, 0) / dimension));

			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-1, -2, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-1, -1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-1,  0, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-1,  1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4(-1,  2, 0, 0) / dimension));

			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 0, -2, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 0, -1, 0, 0) / dimension));
			  color += 24.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 0,  0, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 0,  1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 0,  2, 0, 0) / dimension));

			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 1, -2, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 1, -1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 1,  0, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 1,  1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 1,  2, 0, 0) / dimension));

			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 2, -2, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 2, -1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 2,  0, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 2,  1, 0, 0) / dimension));
			  color += -1.0 * tex2Dproj(bitmap, UNITY_PROJ_COORD(uv + float4( 2,  2, 0, 0) / dimension));

			  return color;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// fixed4 color = tex2D(_GrabTexture, i.uvgrab);
				// fixed4 color = fixed4(tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab)).rgb, 1);
				// color -= edge;

				fixed4 color = tex2D(_CameraTexture, i.uv);
				fixed4 edge = 1.0 - clamp(abs(filter2(_GrabTexture, i.uvgrab, float4(_Resolution * 2, 1, 1))), 0.0, 1.0);
				color.rgb *= edge.rgb;

				fixed4 water = tex2D(_WaterTex, i.uv);
				color.rgb = lerp(color.rgb, water, Luminance(water));

				return color;
			}
			ENDCG
		}
	}
}
