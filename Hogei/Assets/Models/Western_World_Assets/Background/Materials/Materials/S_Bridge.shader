// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Bridge"
{
	Properties
	{
		_Bridge_D("Bridge_D", 2D) = "white" {}
		_Bridge_ambient_occlusion("Bridge_ambient_occlusion", 2D) = "white" {}
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

		uniform sampler2D _Bridge_D;
		uniform float4 _Bridge_D_ST;
		uniform sampler2D _Bridge_ambient_occlusion;
		uniform float4 _Bridge_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Bridge_D = i.uv_texcoord * _Bridge_D_ST.xy + _Bridge_D_ST.zw;
			o.Albedo = tex2D( _Bridge_D, uv_Bridge_D ).rgb;
			float2 uv_Bridge_ambient_occlusion = i.uv_texcoord * _Bridge_ambient_occlusion_ST.xy + _Bridge_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _Bridge_ambient_occlusion, uv_Bridge_ambient_occlusion ).r;
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
Node;AmplifyShaderEditor.SamplerNode;2;-380,231;Float;True;Property;_Bridge_ambient_occlusion;Bridge_ambient_occlusion;1;0;Create;True;0;55d4e4d3cab81844987b36438fe70d3d;55d4e4d3cab81844987b36438fe70d3d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-347,-20;Float;True;Property;_Bridge_D;Bridge_D;0;0;Create;True;0;559e4465cf2e89048b7fec826fc2b26c;559e4465cf2e89048b7fec826fc2b26c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Bridge;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=196033B52ED15D55EB78F692B19A54945D775253