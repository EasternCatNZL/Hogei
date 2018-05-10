// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/LambRack"
{
	Properties
	{
		_M_LambRack_ambient_occlusion("M_LambRack_ambient_occlusion", 2D) = "white" {}
		_LambRack_D1("LambRack_D 1", 2D) = "white" {}
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

		uniform sampler2D _LambRack_D1;
		uniform float4 _LambRack_D1_ST;
		uniform sampler2D _M_LambRack_ambient_occlusion;
		uniform float4 _M_LambRack_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_LambRack_D1 = i.uv_texcoord * _LambRack_D1_ST.xy + _LambRack_D1_ST.zw;
			o.Albedo = tex2D( _LambRack_D1, uv_LambRack_D1 ).rgb;
			float2 uv_M_LambRack_ambient_occlusion = i.uv_texcoord * _M_LambRack_ambient_occlusion_ST.xy + _M_LambRack_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _M_LambRack_ambient_occlusion, uv_M_LambRack_ambient_occlusion ).r;
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
Node;AmplifyShaderEditor.SamplerNode;3;-235,-227;Float;True;Property;_LambRack_D1;LambRack_D 1;2;0;Create;True;0;f1db18d2352b10b40b096537dd37cb0a;f1db18d2352b10b40b096537dd37cb0a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-319,-103;Float;True;Property;_LambRack_D;LambRack_D;0;0;Create;True;0;db7ee636fa7329242a10fef3d7ec4f66;db7ee636fa7329242a10fef3d7ec4f66;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-365,158;Float;True;Property;_M_LambRack_ambient_occlusion;M_LambRack_ambient_occlusion;1;0;Create;True;0;c37560e91b99bf947b47eeeee6984597;c37560e91b99bf947b47eeeee6984597;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/LambRack;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;3;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=386EA43BFAC0EFED1DFAF0B69667FFFA3423550B