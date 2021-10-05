Shader "Unlit/ColorWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BrightnessAmplitude ("White amplitude", float) = 1
        _BrightnessFrequency ("White frequency", float) = 1
        _RedAmplitude ("Red amplitude", float) = 1
        _RedFrequency ("Red frequency", float) = 1
        _GreenAmplitude ("Green amplitude", float) = 1
        _GreenFrequency ("Green frequency", float) = 1
        _BlueAmplitude ("Blue amplitude", float) = 1
        _BlueFrequency ("Blue frequency", float) = 1
        _AlphaAmplitude ("Alpha amplitude", float) = 1
        _AlphaFrequency ("Alpha frequency", float) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
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
            float _BrightnessAmplitude;
            float _BrightnessFrequency;
            float _RedAmplitude;
            float _RedFrequency;
            float _BlueAmplitude;
            float _BlueFrequency;
            float _GreenAmplitude;
            float _GreenFrequency;
            float _AlphaAmplitude;
            float _AlphaFrequency;            

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                col.r = _RedAmplitude * sin(_Time.y * _RedFrequency);
                col.g = _GreenAmplitude * sin(_Time.y * _GreenFrequency);
                col.b = _BlueAmplitude * sin(_Time.y * _BlueFrequency);

                col.rgb = col.rgb * _BrightnessAmplitude * sin(_Time.y * _BrightnessFrequency);

                col.a = _AlphaAmplitude * sin(_Time.y * _AlphaFrequency);

                return col;
            }
            ENDCG
        }
    }
}