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
			#pragma geometry geom
			#pragma fragment frag
			#pragma target 3.0
			
			#include "UnityCG.cginc"
			#include "Utils.cginc"
			
			struct GS_INPUT
			{
				float4 vertex		: POSITION;
				float3 normal	: NORMAL;
				float2 uv	: TEXCOORD0;
				float4 screenUV : TEXCOORD1;
			};

			struct FS_INPUT
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			sampler2D _HeightTex;
			float4 _MainTex_ST;
			
			GS_INPUT vert (appdata_full v)
			{
				GS_INPUT o = (GS_INPUT)0;
				v.vertex.z += tex2Dlod(_HeightTex, v.texcoord).r * 8;
				o.vertex = v.vertex;//mul(UNITY_MATRIX_MVP, mul(_World2Object, vertex));
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.normal = v.normal;
				return o;
			}

			[maxvertexcount(3)]
			void geom(triangle GS_INPUT tri[3], inout TriangleStream<FS_INPUT> triStream)
			{
				float4x4 vp = mul(UNITY_MATRIX_MVP, _World2Object);

				float3 a = tri[0].vertex;
				float3 b = tri[1].vertex;
				float3 c = tri[2].vertex;

				float3 normal = normalize(cross(b - a, b - c));

				FS_INPUT pIn = (FS_INPUT)0;
				pIn.vertex = mul(vp, mul(_Object2World, float4(a, 1.0)));
				pIn.uv = tri[0].uv;
				pIn.normal = normal;//tri[0].normal;
				triStream.Append(pIn);

				pIn.vertex =  mul(vp, mul(_Object2World, float4(b, 1.0)));
				pIn.uv = tri[1].uv;
				pIn.normal = normal;//tri[1].normal;
				triStream.Append(pIn);

				pIn.vertex =  mul(vp, mul(_Object2World, float4(c, 1.0)));
				pIn.uv = tri[2].uv;
				pIn.normal = normal;//tri[2].normal;
				triStream.Append(pIn);
			}
			
			fixed4 frag (FS_INPUT i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// float h = tex2D(_HeightTex, i.uv).r;
				// col.rgb = lerp(col.rgb, float3(0,0,1), 1.0 - smoothstep(0.0, 0.5, h));
				// col.rgb = normalize(i.normal) * 0.5 + 0.5;
				float d = dot(normalize(rotateZ(float3(0,1,0), _Time.y * 0.1)), normalize(i.normal)) * 0.5 + 0.6;
				col.rgb = float3(1,1,1) * (sqrt(d));
				return col;
			}
			ENDCG
		}
	}
}