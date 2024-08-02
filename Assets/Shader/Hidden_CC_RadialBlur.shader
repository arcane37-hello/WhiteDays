Shader "Hidden/CC_RadialBlur" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Amount ("Amount", Range(0, 1)) = 0.1
		_Center ("Center Point", Vector) = (0.5,0.5,0,0)
		_Samples ("Samples", Float) = 10
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