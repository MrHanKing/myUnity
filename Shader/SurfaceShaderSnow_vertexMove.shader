Shader "Custom/SurfaceShaderSnow" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpNormal("Normal map", 2D) = "bump" {}
		_SnowColor("SnowColor", Color) = (1, 1, 1, 1)
		_SnowLevel("SnowLevel", Range(0, 1)) = 0
		_SnowDir("SnowDirection", Vector) = (0,1,0)
		_SnowDepth("SnowDepth", Range(0, 0.5)) = 0.1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpNormal;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpNormal;
			float3 worldNormal;INTERNAL_DATA
		};

		float4 _SnowColor;
		float _SnowLevel;
		float4 _Color;
		float4 _SnowDir;
		float _SnowDepth;

		void vert(inout appdata_full v){
			float4 sn = mul(transpose(_Object2World), _SnowDir);
			if (dot(v.normal, sn.xyz) >= lerp(1, -1, (_SnowLevel * 2) / 3)) {
				v.vertex.xyz += (sn.xyz + v.normal) * _SnowDepth * _SnowLevel;
			}
		}

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			float4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Normal = UnpackNormal(tex2D(_BumpNormal, IN.uv_BumpNormal));
			if (dot(WorldNormalVector(IN, o.Normal), _SnowDir.xyz) > lerp(1, -1, _SnowLevel)) {
				o.Albedo = _SnowColor.rgb;
			}
			else{
				o.Albedo = c.rgb;
			}
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
