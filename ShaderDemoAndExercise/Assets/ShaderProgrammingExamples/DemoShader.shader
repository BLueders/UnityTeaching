Shader "Demo/DemoShader"
{
	// defining the main properties as exposed in the inspector
	Properties
	{

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

				// the vertex data is comming in in local space, we need to transform it into clip space!
				// first into world space:
				vOutput.position = mul(unity_ObjectToWorld, vInput.position); // (unity_ObjectToWorld is UNITY_MATRIX_M)
				// then into view space:
				if(vOutput.position.y < 0){
					vOutput.position.y = 0;
				}
				vOutput.position = mul(UNITY_MATRIX_V, vOutput.position);
				// finally via projection into clip space! (the cube)
				vOutput.position = mul(UNITY_MATRIX_P, vOutput.position);

				// Or do all operations with the combined ModelViewProjection Matrix!:
				// vOutput.position = mul(UNITY_MATRIX_MVP, vInput.position);

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
