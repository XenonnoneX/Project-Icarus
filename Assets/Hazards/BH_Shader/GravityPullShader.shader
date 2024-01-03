// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/GravityPullShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
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
            uniform float2 _Position;
            uniform float _Rad;
            uniform float _Ratio;
            uniform float _Distance;
            uniform float _TimeSinceStart;

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

            fixed4 frag(v2f i) : COLOR{
                float2 offset = i.uv - _Position;
                float2 ratio = { _Ratio, 1 };
                float rad = length(offset / ratio);

				float normDist = rad * _Distance / _Rad;
				float normedFinalDist = normDist + (1 - normDist) * (1 - 1 / (1 + _TimeSinceStart));
                
                float deformation = normedFinalDist / normDist;

                offset = offset * deformation;
                
                offset += _Position;

                half4 res = tex2D(_MainTex, i.uv);

                if (rad * _Distance < _Rad) {
                    res = tex2D(_MainTex, offset);
                }

                return res;

            }

            ENDCG
        }
    }
}






/*
Shader "Hidden/GravityPullShader"
{


    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
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
            uniform float2 _Positions[5]; // Array of positions
            uniform float _Rad;
            uniform float _Ratio;
            uniform float _Distances[5];
            uniform float _TimesSinceStart[5];

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

            fixed4 frag(v2f i) : COLOR{
                float4 color = tex2D(_MainTex, i.uv); // Initialize with default color

                float2 totalOffset = float2(0,0);
                int activeEffectCount = 5;

                for (int j = 0; j < 5; ++j)
                {
                    if (_Distances[j] == -1)
                    {
                        activeEffectCount -= 1;
                        continue;
                    }

                    float2 offset = i.uv - _Positions[j];
                    float2 ratio = { _Ratio, 1 };
                    float rad = length(offset / ratio);

                    if (rad * _Distances[j] < _Rad) {
                        activeEffectCount -= 1;
                        continue;
                    }

                    float normDist = rad * _Distances[j] / _Rad;
                    float normedFinalDist = normDist + (1 - normDist) * (1 - 1 / (1 + _TimesSinceStart[j]));

                    float deformation = normedFinalDist / normDist;

                    offset = offset * deformation;

                    offset += _Positions[j];

                    totalOffset += offset;
                }

                totalOffset /= activeEffectCount; // maybe just remove this (its just gravity, add up the forces)

                color = tex2D(_MainTex, totalOffset);

                return color;

            }

            ENDCG
        }
    }
}







*/