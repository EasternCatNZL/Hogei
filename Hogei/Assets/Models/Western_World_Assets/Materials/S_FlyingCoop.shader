// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/FlyingCoop"
{
	Properties
	{
		_M_FlyingCoop_D_ambient_occlusion("M_FlyingCoop_D_ambient_occlusion", 2D) = "white" {}
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

		uniform sampler2D _M_FlyingCoop_D_ambient_occlusion;
		uniform float4 _M_FlyingCoop_D_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = float4(0.4264706,0.4264706,0.4264706,0).rgb;
			float2 uv_M_FlyingCoop_D_ambient_occlusion = i.uv_texcoord * _M_FlyingCoop_D_ambient_occlusion_ST.xy + _M_FlyingCoop_D_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _M_FlyingCoop_D_ambient_occlusion, uv_M_FlyingCoop_D_ambient_occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;734;456;1;True;False
Node;AmplifyShaderEditor.ColorNode;2;-415,-33;Float;False;Constant;_Color0;Color 0;1;0;Create;True;0;0.4264706,0.4264706,0.4264706,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-429,223;Float;True;Property;_M_FlyingCoop_D_ambient_occlusion;M_FlyingCoop_D_ambient_occlusion;0;0;Create;True;0;0952376f9f777ab4f81bfdcf2f4137c3;0952376f9f777ab4f81bfdcf2f4137c3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/FlyingCoop;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;2;0
WireConnection;0;5;1;0
ASEEND*/
//CHKSM=E291910F2A37F3587552F4785507C610A7BA958B