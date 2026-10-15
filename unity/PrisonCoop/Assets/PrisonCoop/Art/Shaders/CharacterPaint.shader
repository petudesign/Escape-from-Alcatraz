Shader "PrisonCoop/CharacterPaint"
{
    Properties { _Color("Tint", Color) = (1,1,1,1) }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0
        struct Input { float4 paint; };
        fixed4 _Color;
        void vert(inout appdata_full v, out Input o) { UNITY_INITIALIZE_OUTPUT(Input,o); o.paint=v.color;
            #ifndef UNITY_COLORSPACE_GAMMA
            o.paint.rgb=GammaToLinearSpace(o.paint.rgb);
            #endif
        }
        void surf(Input i, inout SurfaceOutputStandard o) { o.Albedo=i.paint.rgb*_Color.rgb; o.Smoothness=0.06; o.Metallic=0; o.Alpha=1; }
        ENDCG
    }
    FallBack "Diffuse"
}
