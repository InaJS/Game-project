Shader "Unlit/ColorWaveDanceTiles"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TimeOffset ("Time Offset", float) = 1

        _RedAmplitude ("Red amplitude", float) = 1
        _RedFrequency ("Red frequency", float) = 1
        _GreenAmplitude ("Green amplitude", float) = 1
        _GreenFrequency ("Green frequency", float) = 1
        _BlueAmplitude ("Blue amplitude", float) = 1
        _BlueFrequency ("Blue frequency", float) = 1

        _BrightnessAmplitude ("White amplitude", float) = 1
        _BrightnessFrequency ("White frequency", float) = 1
        _BrightnessDuration ("White duration", float) = 1
        _MinBrightness ("Minimum White", float) = 0.1

        _AlphaAmplitude ("Alpha amplitude", float) = 1
        _AlphaFrequency ("Alpha frequency", float) = 1
        _AlphaDuration ("Alpha duration", float) = 1
        _MinAlpha ("Minimum Alpha", float) = 0.1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _TimeOffset;

            float _RedAmplitude;
            float _RedFrequency;
            float _BlueAmplitude;
            float _BlueFrequency;
            float _GreenAmplitude;
            float _GreenFrequency;

            float _BrightnessAmplitude;
            float _BrightnessFrequency;
            float _BrightnessDuration;
            float _MinBrightness;

            float _AlphaAmplitude;
            float _AlphaFrequency;
            float _AlphaDuration;
            float _MinAlpha;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float fracWave(float value, float timeBetweenPeaks, float timeBiggerThanZero)
            {
                float w = timeBiggerThanZero;

                float alpha = - timeBetweenPeaks / w + 1 + w;

                float result = frac(saturate(alpha - w + (value / w)));
                return result;
            }

            float sin01(float value)
            {
                return 0.5f * sin(value) + 0.5f;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // The idea is to use a BW mask and recolor it within the shader using simple waves

                col.r *= _RedAmplitude * sin01(_TimeOffset + _Time.y * UNITY_TWO_PI *_RedFrequency);
                col.g *= _GreenAmplitude * sin01(_TimeOffset + _Time.y * UNITY_TWO_PI * _GreenFrequency);
                col.b *= _BlueAmplitude * sin01(_TimeOffset + _Time.y * UNITY_TWO_PI * _BlueFrequency);

                col.rgb *= _BrightnessAmplitude * fracWave(_TimeOffset + _Time.y, 1/_BrightnessFrequency,_BrightnessDuration);
                // col.rgb *= _BrightnessAmplitude * sin01(_TimeOffset + _Time.y * UNITY_TWO_PI * _BrightnessFrequency) + _MinBrightness;

                col.a *= _AlphaAmplitude * fracWave(_TimeOffset + _Time.y, 1/_AlphaFrequency,_AlphaDuration) + _MinAlpha;
                // col.a *= _AlphaAmplitude * sin01(_TimeOffset + _Time.y * UNITY_TWO_PI  * _AlphaFrequency) + _MinAlpha;

                // https://www.desmos.com/calculator/n1stbulhpe
                
                return col;
            }
            ENDCG
        }
    }
}