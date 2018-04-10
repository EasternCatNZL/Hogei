// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Crate1"
{
	Properties
	{
		_Metal_Intensity("Metal_Intensity", Range( 0 , 1)) = 0
		_Float0("Float 0", Range( 0 , 1)) = 0
		_M_Crate_D_Bright("M_Crate_D_Bright", 2D) = "white" {}
		_AO_Crate("AO_Crate", 2D) = "white" {}
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

		uniform sampler2D _M_Crate_D_Bright;
		uniform float4 _M_Crate_D_Bright_ST;
		uniform float _Float0;
		uniform float _Metal_Intensity;
		uniform sampler2D _AO_Crate;
		uniform float4 _AO_Crate_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_M_Crate_D_Bright = i.uv_texcoord * _M_Crate_D_Bright_ST.xy + _M_Crate_D_Bright_ST.zw;
			o.Albedo = tex2D( _M_Crate_D_Bright, uv_M_Crate_D_Bright ).rgb;
			o.Metallic = _Float0;
			o.Smoothness = _Metal_Intensity;
			float2 uv_AO_Crate = i.uv_texcoord * _AO_Crate_ST.xy + _AO_Crate_ST.zw;
			o.Occlusion = tex2D( _AO_Crate, uv_AO_Crate ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;1032.368;353.3479;1.3;True;False
Node;AmplifyShaderEditor.RangedFloatNode;5;-404.5,131.2;Float;False;Property;_Float0;Float 0;1;0;Create;True;0;0;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-385.0002,227.7001;Float;False;Property;_Metal_Intensity;Metal_Intensity;0;0;Create;True;0;0;0.7634133;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;8;-526.6675,-50.44794;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;7754ee730255c474cbcc7d9e477f8674;7754ee730255c474cbcc7d9e477f8674;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-540.9674,-254.548;Float;True;Property;_M_Crate_D_Bright;M_Crate_D_Bright;2;0;Create;True;0;c647de4f392a51346b3c59d6923b5117;c647de4f392a51346b3c59d6923b5117;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-473.3676,381.1519;Float;True;Property;_AO_Crate;AO_Crate;3;0;Create;True;0;fb16d61e830118e47a6d250683fe9812;fb16d61e830118e47a6d250683fe9812;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Crate1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;6;0
WireConnection;0;3;5;0
WireConnection;0;4;3;0
WireConnection;0;5;9;0
ASEEND*/
//CHKSM=4EF545E2CB10E159526BEB1342A613FBA528F3B6