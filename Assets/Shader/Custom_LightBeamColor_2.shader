Shader "Custom/LightBeamColor" {
	Properties {
		[HideInInspector] _Color ("Color", Vector) = (1,1,1,1)
		_FadeDist ("Fade Distance", Float) = 12
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

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Transparent/VertexLit"
	//CustomEditor "LightBeamColorEditor"
}