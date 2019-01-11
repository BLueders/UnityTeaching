// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Terrain" {

	Properties {
		_ObjectUpVector("Object space up vector", Vector) = (0.0, 1.0, 0.0, 0.0)

		_UpperColor ("Upper Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainColor ("Middle Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_LowerColor ("Lower Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_VerticalLowerColor("Vertical Lower Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)

		_UpperBorder ("Upper Border", Range(-1.0, 1.0)) = 0.5
		_UpperLerp ("Upper Lerp Zone", Range(0.0, 0.5)) = 0.2
		_LowerBorder ("Lower Border", Range(-1.0, 1.0)) = -0.5
		_LowerLerp ("Lower Lerp Zone", Range(0.0, 0.5)) = 0.2

		_VerticalBorder ("Vertical Border", Range(-100.0, 100.0)) = 10
		_VericalLerp ("Vertical Lerp Zone", Range(0.0, 10)) = 5
		_BorderDistFreq ("Vertical Border Distortion Frequency", Range(0.0, 5.0)) = 1.0
		_BorderDistAmp ("Vertical Border Distortion Amplitude", Range(0.0, 5.0)) = 0.5

		_UpperTex("Upper texture", 2D) = "white" {}
		_MiddleTex("Middle texture", 2D) = "white" {}
		_LowerTex("Lower texture", 2D) = "white" {}
		_VerticalLowerTex("Vertical lower texture", 2D) = "white" {}

	}
	
	CGINCLUDE
 
	#include "UnityCG.cginc"
	#pragma target 3.0
	
	#pragma vertex vert
	#pragma fragment frag

	uniform fixed4 _LightColor0;

	uniform fixed4 _ObjectUpVector;
	
	uniform fixed4 _UpperColor;
	uniform fixed4 _MainColor;
	uniform fixed4 _LowerColor;
	uniform fixed4 _VerticalLowerColor;

	uniform fixed _UpperBorder;
	uniform fixed _UpperLerp;
	uniform fixed _LowerBorder;
	uniform fixed _LowerLerp;

	uniform float _VerticalBorder;
	uniform float _VericalLerp;
	uniform half _BorderDistFreq;
	uniform half _BorderDistAmp;

	uniform sampler2D _UpperTex;
	uniform float4 _UpperTex_ST;
	uniform sampler2D _MiddleTex;
	uniform float4 _MiddleTex_ST;
	uniform sampler2D _LowerTex;
	uniform float4 _LowerTex_ST;

	uniform sampler2D _VerticalLowerTex;
	uniform float4 _VerticalLowerTex_ST;


	ENDCG
 
	SubShader {
		Tags { 
			"RenderType" = "Opaque"
		}
		
		LOD 200
		
		Pass {
			Tags { 
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM
			#include "AutoLight.cginc"
			#pragma multi_compile_fwdbase

			struct v2f {
				float4 pos : SV_POSITION;
				half4 uv : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalWorld : NORMAL;
				LIGHTING_COORDS(2, 3)
			};
			
			v2f vert(appdata_full v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normalWorld = normalize(mul(float4(v.normal, 1.0), unity_WorldToObject).xyz);

				o.uv = v.texcoord;

				TRANSFER_VERTEX_TO_FRAGMENT(o);
				
				return o;
			}

			fixed4 NormalBasedTexture(float2 uv, fixed udn) {
				half halfUL = _UpperLerp / 2;
				half halfLL = _LowerLerp / 2;
				half lerpUU = _UpperBorder + halfUL;
				half lerpUL = _UpperBorder - halfUL;
				half lerpLU = _LowerBorder + halfLL;
				half lerpLL = _LowerBorder - halfLL;

				if (udn < lerpLL) {
					fixed4 tex = tex2D(_LowerTex, uv * _LowerTex_ST.xy + _LowerTex_ST.zw);
					return tex * _LowerColor;

				} else if (udn >= lerpLL && udn < lerpLU) {
					// Lerp factor in the lerp zone
					fixed lerpf = (udn - lerpLL) / (lerpLU - lerpLL);

					fixed4 lowerTex = tex2D(_LowerTex, uv * _LowerTex_ST.xy + _LowerTex_ST.zw);
					fixed4 middleTex = tex2D(_MiddleTex, uv * _MiddleTex_ST.xy + _MiddleTex_ST.zw);

					return lerp(_LowerColor, _MainColor, lerpf) * lerp(lowerTex, middleTex, lerpf);

				} else if (udn >= lerpLU && udn < lerpUL) {
					fixed4 tex = tex2D(_MiddleTex, uv * _MiddleTex_ST.xy + _MiddleTex_ST.zw);

					return tex * _MainColor;

				} else if (udn >= lerpLU && udn < lerpUU) {
					fixed lerpf = (udn - lerpUL) / (lerpUU - lerpUL);

					fixed4 middleTex = tex2D(_MiddleTex, uv * _MiddleTex_ST.xy + _MiddleTex_ST.zw);
					fixed4 upperTex = tex2D(_UpperTex, uv * _UpperTex_ST.xy + _UpperTex_ST.zw);

					return lerp(_MainColor, _UpperColor, lerpf) * lerp(middleTex, upperTex, lerpf);

				} else {
					fixed4 tex = tex2D(_UpperTex, uv * _UpperTex_ST.xy + _UpperTex_ST.zw);

					return tex * _UpperColor;
				}
			}
			
			fixed4 frag(v2f i) : COLOR {
				fixed3 lightDirection;
				fixed attenuation;
				
				if (_WorldSpaceLightPos0.w == 0.0) { // Directional light
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
					attenuation = 1;
				} else {
					float3 fragToLight = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					float distance = length(fragToLight);
					attenuation = 1.0 / distance;
					lightDirection = normalize(fragToLight);
				}
				
				attenuation *= LIGHT_ATTENUATION(i);

				fixed3 diffuseLight = attenuation * _LightColor0.rgb * saturate(dot(i.normalWorld, lightDirection));
				fixed3 ambLight = UNITY_LIGHTMODEL_AMBIENT.rgb;
				fixed4 finalLight = float4(ambLight + diffuseLight, 1.0);

				// Up vector dot face normal vector
				// 1 means horizontal face pointing up, 0 is vertical, -1 means down
				fixed udn = dot(_ObjectUpVector, i.normalWorld);

				fixed vDist = sin(i.posWorld.x * _BorderDistFreq) * sin(i.posWorld.z * _BorderDistFreq) * _BorderDistAmp;

				half halfVL = _VericalLerp / 2;
				half lerpVU = _VerticalBorder + halfVL + vDist;
				half lerpVL = _VerticalBorder - halfVL - vDist;

				fixed4 tex;

				if (i.posWorld.y > lerpVU) {
					tex = NormalBasedTexture(i.uv.xy, udn);

				} else if (i.posWorld.y < lerpVU && i.posWorld.y > lerpVL) {
					fixed lerpf = (i.posWorld.y - lerpVL) / (lerpVU - lerpVL);

					fixed4 upperTex = NormalBasedTexture(i.uv.xy, udn);
					fixed4 lowerTex = tex2D(_VerticalLowerTex, i.uv.xy * _VerticalLowerTex_ST.xy + _VerticalLowerTex_ST.zw) * _VerticalLowerColor;

					tex = lerp(lowerTex, upperTex, lerpf);
				} else {
					tex = tex2D(_VerticalLowerTex, i.uv.xy * _VerticalLowerTex_ST.xy + _VerticalLowerTex_ST.zw) * _VerticalLowerColor;
				}

				return tex * finalLight;
			}


			
			ENDCG
		}
		
		Pass {
			Tags { 
				"LightMode" = "ForwardAdd"
			}
			
			Blend One One

			CGPROGRAM

			struct v2f {
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD1;
				float3 normal : TEXCOORD0;
				float3 normalWorld : TEXCOORD2;
			};
			
			v2f vert(appdata_full v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normal = v.normal;	
				o.normalWorld = normalize(mul(float4(v.normal, 1.0), unity_WorldToObject).xyz);
								
				return o;
			}
			
			fixed4 frag(v2f i) : COLOR {
				fixed3 lightDirection;
				fixed attenuation;
				
				if (_WorldSpaceLightPos0.w == 0.0) { // Directional light
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
					attenuation = 1;
				} else {
					float3 fragToLight = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					float distance = length(fragToLight);
					attenuation = 1.0 / distance;
					lightDirection = normalize(fragToLight);
				}
				
				fixed3 diffuseLight = attenuation * _LightColor0.rgb * saturate(dot(i.normalWorld, lightDirection));
				fixed4 finalLight = float4(diffuseLight, 1.0);
				return finalLight;
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
