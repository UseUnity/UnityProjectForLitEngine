Shader "MJ/Desk" {
	Properties{
		_Color("主颜色", Color) = (1,1,1,1)
		[NoScaleOffset] _MainTex("主贴图", 2D) = "white" {}
		[NoScaleOffset] _LightMask("光照贴图", 2D) = "white" {}
		[NoScaleOffset] _BumpMap("法线", 2D) = "bump" {}
		_normal("Normal", Range(-3, 3)) = -1.48718
		_specular("Specular", Range(0, 3)) = 1.102564
		_emission("Emission", Range(-2, 3)) = 0
		_diffuse("Diffuse", Range(0, 3)) = 1.595953
		}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 250

		CGPROGRAM
		#pragma surface surf MobileBlinnPhong finalcolor:Deskcolor exclude_path:prepass nolightmap noshadow noforwardadd halfasview novertexlights
		
		struct MySurfaceOutput {
			fixed3 Albedo;
			fixed3 Lightmask;
			fixed3 Normal;
			fixed3 Emission;
			fixed Alpha;
		};

		

		inline fixed4 LightingMobileBlinnPhong(MySurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
		{
			half gloss = 0.5;
			half specPow = exp2(gloss * 10.0 + 1.0);

			fixed diff = max(0, dot(s.Normal, lightDir));
			fixed nh = max(0, dot(s.Normal, halfDir));
			fixed spec = (floor(atten) * _LightColor0.rgb) * pow(nh, specPow) * s.Lightmask;

			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * diff * atten +  spec ;
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}

		sampler2D _MainTex;
		sampler2D _LightMask;
		sampler2D _BumpMap;

		half _specular;
		half _emission;
		half _normal;
		half _diffuse;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		void Deskcolor(Input IN, MySurfaceOutput o, inout fixed4 color)
		{
			color *= _Color;
		}

		void surf(Input IN, inout MySurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb * _diffuse;
			o.Alpha = tex.a;

			o.Lightmask = tex2D(_LightMask, IN.uv_MainTex).rgb * _specular;

			fixed3 _BumpMaptex = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
			o.Normal = lerp(_BumpMaptex.rgb, float3(0, 0, 1), _normal);
			
			o.Emission = tex.rgb * _emission;
		}
		ENDCG
	}

	FallBack "Mobile/VertexLit"
}
