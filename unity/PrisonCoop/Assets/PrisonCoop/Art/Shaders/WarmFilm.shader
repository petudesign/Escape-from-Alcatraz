Shader "PrisonCoop/WarmFilm"
{
    Properties { _MainTex ("Image", 2D) = "white" {} }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            fixed4 frag(v2f_img i) : SV_Target
            {
                float3 c = tex2D(_MainTex, i.uv).rgb;
                float3 glow = 0;
                for (int x=-1; x<=1; x++) for (int y=-1; y<=1; y++)
                    glow += max(tex2D(_MainTex, i.uv + float2(x,y)*_MainTex_TexelSize.xy*3).rgb - 0.82, 0);
                c += glow * 0.012;
                float grey = dot(c, float3(0.2126,0.7152,0.0722));
                c = lerp(grey.xxx, c, 0.94);
                c = (c - 0.5) * 1.035 + 0.5;
                float2 edge = (i.uv - 0.5) * 2;
                c *= 1 - dot(edge,edge) * 0.035;
                return float4(c,1);
            }
            ENDCG
        }
    }
}
