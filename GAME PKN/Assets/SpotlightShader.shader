Shader "Custom/2DSpotlightWithObjectVisibility"
{
    Properties
    {
        _PlayerPos ("Player Position", Vector) = (0,0,0,0)
        _SpotRadius ("Spot Radius", Float) = 5
        _Falloff ("Falloff", Float) = 2
        _DarknessColor ("Darkness Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { 
            "Queue"="Transparent+100"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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
                float3 worldPos : TEXCOORD1;
            };

            float2 _PlayerPos;
            float _SpotRadius;
            float _Falloff;
            fixed4 _DarknessColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Hitung jarak dari player
                float dist = distance(i.worldPos.xy, _PlayerPos);
                
                // Hitung intensitas spotlight
                float spot = 1 - smoothstep(0, _SpotRadius, dist);
                spot = pow(spot, _Falloff);
                
                // Warna hitam solid dengan alpha yang berubah
                fixed4 col = _DarknessColor;
                col.a = 1 - spot;
                
                return col;
            }
            ENDCG
        }
    }
}