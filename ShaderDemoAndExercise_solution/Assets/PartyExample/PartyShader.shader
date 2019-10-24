// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/PartyShader" {
	SubShader {
		Tags { "Queue" = "Transparent" } 
	
      Pass {
          
         Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         struct vertexInput {
            float4 vertex : POSITION;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 position_in_world_space : TEXCOORD0;
            float4 position_in_local_space : TEXCOORD1;
         };
 
         vertexOutput vert(vertexInput input) 
         {
         	float seconds = _Time.y * 10;
         	float seconds2 = _Time.y * 2.5;
         	float timeModifier1 = sin(seconds) ;
         	float timeModifier2 = sin(seconds2);
            vertexOutput output; 
            output.pos = input.vertex;
            output.position_in_local_space = input.vertex;
 			if(output.pos.y > 0) output.pos.y +=  output.pos.y * timeModifier1 * 0.2;
 			output.pos.x += pow(output.pos.y + 0.5,2) * timeModifier2 * 0.2;
            
            output.pos =  UnityObjectToClipPos(output.pos);
            output.position_in_world_space = 
               mul(unity_ObjectToWorld, input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
			 float dist = length(input.position_in_local_space);

             float4 color = float4(floor(input.position_in_world_space.x*3)/3,
             floor(input.position_in_world_space.y*3)/3,
             floor(input.position_in_world_space.z*3)/3, 1);
			 color = float4(floor((abs(input.position_in_local_space.x + _Time.y / 5) % 1)*5)/5 + (sin(_Time.y * 4.34532) + 0.2)/2 + floor(sin(_Time.y / 5) * 2) * 0.2, 
			 		        floor((abs(input.position_in_local_space.y + _Time.y / 5) % 1)*5)/5 + (sin(_Time.y * 3.6523) + 0.2)/2 + floor(sin(_Time.y / 10) * 2) * 0.2, 
			 		        floor((abs(input.position_in_local_space.z + _Time.y / 5) % 1)*5)/5 + (sin(_Time.y * 2.424) + 0.2)/2 + floor(sin(_Time.y / 20) * 2) * 0.2, 1);
             return color;
         }
 
         ENDCG  
      }
   }
}