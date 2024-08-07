Shader "Custom/Eye Renderer" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpecMap ("Specular map", 2D) = "black" {}
		_SpecColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range(0.03, 1)) = 0.078125
		_CubeMap ("Cube map", Cube) = "" {}
		_Sensitivity ("Light Sensitivity", Range(1, 10)) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/VertexLit"
}