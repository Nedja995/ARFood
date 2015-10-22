Shader "Custom/TextureChange" {
	Properties{
		_Blend("Blend", Range(0, 1)) = 0.5
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Texture 1", 2D) = "white" {}
		_Texture2("Texture 2", 2D) = ""
		_BumpMap("Normalmap", 2D) = "bump" {}

	/*	_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess("Shininess", Range(0.01, 1)) = 0.078125
		_ReflectColor("Reflection Color", Color) = (1,1,1,0.5)
		_Cube("Reflection Cubemap", Cube) = "black" { TexGen CubeReflect }*/


	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 300
		Pass{
		SetTexture[_MainTex]
		SetTexture[_Texture2]{
		ConstantColor(0,0,0,[_Blend])
		Combine texture Lerp(constant) previous
	}
	}

		CGPROGRAM
#pragma surface surf Lambert

	/*samplerCUBE _Cube;

	fixed4 _ReflectColor;
	half _Shininess;
*/


	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;
	sampler2D _Texture2;
	float _Blend;

	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
		float2 uv_Texture2;
		//float3 worldRefl;

	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 t1 = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		fixed4 t2 = tex2D(_Texture2, IN.uv_MainTex) * _Color;

		o.Albedo = lerp(t1, t2, _Blend);
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));


		/*//o.Albedo = 0;
		o.Gloss = 1;
		o.Specular = _Shininess;

		fixed4 reflcol = texCUBE(_Cube, IN.worldRefl);
		o.Emission = reflcol.rgb * _ReflectColor.rgb;
		o.Alpha = reflcol.a * _ReflectColor.a;*/


		//o.Gloss = texS.g *_Gloss* pow(rim, _RimPower);
		////alpha
		//o.Alpha = tex.a; //* _Color.a;
		//				 //specular
		//o.Specular = texS.r *_Spec;
		//Cubemap
		/*float3 worldRefl = WorldReflectionVector(IN, o.Normal);
		fixed4 reflcol = texCUBE(_Cube, worldRefl);
		reflcol *= texS.b * pow(rim, _RimPower);
		o.Emission = reflcol.rgb * _ReflectColor.rgb;*/


	}
	ENDCG
	}

		FallBack "Diffuse"
}
