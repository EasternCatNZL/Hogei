// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Whisky"
{
	Properties
	{
		_M_Whiskey_ambient_occlusion("M_Whiskey_ambient_occlusion", 2D) = "white" {}
		_Metal_value("Metal_value", Range( 0 , 1)) = 0
		_Smooth_Value("Smooth_Value", Range( 0 , 1)) = 0
		_Whisky_Diffuse_2("Whisky_Diffuse_2", 2D) = "white" {}
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

		uniform sampler2D _Whisky_Diffuse_2;
		uniform float4 _Whisky_Diffuse_2_ST;
		uniform float _Metal_value;
		uniform float _Smooth_Value;
		uniform sampler2D _M_Whiskey_ambient_occlusion;
		uniform float4 _M_Whiskey_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Whisky_Diffuse_2 = i.uv_texcoord * _Whisky_Diffuse_2_ST.xy + _Whisky_Diffuse_2_ST.zw;
			o.Albedo = tex2D( _Whisky_Diffuse_2, uv_Whisky_Diffuse_2 ).rgb;
			o.Metallic = _Metal_value;
			o.Smoothness = _Smooth_Value;
			float2 uv_M_Whiskey_ambient_occlusion = i.uv_texcoord * _M_Whiskey_ambient_occlusion_ST.xy + _M_Whiskey_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _M_Whiskey_ambient_occlusion, uv_M_Whiskey_ambient_occlusion ).r;
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
Node;AmplifyShaderEditor.SamplerNode;2;-357,227;Float;True;Property;_M_Whiskey_ambient_occlusion;M_Whiskey_ambient_occlusion;1;0;Create;True;0;de61ef29bd7d9334ea189740b949104d;de61ef29bd7d9334ea189740b949104d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-348,67;Float;False;Property;_Metal_value;Metal_value;2;0;Create;True;0;0;0.5529412;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-351,144;Float;False;Property;_Smooth_Value;Smooth_Value;3;0;Create;True;0;0;0.1411765;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;79,-329;Float;True;Property;_Whisky_Diffuse_2;Whisky_Diffuse_2;5;0;Create;True;0;bb0c1d63bc748fd438d41b7b28486054;bb0c1d63bc748fd438d41b7b28486054;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-218,-252;Float;True;Property;_Whisky_Diffuse_1;Whisky_Diffuse_1;4;0;Create;True;0;e0fbdf3b3a5df2e4c8f60b849dae62dd;e0fbdf3b3a5df2e4c8f60b849dae62dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-404,-137;Float;True;Property;_Whisky_Diffuse;Whisky_Diffuse;0;0;Create;True;0;3da9872a930616041a2808989f87cac1;3da9872a930616041a2808989f87cac1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Whisky;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;6;0
WireConnection;0;3;3;0
WireConnection;0;4;4;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=F92E06CC1FE3F53056B0CD1141A6F5BA7F804C80