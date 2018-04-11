// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Cactus"
{
	Properties
	{
		_AO_Cactus("AO_Cactus", 2D) = "white" {}
		_Emission("Emission", Range( 0 , 1)) = 0
		_Cactus_Change_D1("Cactus_Change_D 1", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Cactus_Change_D1;
		uniform float4 _Cactus_Change_D1_ST;
		uniform float _Emission;
		uniform sampler2D _AO_Cactus;
		uniform float4 _AO_Cactus_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Cactus_Change_D1 = i.uv_texcoord * _Cactus_Change_D1_ST.xy + _Cactus_Change_D1_ST.zw;
			o.Albedo = tex2D( _Cactus_Change_D1, uv_Cactus_Change_D1 ).rgb;
			float3 temp_cast_1 = (_Emission).xxx;
			o.Emission = temp_cast_1;
			float2 uv_AO_Cactus = i.uv_texcoord * _AO_Cactus_ST.xy + _AO_Cactus_ST.zw;
			o.Occlusion = tex2D( _AO_Cactus, uv_AO_Cactus ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;863.0368;301.7215;1;True;False
Node;AmplifyShaderEditor.SamplerNode;4;-424,-180;Float;True;Property;_Cactus_Change_D1;Cactus_Change_D 1;2;0;Create;True;0;3c623e7cab312734cb0d834c33540453;3c623e7cab312734cb0d834c33540453;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-413,393;Float;True;Property;_AO_Cactus;AO_Cactus;0;0;Create;True;0;f2d80f772352d0645b77e29d0a29c27b;f2d80f772352d0645b77e29d0a29c27b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-408.0368,96.2785;Float;False;Property;_Emission;Emission;1;0;Create;True;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Cactus;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;4;0
WireConnection;0;2;5;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=442A80412369CBDB81734A78161E6225B515867D