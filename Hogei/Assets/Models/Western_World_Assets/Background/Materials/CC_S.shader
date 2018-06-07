// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/ChickenC"
{
	Properties
	{
		_Chicken_Coop_ambient_occlusion("Chicken_Coop_ambient_occlusion", 2D) = "white" {}
		_ChickenCoop_D2("ChickenCoop_D 2", 2D) = "white" {}
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

		uniform sampler2D _ChickenCoop_D2;
		uniform float4 _ChickenCoop_D2_ST;
		uniform sampler2D _Chicken_Coop_ambient_occlusion;
		uniform float4 _Chicken_Coop_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_ChickenCoop_D2 = i.uv_texcoord * _ChickenCoop_D2_ST.xy + _ChickenCoop_D2_ST.zw;
			o.Albedo = tex2D( _ChickenCoop_D2, uv_ChickenCoop_D2 ).rgb;
			float2 uv_Chicken_Coop_ambient_occlusion = i.uv_texcoord * _Chicken_Coop_ambient_occlusion_ST.xy + _Chicken_Coop_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _Chicken_Coop_ambient_occlusion, uv_Chicken_Coop_ambient_occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;876.3774;279.0857;1;True;False
Node;AmplifyShaderEditor.SamplerNode;5;-537.3774,155.9143;Float;True;Property;_CC_D_1;CC_D_1;1;0;Create;True;0;eb44c70fab5533e4db389c1030759da9;eb44c70fab5533e4db389c1030759da9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-463.3774,-51.08569;Float;True;Property;_ChickenCoop_D2;ChickenCoop_D 2;3;0;Create;True;0;f076aacd311ed6b43901560aed2c5147;f076aacd311ed6b43901560aed2c5147;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-380,332;Float;True;Property;_Chicken_Coop_ambient_occlusion;Chicken_Coop_ambient_occlusion;0;0;Create;True;0;dbe98fe3a558d7b4bab5e23ef2c2db2f;dbe98fe3a558d7b4bab5e23ef2c2db2f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-239.3774,-155.0857;Float;True;Property;_ChickenCoop_D1;ChickenCoop_D 1;2;0;Create;True;0;20b61afd7a0f2e348bdb742b5e992371;20b61afd7a0f2e348bdb742b5e992371;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/ChickenC;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;8;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=3DB6DAB9ED40A784022CFE15D5E1F0AF1E18FBFF