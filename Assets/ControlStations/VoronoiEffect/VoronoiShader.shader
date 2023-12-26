// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/VoronoiShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
uniform int _StationCount;
#define MAX_NUM_STATIONS 20
uniform float2 _Positions[MAX_NUM_STATIONS];
uniform half4 _Colors[MAX_NUM_STATIONS];

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

			v2f vert(appdata_img v)
			{
				v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

            fixed4 frag(v2f id) : COLOR{
    
    float closestDist = 1000000;
    int closestIndex = 0;
    float dist = 0;
    for (int i = 0; i < _StationCount; i++)
    {
        
        float2 screenPos = id.pos.xy / id.pos.w;
        dist = length(screenPos - _Positions[i]);
        if (dist < closestDist)
        {
            closestDist = dist;
            closestIndex = i;
        }
    }
    
    // half4 res = tex2D(_MainTex, id.uv);
    
    half4 color = _Colors[closestIndex];
    
    
    return color; // (res + color) / 2;
                    
            }
            
            ENDCG
        }
    }
}
