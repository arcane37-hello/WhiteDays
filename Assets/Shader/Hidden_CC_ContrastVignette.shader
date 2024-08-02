Shader "Hidden/CC_ContrastVignette" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Data ("Sharpness (X), Darkness (Y), Contrast (Z), Edge (W)", Vector) = (0.1,0.3,0.25,0.5)
		_Coeffs ("Luminance coeffs (RGB)", Vector) = (0.5,0.5,0.5,1)
		_Center ("Center point (XY)", Vector) = (0.5,0.5,1,1)
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
}