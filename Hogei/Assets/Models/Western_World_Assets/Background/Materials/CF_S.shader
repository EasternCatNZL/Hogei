// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/ChickenF"
{
	Properties
	{
		_Chicken_Feed_ambient_occlusion("Chicken_Feed_ambient_occlusion", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 1)) = 0
		_CF_d2("CF_d 2", 2D) = "white" {}
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

		uniform sampler2D _CF_d2;
		uniform float4 _CF_d2_ST;
		uniform float _Float0;
		uniform sampler2D _Chicken_Feed_ambient_occlusion;
		uniform float4 _Chicken_Feed_ambient_occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_CF_d2 = i.uv_texcoord * _CF_d2_ST.xy + _CF_d2_ST.zw;
			o.Albedo = ( tex2D( _CF_d2, uv_CF_d2 ) * _Float0 ).rgb;
			float2 uv_Chicken_Feed_ambient_occlusion = i.uv_texcoord * _Chicken_Feed_ambient_occlusion_ST.xy + _Chicken_Feed_ambient_occlusion_ST.zw;
			o.Occlusion = tex2D( _Chicken_Feed_ambient_occlusion, uv_Chicken_Feed_ambient_occlusion ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;652;479;1;True;False
Node;AmplifyShaderEditor.SamplerNode;10;-454,-336;Float;True;Property;_CF_d2;CF_d 2;3;0;Create;True;0;da2ea7847624aa54c89bcb7a2b2cb1bb;da2ea7847624aa54c89bcb7a2b2cb1bb;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-349,26;Float;False;Property;_Float0;Float 0;2;0;Create;True;0;0;0.6705883;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-125,-143;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-461,-235;Float;True;Property;_CF_d;CF_d;0;0;Create;True;0;27d441574f78f2a46b14ca7637ac3dd4;27d441574f78f2a46b14ca7637ac3dd4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-467,204;Float;True;Property;_Chicken_Feed_ambient_occlusion;Chicken_Feed_ambient_occlusion;1;0;Create;True;0;008d1fc660b7f174dbe2c9a510aea321;008d1fc660b7f174dbe2c9a510aea321;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/ChickenF;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;10;0
WireConnection;9;1;6;0
WireConnection;0;0;9;0
WireConnection;0;5;2;0
ASEEND*/
//CHKSM=CEF34CD5B379D4FCA3262E0B099842A40C1BA73A