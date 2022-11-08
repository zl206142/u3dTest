// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/UI/2D-FanPai"
{
    Properties
    {
        _FontTex ("Sprite Texture", 2D) = "white" {}
        _BackTex ("Sprite Texture", 2D) = "white" {}
        _X("x", Float) = 0
        _Y("y", Float) = 0
        _R("r", Float) = 0.1
        _MoveX("mx", FLoat) = 0
        _MoveY("my", FLoat) = 0
        _Color ("Tint", Color) = (1,1,1,1)

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil {}

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4 mask : TEXCOORD2;
                float2 texcoord1 : TEXCOORD3;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _FontTex;
            sampler2D _BackTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _FontTex_ST;
            float _X;
            float _Y;
            float _R;
            float _MoveX;
            float _MoveY;

            float2 rotate(float2 p, float2 c, float a)
            {
                return mul(float2x2(cos(a), -sin(a), sin(a), cos(a)), p - c) + c;
            }

            float2 dv(float2 texcoord)
            {
                if (_X == 0 && _Y == 0)
                {
                    texcoord = texcoord + float2(1, 0);
                }
                else if (_X >= 0 && _Y >= 0)
                {
                    texcoord = rotate(texcoord + float2(1, 0), float2(1, 0), -2 * atan2(_Y, _X));
                }
                else if (_X <= 0 && _Y >= 0)
                {
                    texcoord = rotate(texcoord + float2(-1, 0), float2(0, 0), -2 * atan2(_Y, _X));
                }
                else if (_X >= 0 && _Y <= 0)
                {
                    texcoord = rotate(texcoord + float2(1, 0), float2(1, 1), -2 * atan2(_Y, _X));
                }
                else if (_X <= 0 && _Y <= 0)
                {
                    texcoord = rotate(texcoord + float2(-1, 0), float2(0, 1), -2 * atan2(_Y, _X));
                }
                return texcoord;
            }

            bool is_in(float2 uv)
            {
                return uv.x > 0 && uv.y > 0 && uv.x < 1 && uv.y < 1;
            }

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                v.vertex *= 4;
                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                OUT.texcoord = TRANSFORM_TEX(v.texcoord.xy, _FontTex);

                OUT.color = v.color * _Color;
                OUT.texcoord = (OUT.texcoord - 0.5) * 4 + 0.5;
                OUT.texcoord += float2(_MoveX,_MoveY);
                OUT.texcoord1 = dv(OUT.texcoord);
                float2 dd = dv(float2(0.5, 0.5));
                float2 ddd = float2(0.5, 0.5) - dd;
                OUT.texcoord1 += normalize(ddd) * distance(float2(_X, _Y), float2(0, 0));
                return OUT;
            }

            float2 cuv(float2 rp, float bz)
            {
                float2 add = float2(0, 0);
                float qc = UNITY_HALF_PI * _R;
                float d = qc - abs(rp.x - bz);
                if (d > 0)
                {
                    if (d < _R)
                    {
                        float a = asin(d / _R);
                        float dd = qc * a / UNITY_HALF_PI;
                        float ddd = dd - d;
                        if (rp.x - bz > 0)
                        {
                            add.x = -ddd;
                        }
                        else
                        {
                            add.x = ddd;
                        }
                        if (_X != 0 || _Y != 0)
                        {
                            return rotate(add, float2(0, 0), atan2(_Y, _X));
                        }
                    }
                    else
                    {
                        add.x = 16;
                    }
                }
                return add;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = float4(0, 0, 0, 0);
                float2 dd = dv(float2(0.5, 0.5));
                dd -= float2(0.5, 0.5);
                dd.x = -dd.x;
                dd += float2(0.5, 0.5);
                float2 ddr = dd;
                if (_X != 0 || _Y != 0)
                {
                    ddr = rotate(dd, float2(0.5, 0.5), -atan2(_Y, _X));
                }
                float2 dvv = normalize(ddr - float2(0.5, 0.5)) * distance(float2(_X, _Y), float2(0, 0));
                float bz = ((ddr - float2(0.5, 0.5)) / 2 + float2(0.5, 0.5) - dvv / 2).x;
                float2 rp = IN.texcoord;
                if (_X != 0 || _Y != 0)
                {
                    rp = rotate(IN.texcoord, float2(0.5, 0.5), -atan2(_Y, _X));
                }
                if (bz < rp.x)
                {
                    float2 cuvv = cuv(rp, bz);
                    if (cuvv.x != 16)
                    {
                        IN.texcoord1 -= cuvv * float2(-1, 1);
                        if (is_in(IN.texcoord1))
                        {
                            color = tex2D(_FontTex, IN.texcoord1);
                        }
                        IN.texcoord += cuvv;
                        if (color.a < 0.1 && is_in(IN.texcoord))
                        {
                            color = tex2D(_BackTex, IN.texcoord);
                        }
                    }
                }
                color = IN.color * (color + _TextureSampleAdd);

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                color.rgb *= color.a;

                return color;
            }
            ENDCG
        }
    }
}