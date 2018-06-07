// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader/Water"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Vector1("Vector 1", Vector) = (0,0,0,0)
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_Water_Emission("Water_Emission", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float2 _Vector0;
		uniform sampler2D _Water_Emission;
		uniform float2 _Vector1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime5 = _Time.y * 1;
			float2 uv_TexCoord3 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner2 = ( uv_TexCoord3 + mulTime5 * _Vector0);
			o.Albedo = tex2D( _TextureSample0, panner2 ).rgb;
			float mulTime10 = _Time.y * 1;
			float2 uv_TexCoord8 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner11 = ( uv_TexCoord8 + mulTime10 * _Vector1);
			o.Emission = tex2D( _Water_Emission, panner11 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
1927;29;1266;958;620.4041;337.7426;1;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;10;-608.6696,480.2981;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;9;-337.6695,578.2981;Float;False;Property;_Vector1;Vector 1;1;0;Create;True;0;0,0;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-654.5059,237.6963;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-613.5,-192;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;6;-296.6637,148.6018;Float;False;Property;_Vector0;Vector 0;2;0;Create;True;0;0,0;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;5;-567.6637,50.60181;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;11;-337.5058,305.6963;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;2;-296.5,-124;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;13;-27.40411,-280.7426;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;None;8d5fb6abfa9888e48a86f7dca85f6570;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-80.5,-107;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;None;5bf04b8fdcf10f8438782f40dfad70d8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-542.6637,-40.39819;Float;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-583.6696,389.2981;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-59.40411,253.2574;Float;True;Property;_Water_Emission;Water_Emission;3;0;Create;True;0;None;ff6918bd2886b0646b5545f255addc3f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;291,-99;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader/Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;8;0
WireConnection;11;2;9;0
WireConnection;11;1;10;0
WireConnection;2;0;3;0
WireConnection;2;2;6;0
WireConnection;2;1;5;0
WireConnection;13;1;2;0
WireConnection;1;1;2;0
WireConnection;7;1;11;0
WireConnection;0;0;1;0
WireConnection;0;2;7;0
ASEEND*/
//CHKSM=41F08D2643FDEF2E44F4EFE79A310288E7256D04