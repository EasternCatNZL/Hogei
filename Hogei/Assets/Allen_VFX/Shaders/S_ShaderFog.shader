// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/ShaderFog"
{
	Properties
	{
		_FogIntensity("FogIntensity", Range( 0 , 1)) = 0.5176471
		_FogMaxIntensity("FogMaxIntensity", Range( 0 , 1)) = 0
		_FogColor("FogColor", Color) = (0.1906358,0.6323529,0.4312954,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Cloud_Speed("Cloud_Speed", Range( 0 , 0.08)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float _Cloud_Speed;
		uniform float4 _FogColor;
		uniform sampler2D _CameraDepthTexture;
		uniform float _FogIntensity;
		uniform float _FogMaxIntensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (_Cloud_Speed).xx;
			float2 uv_TexCoord17 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner18 = ( uv_TexCoord17 + 1 * _Time.y * temp_cast_0);
			o.Albedo = ( tex2D( _TextureSample0, panner18 ) * _FogColor ).rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float eyeDepth7 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float clampResult14 = clamp( ( abs( ( eyeDepth7 - ase_screenPos.w ) ) * (0.01 + (_FogIntensity - 0) * (0.4 - 0.01) / (1 - 0)) ) , 0 , _FogMaxIntensity );
			o.Alpha = clampResult14;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1923;133;1266;958;1422.786;176.8198;1;True;False
Node;AmplifyShaderEditor.ScreenPosInputsNode;6;-1057.856,301.8605;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;7;-834.7145,208.1857;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;8;-841.3359,409.2572;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1082.604,554.3021;Float;False;Property;_FogIntensity;FogIntensity;0;0;Create;True;0;0.5176471;0.278;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1087.575,-135.8656;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1058.02,42.04191;Float;False;Property;_Cloud_Speed;Cloud_Speed;5;0;Create;True;0;0;0.0509;0;0.08;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;12;-781.6041,542.302;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.01;False;4;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;10;-693.8604,418.5256;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;18;-782.9841,-124.3715;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1153.522,816.1639;Float;False;Property;_FogMaxIntensity;FogMaxIntensity;1;0;Create;True;0;0;0.43;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-491.7333,83.29147;Float;False;Property;_FogColor;FogColor;3;0;Create;True;0;0.1906358,0.6323529,0.4312954,0;0.1937716,0.3937238,0.4117647,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-565.5607,417.952;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;-528.6796,-131.5553;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;None;e28dc97a9541e3642a48c0e3886688c5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-183.8197,82.60037;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;22;-412.8591,259.5854;Float;False;Property;_Color0;Color 0;2;0;Create;True;0;0.1906358,0.6323529,0.4312954,0;0.1498702,0.4575348,0.485294,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;14;-643.4933,766.3256;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/ShaderFog;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;Back;0;0;False;0;0;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;False;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;6;0
WireConnection;8;0;7;0
WireConnection;8;1;6;4
WireConnection;12;0;11;0
WireConnection;10;0;8;0
WireConnection;18;0;17;0
WireConnection;18;2;20;0
WireConnection;9;0;10;0
WireConnection;9;1;12;0
WireConnection;16;1;18;0
WireConnection;21;0;16;0
WireConnection;21;1;15;0
WireConnection;14;0;9;0
WireConnection;14;2;13;0
WireConnection;0;0;21;0
WireConnection;0;9;14;0
ASEEND*/
//CHKSM=D44A6E7513ED597E8B77834D3265E2DEB4CA5C9B