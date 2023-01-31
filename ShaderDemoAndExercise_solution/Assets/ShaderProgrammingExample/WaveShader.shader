// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Exercises/WaveShader"
{
	// defining the main properties as exposed in the inspector
	Properties
	{
		_Multiplier ("Multiplier", float) = 4
	}
	// start first subshader (there is only one, but there could be multible)
	SubShader
	{
		Tags { "RenderType"="Opaque" } // other render types are "Transparent" or "Geometry", defines when stuff gets rendered. 

		Pass // A shader can have multible passes. One pass = one time render everything.
		{
			CGPROGRAM // start a section of CG code

			#pragma vertex vertexShader // define the vertex shader function
			#pragma fragment fragmentShader // define the pixel shader function
			
			#include "UnityCG.cginc" //has many helpful functions
			
			uniform float _Multiplier;

			//******* everything above here is just setup, you can ignore that ********

			// struct defining the Input for the VertexShader
			struct vertexInput
			{
				float4 position : POSITION; // The : POSITION means what semantic to put this variable in. (what it is intendet for)
			};

			// struct defining the Output of the VertexShader and Input for the FragmentShader
			struct vertexOutput
			{
				float4 position : SV_POSITION;
			};

			vertexOutput vertexShader (vertexInput vInput)
			{
				vertexOutput vOutput;

				// Do wave transformation in local space of the object, use x axis for phase offset of sin curve.
				vOutput.position = vInput.position;

				float4 pos = vInput.position;

				// the vertex data is comming in in local space, we need to transform it into clip space!
				// first into world space:
				pos.y += sin(_Time[1] * _Multiplier/2 + pos.x * _Multiplier);
				pos = mul(unity_ObjectToWorld, pos); // (unity_ObjectToWorld is UNITY_MATRIX_M)

				// then into view space:

				pos = mul(UNITY_MATRIX_V, pos);

				// finally via projection into clip space! (the cube)

				pos = mul(UNITY_MATRIX_P, pos);

				// Or do all operations with the combined ModelViewProjection Matrix!:
			    pos = UnityObjectToClipPos(vInput.position);

				vOutput.position = pos;
				return vOutput;
			}
			
			fixed4 fragmentShader (vertexOutput vOutput) : SV_Target
			{
				// just get a solid color for the shader.
				fixed4 col = fixed4(0.8,0.8,1,1); // light blue
				return col;
			}

			ENDCG
		}
	}
}
