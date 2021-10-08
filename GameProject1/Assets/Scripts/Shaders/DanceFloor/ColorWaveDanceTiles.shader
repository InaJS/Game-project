Shader "Unlit/ColorWaveDanceTiles"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TimeOffset ("Time Offset", float) = 1

        _Color("Color", Color) = (1,1,0,1) 
        
        _LoadingTimeOffset ("Load Offset", float) = 1
        _FlashStrength ("Flash Strength", float) = 1
        _DelayBetweenFlashes ("Delay Time", float) = 1
        _FlashDuration ("Flash Duration", float) = 1
        
        _MinColorValue ("Minimum color", float) = 0.1
        _MinAlphaValue ("Minimum alpha", float) = 0.1
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

            float4 _Color;
            
            float _LoadingTimeOffset;
            float _FlashStrength;
            float _DelayBetweenFlashes;
            float _FlashDuration;
            float _MinColorValue;
            float _MinAlphaValue;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            float spikeWave(float time, float timeBiggerThanZero)
            {
                float w = timeBiggerThanZero;

                float alpha = - 1.0f / w + 1.0f + w;

                float result = alpha - w + (time / w);

                return max(0,result);
            }

            float sin01(float value)
            {
                return 0.5f * sin(value) + 0.5f;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // The idea is to use a BW mask and recolor it within the shader using simple waves

                float time = frac((_Time.y - _TimeOffset - _LoadingTimeOffset)/_DelayBetweenFlashes);
                float flashDuration = _FlashDuration/_DelayBetweenFlashes;

                col.rgb *= _Color.xyz * _FlashStrength * spikeWave(time, flashDuration) + _MinColorValue;
                col.a *= _FlashStrength * spikeWave(time, flashDuration) + _MinAlphaValue;

                // https://www.desmos.com/calculator/n1stbulhpe
                
                return col;
            }
            ENDCG
        }
    }
}