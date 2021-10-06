Shader "Unlit/LightPulseSingleColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Colour ("Colour", Color) = (1,1,1,1)
        _BrightnessAmplitude ("Brightness Amplitude", float) = 1.0
        _BrightnessFrequency ("Brightness Frequency", float) = 1.0
        _AlphaAmplitude ("Alpha Amplitude", float) = 1.0
        _AlphaFrequency ("Alpha Frequency", float) = 1.0
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
            float4 _Colour;
            float _BrightnessAmplitude;
            float _BrightnessFrequency;

            float _AlphaAmplitude;
            float _AlphaFrequency;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float sin01(float value)
            {
                return 0.5f * sin(value) + 0.5f;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                col.rgb = _Colour.rgb * _BrightnessAmplitude * sin01(_Time.y * _BrightnessFrequency);

                col.a *= _AlphaAmplitude * sin01(_Time.y * _AlphaFrequency);

                return col;
            }
            ENDCG
        }
    }
}