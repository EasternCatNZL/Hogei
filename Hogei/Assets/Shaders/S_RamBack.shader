// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/RamBack"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Emission("Emission", Range( 0 , 1)) = 0
		_Spikes_2("Spikes_2", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Spikes_2;
		uniform float4 _Spikes_2_ST;
		uniform float _Emission;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Spikes_2 = i.uv_texcoord * _Spikes_2_ST.xy + _Spikes_2_ST.zw;
			float4 tex2DNode4 = tex2D( _Spikes_2, uv_Spikes_2 );
			o.Albedo = tex2DNode4.rgb;
			float3 temp_cast_1 = (_Emission).xxx;
			o.Emission = temp_cast_1;
			o.Alpha = 1;
			clip( tex2DNode4.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;719.5;378;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;2;-329.5,130;Float;False;Property;_Emission;Emission;2;0;Create;True;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-450.5,359;Float;True;Property;_M_Ram_Spikes_D;M_Ram_Spikes_D;1;0;Create;True;0;80e0ad205141a084181ec7d17c8b37eb;80e0ad205141a084181ec7d17c8b37eb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-374.5,-101;Float;True;Property;_Spikes_1;Spikes_1;3;0;Create;True;0;197ff1061a80f394e8be2067f3c64472;197ff1061a80f394e8be2067f3c64472;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-367.5,-264;Float;True;Property;_Spikes_2;Spikes_2;4;0;Create;True;0;33f9334ec3209b34d9213d6a26c9051a;33f9334ec3209b34d9213d6a26c9051a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;104,-63;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/RamBack;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;4;0
WireConnection;0;2;2;0
WireConnection;0;10;4;4
ASEEND*/
//CHKSM=89B73100E9158B836D38C28B501CB23731732343