// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Anchor"
{
	Properties
	{
		_Anchor_AO("Anchor_AO", 2D) = "white" {}
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

		uniform sampler2D _Anchor_AO;
		uniform float4 _Anchor_AO_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = float4(0.02243727,0.02668608,0.03676468,0).rgb;
			float2 uv_Anchor_AO = i.uv_texcoord * _Anchor_AO_ST.xy + _Anchor_AO_ST.zw;
			o.Occlusion = tex2D( _Anchor_AO, uv_Anchor_AO ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1949;67;1266;958;929.5;271;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-665.5,99;Float;False;Property;_Float0;Float 0;1;0;Create;True;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-643.5,-76;Float;False;Constant;_Color0;Color 0;1;1;[HDR];Create;True;0;0.02243727,0.02668608,0.03676468,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-434.5,201;Float;True;Property;_Anchor_AO;Anchor_AO;0;0;Create;True;0;b3e6dd9ca925ed44bb76c4d297f5a897;b3e6dd9ca925ed44bb76c4d297f5a897;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;12;-588.5,244;Float;False;Property;_Int0;Int 0;2;0;Create;True;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-34,-12;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Anchor;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;9;0
WireConnection;0;5;1;0
ASEEND*/
//CHKSM=FBD3035EC64FB64439F888D878995437F2C6A855