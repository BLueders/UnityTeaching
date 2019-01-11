Shader "Demo/DemoShader_WithLighting"
{
	// defining the main properties as exposed in the inspector
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Main Color", Color) = (1,1,1,0.5)
	}
	// start first subshader (there is only one, but there could be multible)
	SubShader
	{
		Tags { "RenderType"="Opaque" } // other render types are "Transparent" or "Geometry", defines when stuff gets rendered. 
		LOD 100 // There could be mutlible subshaders with other LOD, then the shader gets selected on the current LOD (distance to camera)

		ZTest ON // default: ON, write into the depth buffer

		// Blend SrcAlpha OneMinusSrcAlpha // This is for alpha blending, but only if the shader should be transparent

		Pass // A shader can have multible passes. One pass = one time render everything.
		{
			CGPROGRAM // start a section of CG code
			#pragma vertex vertexShader // define the vertex shader function
			#pragma fragment fragmentShader // define the pixel shader function
			
			#include "UnityCG.cginc" //has many helpful functions
			#include "Lighting.cginc" //includes lighting variables and functions

			//******* everything above here is just setup, you can ignore that ********

			// struct defining the Input for the VertexShader
			struct vertexInput
			{
				float4 position : POSITION; // The : POSITION means what semantic to put this variable in. (what it is intendet for)
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			// struct defining the Output of the VertexShader and Input for the FragmentShader
			struct vertexOutput
			{
				float2 uv : TEXCOORD0;
				float4 position : SV_POSITION;
				float3 normal : NORMAL;
			};

			// uniform (static) input variables, these are constant for each pixel and vertex
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float4 _Color;

			// small helping function that can calculate lighting for one directional light in the scene
			// lighting is calculated in world space
			fixed4 calculateLighting(vertexOutput i){
				fixed3 lightDirection = normalize(_WorldSpaceLightPos0.xyz); // _WorldSpaceLightPos0 comes from the Unity graphics engine
				fixed3 diffuseLight = _LightColor0.rgb * saturate(dot(i.normal, lightDirection)); // saturate ensures the range of 0-1 (like clamp)
				fixed3 ambLight = UNITY_LIGHTMODEL_AMBIENT.rgb; // UNITY_LIGHTMODEL_AMBIENT comes from the Unity graphics engine
				fixed4 finalLight = float4(ambLight + diffuseLight, 1.0);
				return finalLight;
			}

			vertexOutput vertexShader (vertexInput vInput)
			{
				vertexOutput vOutput;

				// the vertex data is comming in in local space, we need to transform it into clip space!
				// first into world space:
				vOutput.position = mul(unity_ObjectToWorld, vInput.position); // (unity_ObjectToWorld is UNITY_MATRIX_M)
				// then into view space:
				vOutput.position = mul(UNITY_MATRIX_V, vOutput.position);
				// finally via projection into clip space! (the cube)
				vOutput.position = mul(UNITY_MATRIX_P, vOutput.position);

				// Or do all operations with the combined ModelViewProjection Matrix!:
				// vOutput.position = mul(UNITY_MATRIX_MVP, vInput.position);

				// The last two operations handle uv and normal data, so it is given correctly to the pixel shader
				// apply texture scale and offset (tiling) (This uses MainTex_ST)
				vOutput.uv = TRANSFORM_TEX(vInput.uv, _MainTex);
				// transform the normal to world space so we can use it to calculate lighting
				vOutput.normal = mul(unity_ObjectToWorld, vInput.normal);
				return vOutput;
			}
			
			fixed4 fragmentShader (vertexOutput vOutput) : SV_Target
			{
				// sample the texture, this means get the pixel on the texture according to its uv coordinates
				fixed4 col = tex2D(_MainTex, vOutput.uv);
				// apply main color
				col *= _Color;
				col *= calculateLighting(vOutput);
				return col;
			}

			ENDCG
		}
	}
}
