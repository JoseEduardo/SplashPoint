Shader "Custom/SSDecal" {
 
    Properties {
 
        _MainTex ("Base (RGB)", 2D) = "white" {}
 
    }
 
    SubShader {
 
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1"}
 
        ColorMask 0
 
        ZWrite off
 
        Stencil {
 
            Ref 1
 
            Comp always
 
            Pass replace
 
        }
 
 
 
        CGINCLUDE
 
            #include "UnityCG.cginc"
 
           
 
            sampler2D _MainTex;
 
           
 
            struct appdata {
 
                float4 vertex : POSITION;
 
            };
 
            struct v2f {
 
                float4 pos : SV_POSITION;
 
                float2 uv : TEXCOORD0;
 
            };
 
            v2f vert(appdata v) {
 
                v2f o;
 
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
 
                float4 projPos = mul(UNITY_MATRIX_MVP, v.vertex);
 
                float4 posWorld = mul(_Object2World, v.vertex);
 
                float4 posObj = mul(_World2Object, posWorld);
 
               
 
                posObj += 0.5;
 
                o.uv = posObj.xz;
 
               
 
                return o;
 
            }
 
            half4 frag(v2f i) : COLOR {
 
                fixed4 c = tex2D(_MainTex, i.uv);
 
                return c;
 
            }
 
        ENDCG
 
 
 
        Pass {
 
            Cull Front
 
            ZTest Less
 
        }
 
        Pass {
 
            Cull Back
 
            ZTest Greater
 
        }
 
       
 
        Pass {
 
            Tags { "RenderType"="Opaque" "Queue"="Geometry+2"}
 
 
 
            ColorMask RGB
 
            Cull Front
 
            ZTest Always
 
            Stencil {
 
                Ref 1
 
                Comp notequal
 
            }
 
 
 
            CGPROGRAM
 
            #pragma vertex vert
 
            #pragma fragment frag
 
            ENDCG
 
        }
 
    }
 
}