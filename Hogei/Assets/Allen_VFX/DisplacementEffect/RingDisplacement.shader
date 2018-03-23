Shader "Hidden/RingDisplacement"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DispTex ("Displace Texture", 2D) = "white" {}
		_DispStrength("Displacement Strength",Range(0,2)) = .1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _DispTex;
			fixed _DispStrength;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 disp = tex2D(_DispTex, i.uv);\
				i.uv.y -=(disp.r+.5)*_DispStrength*disp.b;
				i.uv.x -=(disp.g+.5)*_DispStrength*disp.b;

				fixed4 col = tex2D(_MainTex, i.uv);

				return col;
			}
			ENDCG
		}
	}
}
