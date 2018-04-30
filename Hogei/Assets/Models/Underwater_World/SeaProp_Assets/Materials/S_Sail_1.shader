// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Sail1"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Pirate_Sail_ambient_occlusion("Pirate_Sail_ambient_occlusion", 2D) = "white" {}
		_PirateSail_1_D("PirateSail_1_D", 2D) = "white" {}
		_PirateSail_1_Opacity2("PirateSail_1_Opacity2", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _PirateSail_1_D;
		uniform float4 _PirateSail_1_D_ST;
		uniform sampler2D _Pirate_Sail_ambient_occlusion;
		uniform float4 _Pirate_Sail_ambient_occlusion_ST;
		uniform sampler2D _PirateSail_1_Opacity2;
		uniform float4 _PirateSail_1_Opacity2_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_PirateSail_1_D = i.uv_texcoord * _PirateSail_1_D_ST.xy + _PirateSail_1_D_ST.zw;
			o.Albedo = tex2D( _PirateSail_1_D, uv_PirateSail_1_D ).rgb;
			float2 uv_Pirate_Sail_ambient_occlusion = i.uv_texcoord * _Pirate_Sail_ambient_occlusion_ST.xy + _Pirate_Sail_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _Pirate_Sail_ambient_occlusion, uv_Pirate_Sail_ambient_occlusion ).r;
			o.Alpha = 1;
			float2 uv_PirateSail_1_Opacity2 = i.uv_texcoord * _PirateSail_1_Opacity2_ST.xy + _PirateSail_1_Opacity2_ST.zw;
			clip( tex2D( _PirateSail_1_Opacity2, uv_PirateSail_1_Opacity2 ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;1217.909;400.9155;1.6;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-475,161;Float;True;Property;_Pirate_Sail_ambient_occlusion;Pirate_Sail_ambient_occlusion;1;0;Create;True;0;e88453fd522c30e4f944cdc2256923be;e88453fd522c30e4f944cdc2256923be;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-446,394;Float;True;Property;_PirateSail_1_Opacity;PirateSail_1_Opacity;3;0;Create;True;0;4b71cf1467f051447a9b802797d17e49;4b71cf1467f051447a9b802797d17e49;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-391,500;Float;True;Property;_PirateSail_1_Opacity2;PirateSail_1_Opacity2;4;0;Create;True;0;bd03686cae3d7d542b3c5ad548d3d794;bd03686cae3d7d542b3c5ad548d3d794;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-441.6,-65.20004;Float;True;Property;_PirateSail_1_D;PirateSail_1_D;2;0;Create;True;0;8badc15f0134d8d479e5be09f361e221;8badc15f0134d8d479e5be09f361e221;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Sail1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;2;0
WireConnection;0;5;1;0
WireConnection;0;10;4;0
ASEEND*/
//CHKSM=2F5FF6C7D49256CE8FEB0C98E3912A8FC9B16BF1