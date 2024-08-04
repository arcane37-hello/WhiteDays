Shader "Custom/LightBeamTexture" {
	Properties {
		[HideInInspector] _Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_FadeDist ("Fade Distance", Float) = 12
		_TimeXInc ("Time movement x", Float) = 0.01
		_TimeYInc ("Time movement y", Float) = 0.02
		_LerpStart ("Lerp start", Float) = -0.5
		_LerpEnd ("Lerp end", Float) = 2.5
		_Power ("Fade Power", Float) = 2
		_NormalPower ("Normal Power", Float) = 1
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
	Fallback "Transparent/VertexLit"
	//CustomEditor "LightBeamColorEditor"
}