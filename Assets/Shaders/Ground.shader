Shader "Unlit/Ground"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };
            
            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed h = (i.worldPos.y / 5);
                fixed r = 0;
                fixed g = 0;
                fixed b = 0;
                if (h > 0.75)
                {
                    r = h;
                    g = h;
                    b = h;
                }
                else if (h > 0.4)
                {
                    g = 1 - h;
                }
                else
                {
                    r = h;
                    g = h * 0.6;
                }
                return fixed4(r, g, b, 1);
            }
            ENDCG
        }
    }
}
