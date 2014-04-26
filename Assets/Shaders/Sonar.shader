Shader "Custom/Sonar" {
	Properties {
		_SonarOrigin ("Origin", Vector) = (0,0,0,0)
		_SonarDistance ("Distance", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		float3 _SonarOrigin;
		float _SonarDistance;
		float _SonarMaxDistance;

		struct Input {
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float dist = distance(_SonarOrigin, IN.worldPos);
			float proximity = abs(dist - _SonarDistance) - 0.5;
			float percentComplete = 1 - _SonarDistance / _SonarMaxDistance;
			
			o.Albedo = half3(0,0,0);
			o.Alpha = 0;
			o.Emission = saturate((0.1 / pow(proximity, 10))) * percentComplete;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
