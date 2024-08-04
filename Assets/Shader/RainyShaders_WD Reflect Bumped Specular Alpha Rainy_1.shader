Shader "RainyShaders/WD Reflect Bumped Specular Alpha Rainy" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_SpecColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range(0.01, 1)) = 0.078125
		_ReflectColor ("Reflection Color", Vector) = (1,1,1,0.5)
		_MainTex ("Base (RGB) RefStrGloss (A)", 2D) = "white" {}
		_Cube ("Reflection Cubemap", Cube) = "" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_RainFlow ("RainFlow", 2D) = "black" {}
		_RainGradient ("RainGradient", 2D) = "black" {}
		_RainPower ("RainPower", Range(0, 2)) = 1
		_RainSpeed ("RainSpeed", Range(0, 2)) = 1
		_RainTilingX ("RainTilingX", Float) = 1
		_RainTilingY ("RainTilingY", Float) = 1
		_fallow ("_RimReflection", Range(0.01, 1.5)) = 1
		_level ("_RimReflection_2", Range(0.01, 1.5)) = 1
		_WaterContrast ("WaterContrast", Range(0, 2)) = 0.5
		_RunTime ("Runtime", Float) = 0
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
}