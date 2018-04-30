// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Sail2"
{
	Properties
	{
		_Pirate_Sail_ambient_occlusion("Pirate_Sail_ambient_occlusion", 2D) = "white" {}
		_PirateSail_2_D("PirateSail_2_D", 2D) = "white" {}
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

		uniform sampler2D _PirateSail_2_D;
		uniform float4 _PirateSail_2_D_ST;
		uniform sampler2D _Pirate_Sail_ambient_occlusion;
		uniform float4 _Pirate_Sail_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_PirateSail_2_D = i.uv_texcoord * _PirateSail_2_D_ST.xy + _PirateSail_2_D_ST.zw;
			o.Albedo = tex2D( _PirateSail_2_D, uv_PirateSail_2_D ).rgb;
			float2 uv_Pirate_Sail_ambient_occlusion = i.uv_texcoord * _Pirate_Sail_ambient_occlusion_ST.xy + _Pirate_Sail_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _Pirate_Sail_ambient_occlusion, uv_Pirate_Sail_ambient_occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;633;479;1;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-336,-78;Float;True;Property;_PirateSail_2_D;PirateSail_2_D;1;0;Create;True;0;cdb16f8906f0f7a498e4724122980671;cdb16f8906f0f7a498e4724122980671;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-369,169;Float;True;Property;_Pirate_Sail_ambient_occlusion;Pirate_Sail_ambient_occlusion;0;0;Create;True;0;e88453fd522c30e4f944cdc2256923be;e88453fd522c30e4f944cdc2256923be;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Sail2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;2;0
WireConnection;0;5;1;0
ASEEND*/
//CHKSM=2A06049A5503B61FCBB32D446503AD3B08EA5268