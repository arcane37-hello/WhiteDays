Shader "Hidden/CC_Levels" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_InputMin ("Input Black", Vector) = (0,0,0,1)
		_InputMax ("Input White", Vector) = (1,1,1,1)
		_InputGamma ("Input Gamma", Vector) = (1,1,1,1)
		_OutputMin ("Output Black", Vector) = (0,0,0,1)
		_OutputMax ("Output White", Vector) = (1,1,1,1)
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