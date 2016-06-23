Shader "Unlit/EdgeFilter"
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
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "../Utils.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float2 _Resolution;
			
			fixed4 frag (v2f_img i) : SV_Target
			{
				// fixed4 col = tex2D(_MainTex, i.uv);

				fixed4 col = filter(_MainTex, i.uv, _Resolution);

				return col;
			}
			ENDCG
		}
	}
}
