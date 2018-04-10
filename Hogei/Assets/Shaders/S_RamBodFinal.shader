// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/RamBodFinal"
{
	Properties
	{
		_M_Ram_D1("M_Ram_D1", 2D) = "white" {}
		_Metallic_Intensity("Metallic_Intensity", Range( 0 , 1)) = 0
		_Emission("Emission", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _M_Ram_D1;
		uniform float4 _M_Ram_D1_ST;
		uniform float _Emission;
		uniform float _Metallic_Intensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_M_Ram_D1 = i.uv_texcoord * _M_Ram_D1_ST.xy + _M_Ram_D1_ST.zw;
			o.Albedo = tex2D( _M_Ram_D1, uv_M_Ram_D1 ).rgb;
			float3 temp_cast_1 = (_Emission).xxx;
			o.Emission = temp_cast_1;
			o.Metallic = _Metallic_Intensity;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;810.5;306;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;4;-318.5,197;Float;False;Property;_Emission;Emission;3;0;Create;True;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-397.5,-224;Float;True;Property;_M_Ram_D2;M_Ram_D2;2;0;Create;True;0;6809ffc8c66424c41974a2c1b20eb1c1;6809ffc8c66424c41974a2c1b20eb1c1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-514.5,-35;Float;True;Property;_M_Ram_D1;M_Ram_D1;0;0;Create;True;0;ce5e810872eb014468beb66292dc7087;ce5e810872eb014468beb66292dc7087;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;2;-349.5,300;Float;False;Property;_Metallic_Intensity;Metallic_Intensity;1;0;Create;True;0;0;0.1176471;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;87,6;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/RamBodFinal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;2;4;0
WireConnection;0;3;2;0
ASEEND*/
//CHKSM=66BE7FAB02B709AA284B2B680F234C99D6E86136