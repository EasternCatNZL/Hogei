// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/ChickenH"
{
	Properties
	{
		_Chicken_House_ambient_occlusion("Chicken_House_ambient_occlusion", 2D) = "white" {}
		_CH_D("CH_D", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _CH_D;
		uniform float4 _CH_D_ST;
		uniform sampler2D _Chicken_House_ambient_occlusion;
		uniform float4 _Chicken_House_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_CH_D = i.uv_texcoord * _CH_D_ST.xy + _CH_D_ST.zw;
			o.Albedo = tex2D( _CH_D, uv_CH_D ).rgb;
			float2 uv_Chicken_House_ambient_occlusion = i.uv_texcoord * _Chicken_House_ambient_occlusion_ST.xy + _Chicken_House_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _Chicken_House_ambient_occlusion, uv_Chicken_House_ambient_occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;693;378;1;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-372,-63;Float;True;Property;_CH_D;CH_D;1;0;Create;True;0;ce87cf43cd8a40448b0d6c35b907a292;ce87cf43cd8a40448b0d6c35b907a292;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-436,262;Float;True;Property;_Chicken_House_ambient_occlusion;Chicken_House_ambient_occlusion;0;0;Create;True;0;fcf32f8466a03224eaa803843267a9fc;fcf32f8466a03224eaa803843267a9fc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/ChickenH;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;2;0
WireConnection;0;5;1;0
ASEEND*/
//CHKSM=C08C5DE600D8A75351394CFD58BF6B3F67CFCB67