// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Grass_2"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_M_Grass_4("M_Grass_4", 2D) = "white" {}
		_Lightness("Lightness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _M_Grass_4;
		uniform float4 _M_Grass_4_ST;
		uniform float _Lightness;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_M_Grass_4 = i.uv_texcoord * _M_Grass_4_ST.xy + _M_Grass_4_ST.zw;
			float4 tex2DNode1 = tex2D( _M_Grass_4, uv_M_Grass_4 );
			o.Albedo = ( tex2DNode1 * _Lightness ).rgb;
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
1930;29;1263;958;903.889;328.7237;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;5;-466.889,175.2763;Float;False;Property;_Lightness;Lightness;2;0;Create;True;0;0;0.8;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-506,-40;Float;True;Property;_M_Grass_4;M_Grass_4;1;0;Create;True;0;1559981d557b4fc4b972884099bb9968;92dc4e43d43eeb242947d1134529e4dc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-187,26;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;3;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Lambert;Shader/Grass_2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;1;0
WireConnection;4;1;5;0
WireConnection;3;0;4;0
WireConnection;3;10;1;4
ASEEND*/
//CHKSM=20E41DFB6E5CF9E5C3EA81B431849958B75D3899