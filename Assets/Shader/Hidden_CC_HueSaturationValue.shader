Shader "Hidden/CC_HueSaturationValue" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Master ("Master (HSV)", Vector) = (0,0,0,0)
		_Reds ("Reds (HSV)", Vector) = (0,0,0,0)
		_Yellows ("Yellows (HSV)", Vector) = (0,0,0,0)
		_Greens ("Greens (HSV)", Vector) = (0,0,0,0)
		_Cyans ("Cyans (HSV)", Vector) = (0,0,0,0)
		_Blues ("Blues (HSV)", Vector) = (0,0,0,0)
		_Magentas ("Magentas (HSV)", Vector) = (0,0,0,0)
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