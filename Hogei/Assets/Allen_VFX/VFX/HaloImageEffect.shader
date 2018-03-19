Shader "Hidden/HaloImageEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Halo ("Halo Texture", 2D) = "black" {}
		_Distortion ("Distortion", Range(0,.1))= .01

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
			sampler2D _Halo; 
			float _Distortion;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 halo = tex2D(_Halo, i.uv);
				i.uv += halo*_Distortion;
				fixed4 col = tex2D(_MainTex, i.uv);

				// just invert the colors
				//col = 1 - col;
				return col;
			}
			ENDCG
		}
	}
}
