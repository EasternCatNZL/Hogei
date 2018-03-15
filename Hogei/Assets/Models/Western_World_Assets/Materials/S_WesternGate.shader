// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/WesternGate"
{
	Properties
	{
		_M_SkeletonGate_D("M_SkeletonGate_D", 2D) = "white" {}
		_M_GateKeeper_D_ambient_occlusion("M_GateKeeper_D_ambient_occlusion", 2D) = "white" {}
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

		uniform sampler2D _M_SkeletonGate_D;
		uniform float4 _M_SkeletonGate_D_ST;
		uniform sampler2D _M_GateKeeper_D_ambient_occlusion;
		uniform float4 _M_GateKeeper_D_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_M_SkeletonGate_D = i.uv_texcoord * _M_SkeletonGate_D_ST.xy + _M_SkeletonGate_D_ST.zw;
			o.Albedo = tex2D( _M_SkeletonGate_D, uv_M_SkeletonGate_D ).rgb;
			float2 uv_M_GateKeeper_D_ambient_occlusion = i.uv_texcoord * _M_GateKeeper_D_ambient_occlusion_ST.xy + _M_GateKeeper_D_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _M_GateKeeper_D_ambient_occlusion, uv_M_GateKeeper_D_ambient_occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;1366.443;356.9654;1.657027;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-567.4523,-11.18697;Float;True;Property;_M_SkeletonGate_D;M_SkeletonGate_D;0;0;Create;True;0;ebafd2faa500e0c409430a329eb2fe51;ebafd2faa500e0c409430a329eb2fe51;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-635.9039,340.8658;Float;True;Property;_M_GateKeeper_D_ambient_occlusion;M_GateKeeper_D_ambient_occlusion;1;0;Create;True;0;086d7e51dc6e32741a23b66652ac8230;086d7e51dc6e32741a23b66652ac8230;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/WesternGate;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=44386DCF6238BC24FD6A7E7EA8612044F3C6B23F