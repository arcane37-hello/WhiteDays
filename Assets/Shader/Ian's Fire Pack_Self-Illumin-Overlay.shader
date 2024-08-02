Shader "Ian's Fire Pack/Self-Illumin-Overlay" {
	Properties {
		_Color ("Illum Color", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB) Alum(A)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Mask ("Overlay Mask (A)", 2D) = "white" {}
		_Overlay ("Overlay (RGB)", 2D) = "white" {}
		_OverlayColor ("Overlay Color", Vector) = (1,1,1,1)
		_ScrollSpeed ("ScrollSpeed (Overlay)", Float) = 1
		_OverlayScale ("Overlay Scale", Range(0, 5)) = 1
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
	Fallback "Legacy Shaders/Self-Illumin/VertexLit"
	//CustomEditor "LegacyIlluminShaderGUI"
}