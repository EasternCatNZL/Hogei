// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/BigFence"
{
	Properties
	{
		_M_Fences_D("M_Fences_D", 2D) = "white" {}
		_AO_BigFence("AO_BigFence", 2D) = "white" {}
		_Metal_Intensity("Metal_Intensity", Range( 0 , 1)) = 0
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

		uniform sampler2D _M_Fences_D;
		uniform float4 _M_Fences_D_ST;
		uniform float _Metal_Intensity;
		uniform sampler2D _AO_BigFence;
		uniform float4 _AO_BigFence_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_M_Fences_D = i.uv_texcoord * _M_Fences_D_ST.xy + _M_Fences_D_ST.zw;
			o.Albedo = tex2D( _M_Fences_D, uv_M_Fences_D ).rgb;
			o.Metallic = _Metal_Intensity;
			float2 uv_AO_BigFence = i.uv_texcoord * _AO_BigFence_ST.xy + _AO_BigFence_ST.zw;
			o.Occlusion = tex2D( _AO_BigFence, uv_AO_BigFence ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;857;260;1;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-493,-86;Float;True;Property;_M_Fences_D;M_Fences_D;0;0;Create;True;0;638036d2415969b4a833304eb36af701;638036d2415969b4a833304eb36af701;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-452,159;Float;False;Property;_Metal_Intensity;Metal_Intensity;2;0;Create;True;0;0;0.282353;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-469,398;Float;True;Property;_AO_BigFence;AO_BigFence;1;0;Create;True;0;3338c4c3ee7c9c1428af850a451aa66e;3338c4c3ee7c9c1428af850a451aa66e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/BigFence;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;3;3;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=CE8363418D82AAED03343897C63BE740B12C3853