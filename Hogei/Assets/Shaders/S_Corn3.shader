// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Corn3"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_M_Corns_D("M_Corns_D", 2D) = "white" {}
		_AO_Corn3("AO_Corn3", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 1)) = 0.2352941
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _M_Corns_D;
		uniform float4 _M_Corns_D_ST;
		uniform float _Float0;
		uniform sampler2D _AO_Corn3;
		uniform float4 _AO_Corn3_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_M_Corns_D = i.uv_texcoord * _M_Corns_D_ST.xy + _M_Corns_D_ST.zw;
			float4 tex2DNode1 = tex2D( _M_Corns_D, uv_M_Corns_D );
			o.Albedo = tex2DNode1.rgb;
			o.Smoothness = _Float0;
			float2 uv_AO_Corn3 = i.uv_texcoord * _AO_Corn3_ST.xy + _AO_Corn3_ST.zw;
			o.Occlusion = tex2D( _AO_Corn3, uv_AO_Corn3 ).r;
			o.Alpha = 1;
			clip( tex2DNode1.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;964.5275;254.2072;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;3;-422.5275,223.7928;Float;False;Property;_Float0;Float 0;3;0;Create;True;0;0.2352941;0.021;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-537.3285,-34.50879;Float;True;Property;_M_Corns_D;M_Corns_D;1;0;Create;True;0;10efeb1f6cb20d447863f160cf449e0f;10efeb1f6cb20d447863f160cf449e0f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-520.5275,410.7928;Float;True;Property;_AO_Corn3;AO_Corn3;2;0;Create;True;0;727ef55caabe31845b3169a1a19132a2;727ef55caabe31845b3169a1a19132a2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Corn3;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;4;3;0
WireConnection;0;5;2;0
WireConnection;0;10;1;4
ASEEND*/
//CHKSM=D635C605972B3374EF86F3AAD697F08A7D37C4D2