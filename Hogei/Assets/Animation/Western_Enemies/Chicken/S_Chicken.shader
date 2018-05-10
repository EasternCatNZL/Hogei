// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Chicken"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Chicken_Real_2_D("Chicken_Real_2_D", 2D) = "white" {}
		_Chicken_D_ambient_occlusion("Chicken_D_ambient_occlusion", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Overlay+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Chicken_Real_2_D;
		uniform float4 _Chicken_Real_2_D_ST;
		uniform sampler2D _Chicken_D_ambient_occlusion;
		uniform float4 _Chicken_D_ambient_occlusion_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Chicken_Real_2_D = i.uv_texcoord * _Chicken_Real_2_D_ST.xy + _Chicken_Real_2_D_ST.zw;
			float4 tex2DNode1 = tex2D( _Chicken_Real_2_D, uv_Chicken_Real_2_D );
			o.Albedo = tex2DNode1.rgb;
			float2 uv_Chicken_D_ambient_occlusion = i.uv_texcoord * _Chicken_D_ambient_occlusion_ST.xy + _Chicken_D_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _Chicken_D_ambient_occlusion, uv_Chicken_D_ambient_occlusion ).r;
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
1927;29;1266;958;702;360;1;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-461,165;Float;True;Property;_Chicken_D_ambient_occlusion;Chicken_D_ambient_occlusion;2;0;Create;True;0;08feffe888f762f45b53046c87b0ca39;08feffe888f762f45b53046c87b0ca39;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-463,-106;Float;True;Property;_Chicken_Real_2_D;Chicken_Real_2_D;1;0;Create;True;0;713bc2270764c2b47930593858f51f11;713bc2270764c2b47930593858f51f11;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;109,-33;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Chicken;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Custom;0.5;True;True;0;True;Opaque;;Overlay;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;5;2;0
WireConnection;0;10;1;4
ASEEND*/
//CHKSM=B92E054355BB12942170E4DE0B29554A5A08B398