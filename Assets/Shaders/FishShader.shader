﻿Shader "Custom/FishShader" {
	Properties {
		_MainCol ("Base", Color) = (1,1,1,1)
		_FearCol ("Fear", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		float4 _MainCol;
		float4 _FearCol;

		struct Input {
			half noting;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _MainCol.rgb;
			o.Alpha = _MainCol.a;
			o.Emission = _FearCol * sin(_Time.z);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
