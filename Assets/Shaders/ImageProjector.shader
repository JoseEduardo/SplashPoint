Shader "Custom/ImageProjector" {
   Properties {
	  _ShadowTex ("Gamma Image", 2D) = "white" {}
	  _Far ("Far Range", Float) = 3000.0
	  _Near ("Near Range", Float) = 50.0
	  _ARange ("Angular Range", Float) = 150.0
   }
   SubShader {
	  Pass {      
		 Blend SrcAlpha OneMinusSrcAlpha 
		 
		 CGPROGRAM
 
		 #pragma target 3.0
		 #pragma vertex vert  
		 #pragma fragment frag
		 #pragma glsl
		 
		 #define TEX2D(sampler,uvs)  tex2Dlod( sampler , float4( ( uvs ) , 0.0f , 0.0f) ) 
 
		 // User-specified properties
		 uniform sampler2D _ShadowTex; 
		 uniform float _Far;
		 uniform float _Near;
		 uniform float _ARange;
 
		 // Projector-specific uniforms
		 uniform float4x4 _Projector; // transformation matrix from object space to projector space 

		 //uniform float4x4 _CustomProjector = float4

		 struct vertexInput {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		 };
		 struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 posProj : TEXCOORD0; // position in projector space
		 };
 
		 vertexOutput vert(vertexInput input) 
		 {
			vertexOutput output;
 
			output.posProj = mul(_Projector, input.vertex);
			output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
			return output;
		 }
 
		 float4 frag(vertexOutput input) : COLOR 
		 {
			if (input.posProj.w > 0.0) // in front of projector?
			{
				float x = (input.posProj.x / input.posProj.w) - 0.5;
				float y = (input.posProj.y / input.posProj.w) - 0.5;
				float z = (0.5 + (input.posProj.z / (input.posProj.w * 2.0)));
				
				float h = sqrt(x*x + z*z);
				float yp = ((_Far+_Near)*sqrt(h*h + y*y)-_Near)/_Far; 
				
				float angle = atan2(z,x);
				float factor = (180.0 - _ARange) / (2.0 * _ARange);
				float xp = -factor +  (1.0 - (angle / radians(180.0))) * (1.0 + factor*2.0);                    	 
						  
				if( xp >= 0.0 && xp <= 1.0 && yp >= 0.0 && yp <= 1.0)
				{
					float4 radarColor = TEX2D(_ShadowTex , float2(xp,yp));
					return radarColor;
				}
				else
					return float4(1.0, 1.0, 0.0, 0.0);
			}
			else // behind projector
			{
			   return float4(1.0, 0.0, 0.0, 0.0);
			}
		 }
 
		 ENDCG
		 
	  }
   }  
}