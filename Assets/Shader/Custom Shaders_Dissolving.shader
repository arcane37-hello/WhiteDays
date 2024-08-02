Shader "Custom Shaders/Dissolving" {
	Properties {
		_MainTex ("Texture (RGB)", 2D) = "white" {}
		_SliceGuide ("Slice Guide (RGB)", 2D) = "white" {}
		_SliceAmount ("Slice Amount", Range(0, 1)) = 0
		_Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
		_DissolveEdgeColor ("Dissolve Edge Color", Vector) = (0.86,0.192,0.192,0)
		_DissolveEdgeRange ("Dissolve Edge Range", Range(0, 1)) = 0.01
		_DissolveEdgeMultiplier ("Dissolve Edge Multiplier", Float) = 120
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}