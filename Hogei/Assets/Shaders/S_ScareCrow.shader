// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/ScareCrowNew"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_ScareCrow("ScareCrow", 2D) = "white" {}
		_AO_ScareCrow("AO_ScareCrow", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _ScareCrow;
		uniform float4 _ScareCrow_ST;
		uniform sampler2D _AO_ScareCrow;
		uniform float4 _AO_ScareCrow_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_ScareCrow = i.uv_texcoord * _ScareCrow_ST.xy + _ScareCrow_ST.zw;
			float4 tex2DNode1 = tex2D( _ScareCrow, uv_ScareCrow );
			o.Albedo = tex2DNode1.rgb;
			float2 uv_AO_ScareCrow = i.uv_texcoord * _AO_ScareCrow_ST.xy + _AO_ScareCrow_ST.zw;
			o.Occlusion = tex2D( _AO_ScareCrow, uv_AO_ScareCrow ).r;
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
1927;29;1266;958;774.7651;596.5398;1.3;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-389.6,-169.3;Float;True;Property;_ScareCrow;ScareCrow;1;0;Create;True;0;869ebdc6b1d05264da1a8c1e4cebd7a7;e843d87efd4c4aa4583dc128bc351c36;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-356,275;Float;True;Property;_AO_ScareCrow;AO_ScareCrow;2;0;Create;True;0;4a6f9623e7fc7604fb76afda419f8566;4a6f9623e7fc7604fb76afda419f8566;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/ScareCrowNew;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Custom;0.5;True;True;0;False;Opaque;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;5;2;0
WireConnection;0;10;1;4
ASEEND*/
//CHKSM=B9E91E1CC469DC9DDE092CD55F8544FC016DDEDD