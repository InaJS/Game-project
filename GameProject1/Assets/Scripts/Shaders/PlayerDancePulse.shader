Shader "Unlit/PlayerDancePulse"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (0.1,0.1,0.1,0)
        _Frequency ("Frequency", float ) = 1
        _Rings ("Rings", float ) = 1
        _CenterTransparency ("Transparency", float ) = 1
        _Radius ("Radius", float ) = 1
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
            float4 _Color1;
            float4 _Color2;
            float _Frequency;
            float _Rings;
            float _CenterTransparency;
            float _Radius;

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
                float dist = length(i.uv - float2(0.5, 0.5));

                float wave = sin01(dist* _Rings - _Time.y * _Frequency);

                float4 result = lerp(_Color1,_Color2,wave);
                result.a = _CenterTransparency * saturate(_Radius- dist);
                
                return float4(result);
            }
            ENDCG
        }
    }
}