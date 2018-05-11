// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/ChickenLeg"
{
	Properties
	{
		_M_Chicken_Leg_ambient_occlusion("M_Chicken_Leg_ambient_occlusion", 2D) = "white" {}
		_Chicken_Leg_Diffuse_4_origianl("Chicken_Leg_Diffuse_4_origianl", 2D) = "white" {}
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

		uniform sampler2D _Chicken_Leg_Diffuse_4_origianl;
		uniform float4 _Chicken_Leg_Diffuse_4_origianl_ST;
		uniform sampler2D _M_Chicken_Leg_ambient_occlusion;
		uniform float4 _M_Chicken_Leg_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Chicken_Leg_Diffuse_4_origianl = i.uv_texcoord * _Chicken_Leg_Diffuse_4_origianl_ST.xy + _Chicken_Leg_Diffuse_4_origianl_ST.zw;
			o.Albedo = tex2D( _Chicken_Leg_Diffuse_4_origianl, uv_Chicken_Leg_Diffuse_4_origianl ).rgb;
			float2 uv_M_Chicken_Leg_ambient_occlusion = i.uv_texcoord * _M_Chicken_Leg_ambient_occlusion_ST.xy + _M_Chicken_Leg_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _M_Chicken_Leg_ambient_occlusion, uv_M_Chicken_Leg_ambient_occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;745;376;1;True;False
Node;AmplifyShaderEditor.SamplerNode;4;-460,-75;Float;True;Property;_Chicken_Leg_Diffuse_4_origianl;Chicken_Leg_Diffuse_4_origianl;1;0;Create;True;0;4ef6156715745bd46aea226bd9fef3ae;4ef6156715745bd46aea226bd9fef3ae;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-407,192;Float;True;Property;_M_Chicken_Leg_ambient_occlusion;M_Chicken_Leg_ambient_occlusion;0;0;Create;True;0;9fc7c6123089d6747aac7f2cab154e4f;9fc7c6123089d6747aac7f2cab154e4f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/ChickenLeg;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;4;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=3B6D58AFC0B05D774859B6B154E437A9365EFCFD