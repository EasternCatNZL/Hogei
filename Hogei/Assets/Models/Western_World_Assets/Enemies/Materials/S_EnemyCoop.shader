// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/EnemyCoop"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0
		_Coop_AO_2("Coop_AO_2", 2D) = "white" {}
		_F_Coop_D_26("F_Coop_D_26", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _F_Coop_D_26;
		uniform float4 _F_Coop_D_26_ST;
		uniform sampler2D _Coop_AO_2;
		uniform float4 _Coop_AO_2_ST;
		uniform float _Cutoff = 0;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_F_Coop_D_26 = i.uv_texcoord * _F_Coop_D_26_ST.xy + _F_Coop_D_26_ST.zw;
			float4 tex2DNode7 = tex2D( _F_Coop_D_26, uv_F_Coop_D_26 );
			o.Albedo = tex2DNode7.rgb;
			float2 uv_Coop_AO_2 = i.uv_texcoord * _Coop_AO_2_ST.xy + _Coop_AO_2_ST.zw;
			o.Occlusion = tex2D( _Coop_AO_2, uv_Coop_AO_2 ).r;
			o.Alpha = 1;
			clip( tex2DNode7.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;939;248;1;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-433,538;Float;True;Property;_Coop_AO;Coop_AO;2;0;Create;True;0;3bc5e01db08d93242b29b2d9a47521f9;3bc5e01db08d93242b29b2d9a47521f9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-506,275;Float;True;Property;_M_CoopEnemy_Opacity;M_CoopEnemy_Opacity;4;0;Create;True;0;5f3ed92c7685f3042a2b06978e23247d;5f3ed92c7685f3042a2b06978e23247d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-407,79;Float;True;Property;_Coop_AO_2;Coop_AO_2;3;0;Create;True;0;447205b3d630e1548b0bb9cf4d9dba06;447205b3d630e1548b0bb9cf4d9dba06;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-467,-129;Float;True;Property;_F_Coop_D_26;F_Coop_D_26;5;0;Create;True;0;eeeed46ded7046a4e9c78aa3ff73d7ff;eeeed46ded7046a4e9c78aa3ff73d7ff;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-711,83;Float;True;Property;_F_Coop_D_25;F_Coop_D_25;0;0;Create;True;0;7daaf83fd42737848ae0c994b09b7e84;7daaf83fd42737848ae0c994b09b7e84;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2,-3;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/EnemyCoop;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Custom;0;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;7;0
WireConnection;0;5;3;0
WireConnection;0;10;7;4
ASEEND*/
//CHKSM=65A8E36A1B0305F3FD792237E173ACDEF2C33457