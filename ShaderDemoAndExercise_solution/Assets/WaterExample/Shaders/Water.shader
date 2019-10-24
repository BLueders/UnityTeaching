// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Water" {

	Properties {
		_Direction("Wind Direction", Vector) = (1.0, 0.0, 1.0, 0.0)
		_Speed("Speed", Range(0.1, 5)) = 0.5
		_Amplitude("Amplitude", Range(0.0, 5.0)) = 0.5
		_Frequency("Frequency", Range(0.0, 3.0)) = 1.0
		_Smoothing("Smoothing", Range(0.0, 1.0)) = 0.5

		_ColorRamp("ColorRamp", 2D) = "white" {}
		_NNoise1("Normal Noise 1", 2D) = "white" {}
		_NNoise2("Normal Noise 2", 2D) = "white" {}
		_FoamTex("Foam Texture", 2D) = "white" {}
		_FoamFactor("Foam Factor", Range(0.0, 2.0)) = 1.0
		_FoamColor("Foam Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_DistortFactor("Distortion Factor", Range(0.0, 10.0)) = 0.5
		_MaxDepth("Max Depth", Range(0.01, 20.0)) = 10

		_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess("Shininess", Range(0.1, 100)) = 10
		
		_ReflectionMap("Reflection Map", Cube) = "" {}
		_Reflectivity("Reflectivity", Range(0, 1)) = 0.5

	}

	CGINCLUDE

	#pragma target 3.0
	#pragma vertex vert
	#pragma fragment frag

	#include "UnityCG.cginc"
	#include "AutoLight.cginc"

	uniform half4 _Direction;
	uniform half _Speed;
	uniform fixed _Amplitude;
	uniform half _Frequency;
	uniform half _DistortFactor;
	uniform half _MaxDepth;
	uniform fixed _Smoothing;

	uniform sampler2D _ColorRamp;
	uniform sampler2D _NNoise1;
	uniform float4 _NNoise1_ST;
	uniform sampler2D _NNoise2;
	uniform float4 _NNoise2_ST;
	uniform sampler2D _FoamTex;
	uniform float4 _FoamTex_ST;

	uniform half _FoamFactor;
	uniform fixed4 _FoamColor;

	uniform sampler2D _CameraDepthTexture;
	uniform sampler2D _GrabTexture;
	uniform fixed4 _SpecColor;
	uniform half _Shininess;
	uniform samplerCUBE _ReflectionMap;
	uniform half _Reflectivity;
	uniform float4 _LightColor0;

	// depth = float depth of pixel, projPos = float4 screenPosition of pixel
	float DepthBufferDistance(float depth, float4 projPos){

        //Grab the depth value from the depth texture
        //Linear01Depth restricts this value to [0, 1]
        float depth1 = Linear01Depth (tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(projPos)).x);
        // get depth of pixel we are rendering                                          
        float depth2 = Linear01Depth(depth);
        
        // transform to world space distance _ProjectionParams.y = near plane, _ProjectionParams.z = far plane
        float worldDepth1 = _ProjectionParams.y + (_ProjectionParams.z-_ProjectionParams.y)*depth1;
        float worldDepth2 = _ProjectionParams.y + (_ProjectionParams.z-_ProjectionParams.y)*depth2;

		return worldDepth1 - worldDepth2;
	}

	float VertexDisplacementLinear(float3 vertex) {
		fixed2 dir = normalize(_Direction.xz);
		float x = (vertex.x * dir.x + vertex.z * dir.y) * _Frequency + _Time.w * _Speed;
        return sin(x) * _Amplitude;
	}

	float VertexDisplacementRipple(float3 v) {
		float x = -length(v.xz * _Frequency - _Direction.xz) + _Time.w * -_Speed;
		return sin(x) * _Amplitude;
	}

	ENDCG

	SubShader {
		Tags { 
			"Queue" = "Geometry"
			"RenderType" = "Opaque" 
		}

		GrabPass { "_GrabTexture" }

		Pass {

 			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv1 : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
				float4 posWorld : TEXCOORD3;
				float3 normalWorld : NORMAL;
                float4 projPos : TEXCOORD4; 
                float depth : TEXCOORD5;
                float3 viewDir : TEXCOORD6;
            };

            v2f vert(appdata_full v) {
                v2f o;

                // Vertex displacement
                // Move the vertex to world space
				float3 v0 = mul(unity_ObjectToWorld, v.vertex).xyz;
				
				// Create two fake neighbour vertices, so we can simulate the new slope, and generate a new normal
				// The fake neighbours are both in the XZ plane, meaning that this will only work for a surface parallel with this
				float3 v1 = v0 + float3(0.05, 0, 0); // +X
				float3 v2 = v0 + float3(0, 0, 0.05); // +Z
				
				// Now apply the vertex displacement to the original vertex, and the two fake neighbours. Since the fake vertices
				// are applied the function relative to their own coordinates, we can use them to generate the new normal, even though
				// they don't assume the position of actual vertices.
				
				v0.y += VertexDisplacementRipple(v0);
				v1.y += VertexDisplacementRipple(v1);
				v2.y += VertexDisplacementRipple(v2);
				
				// We smooth out the Y difference to avoid hard edges. This will actually result in incorrect normals, but
				// it can make the water look smoother
				// A value of 1 for smoothing will result in the original normal, and no effect
				v1.y -= (v1.y - v0.y) * _Smoothing;
				v2.y -= (v2.y - v0.y) * _Smoothing;
				
				// Take the cross product of the two vectors from the original vertex to the two fake neighbour vertices 
				// resulting in the new normal. Move the new normal back to object space, normalize it, and we're done.
				float3 vta = v2-v0;
				float3 vna = cross(vta, v1-v0);
				
				float3 vt = mul((float3x3)unity_WorldToObject, vta);
				float3 vn = mul((float3x3)unity_WorldToObject, vna);
				
				v.tangent = float4(normalize(vt), v.tangent.w);
				v.normal = normalize(vn);

				o.posWorld = float4(v0, 1.0);
				v.vertex = mul(unity_WorldToObject, o.posWorld);

				o.normalWorld = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				//o.tangentWorld = normalize(mul(_Object2World, v.tangent).xyz);
				// tangent.w is specific to Unity
				//o.binormalWorld = normalize(cross(o.normalWorld, o.tangentWorld) * v.tangent.w);

                o.pos = UnityObjectToClipPos(v.vertex);

                //Screen position of pos
                o.projPos = ComputeScreenPos(o.pos);
 				o.depth = o.pos.z/o.pos.w;

 				o.uv1 = v.texcoord * _NNoise1_ST.xy + _NNoise1_ST.zw + float2(_Time.x * _Speed, 0);
 				o.uv2 = v.texcoord * _NNoise2_ST.xy + _NNoise2_ST.zw + float2(0, _Time.x * _Speed);
 				o.uv3 = (v.texcoord * _FoamTex_ST.xy + _FoamTex_ST.zw);// + float2(_Time.x * _Speed, 0);

				//Direction from camera to vertex, neccessary for cubemap reflections
            	o.viewDir = mul(unity_ObjectToWorld, v.vertex).xyz - _WorldSpaceCameraPos.xyz;

                return o;
            }

			half4 frag (v2f i) : COLOR {
				float realDepth = DepthBufferDistance(i.depth, i.projPos);
				float adjustedDepth = clamp((realDepth) / 20, 0, _MaxDepth);

				float4 distortNormal1 = (tex2D(_NNoise1, i.uv1) - 0.5) * 2;
				float4 distortNormal2 = (tex2D(_NNoise2, i.uv2) - 0.5) * 2;
				float4 distortNormal = ((distortNormal1 + distortNormal2)  / 2) * _DistortFactor;
				
				float4 projPosDistorted = i.projPos + distortNormal * adjustedDepth;
	
				float distortedDepth = DepthBufferDistance(i.depth, projPosDistorted);

				if (distortedDepth < 0) {
					distortedDepth = DepthBufferDistance(i.depth, i.projPos);
					projPosDistorted = i.projPos;
				}

				half4 distortedColor = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(projPosDistorted));


				fixed relativeDepth = clamp(distortedDepth, 0.5, _MaxDepth);

				relativeDepth = relativeDepth / _MaxDepth;
				half oneMinusDepth = 1 - relativeDepth;

				relativeDepth = 1 - pow(oneMinusDepth, 3);
				
				fixed4 depthColor = tex2D(_ColorRamp, float2(relativeDepth * 0.95, 0));

				// foam
				fixed4 foam = tex2D(_FoamTex, i.uv3) * _FoamColor;

				foam.a = foam.a * ((_FoamFactor - realDepth) / _FoamFactor);

				foam.a = saturate(foam.a);
				half4 col = (relativeDepth * depthColor + oneMinusDepth * distortedColor) + foam * foam.a;


				// Lighting

				float3 lightDirection;
				float attenuation;
				
				if (_WorldSpaceLightPos0.w == 0.0) { // Directional light
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
					attenuation = 1;
				} else {
					float3 fragToLight = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					float distance = length(fragToLight);
					attenuation = 1.0 / pow(distance, 3);
					lightDirection = normalize(fragToLight);
				}
				
				attenuation *= LIGHT_ATTENUATION(i);
				
				float3 normal = normalize(i.normalWorld + distortNormal/2);

				// saturate() clamps values to min 0 or max 1
				// we saturate, because negative dot product indicates that we are on the wrong side of the face
				
				// Diffuse lighting
				float3 diffuseReflection = attenuation * _LightColor0.rgb * saturate(dot(normal, lightDirection));
				
				// Specular lighting
				//float3 specularReflection = diffuseReflection * _SpecColor.rgb * 
				//	pow(saturate(dot(reflect(-lightDirection, normal), i.viewDir)), _Shininess);

				// Ambient lighting
				float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb;
				
				// Final lighting
				float4 finalLighting = float4(diffuseReflection + ambientLighting, 1.0);// specularReflection + ambientLighting, 1.0);

				// Blend in relfections
				float3 reflectedDir = reflect(i.viewDir, normal);
            	col = texCUBE(_ReflectionMap, reflectedDir) * _Reflectivity + col * (1 - _Reflectivity);

				col.a = saturate(realDepth + 0.1);
				return col * finalLighting;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}