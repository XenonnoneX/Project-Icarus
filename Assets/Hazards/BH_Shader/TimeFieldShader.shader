// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/TimeFieldShader"
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
            uniform float2 _Direction;
            uniform float _Strength;
            uniform float _Frequency;
            uniform float _Ratio;
            uniform float _TimeSinceStart;
            uniform float _SaturationAdd;
			uniform float _SaturationSinMultiplier;

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

            // Convert RGB to HSV
            float3 RGBtoHSV(float3 color)
            {
                float maxVal = max(max(color.r, color.g), color.b);
                float minVal = min(min(color.r, color.g), color.b);
                float delta = maxVal - minVal;

                float3 hsv = float3(0, 0, maxVal);

                if (maxVal > 0)
                {
                    hsv.y = (maxVal > 0) ? delta / maxVal : 0;

                    if (delta > 0)
                    {
                        hsv.x = (color.r == maxVal) ? (color.g - color.b) / delta : ((color.g == maxVal) ? 2 + (color.b - color.r) / delta : 4 + (color.r - color.g) / delta);
                        hsv.x = (hsv.x < 0) ? hsv.x + 6 : hsv.x;
                        hsv.x = hsv.x * 60;
                    }
                }

                return hsv;
            }

            // Convert HSV to RGB
            float3 HSVtoRGB(float3 hsv)
            {
                float c = hsv.y * hsv.z;
                float h = (hsv.x / 60.0) % 6;
                float x = c * (1.0 - abs(h - 2 * floor(h / 2) - 1.0));

                float3 rgb = float3(0, 0, 0);

                if (h >= 0 && h < 1)
                    rgb = float3(c, x, 0);
                else if (h >= 1 && h < 2)
                    rgb = float3(x, c, 0);
                else if (h >= 2 && h < 3)
                    rgb = float3(0, c, x);
                else if (h >= 3 && h < 4)
                    rgb = float3(0, x, c);
                else if (h >= 4 && h < 5)
                    rgb = float3(x, 0, c);
                else if (h >= 5 && h < 6)
                    rgb = float3(c, 0, x);

                float minVal = hsv.z - c;
                rgb += minVal;

                return rgb;
            }

            fixed4 frag(v2f i) : COLOR{
                float2 dist = i.uv * _Direction;
    float2 ratio = { _Ratio, 1 };

    float sinVal = sin((dist + _TimeSinceStart) * _Frequency);

    // Adjust the scaling factor based on your preference for subtlety
    float saturationFactor = _SaturationAdd + _SaturationSinMultiplier * sin(sinVal);

    float2 offset = i.uv + sinVal * sinVal * _Direction * _Strength;

    half4 originalColor = tex2D(_MainTex, offset);

    // Convert to HSV
    float3 hsv = RGBtoHSV(originalColor.rgb);

    // Modify saturation based on sinVal
    hsv.y = saturate(hsv.y + saturationFactor); // Ensure saturation is in the valid range

    // Convert back to RGB
    half3 modifiedColor = HSVtoRGB(hsv);

    return half4(modifiedColor, originalColor.a);

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