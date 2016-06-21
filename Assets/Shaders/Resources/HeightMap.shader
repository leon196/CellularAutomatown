Shader "Unlit/HeightMap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _HeightTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				float4 vertex = mul(_Object2World, v.vertex);
				vertex.y += tex2Dlod(_HeightTex, float4(v.uv, 0, 0)).r * 8;
				o.vertex = mul(UNITY_MATRIX_MVP, mul(_World2Object, vertex));
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				float h = tex2D(_HeightTex, i.uv).r;
				col.rgb = lerp(col.rgb, float3(0,0,1), 1.0 - smoothstep(0.0, 0.5, h));
				return col;
			}
			ENDCG
		}
	}
}
