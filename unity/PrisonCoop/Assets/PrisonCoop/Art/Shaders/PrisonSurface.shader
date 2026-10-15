Shader "PrisonCoop/Institutional Surface"
{
    Properties
    {
        _Color("Paint",Color)=(1,1,1,1)
        _MainTex("Wear",2D)="white"{}
        _Glossiness("Smoothness",Range(0,1))=0.16
        _Wear("Wear strength",Range(0,1))=0.55
        _SurfaceKind("Surface: plain / wall / floor / iron / wood",Float)=0
        _Age("Years-of-use wear intensity",Range(0,1))=0.7
        _EmissionColor("Emission",Color)=(0,0,0,0)
    }
    SubShader
    {
        Tags {"RenderType"="Opaque"}
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0
        sampler2D _MainTex;
        fixed4 _Color,_EmissionColor;
        half _Glossiness,_Wear,_SurfaceKind,_Age;
        struct Input {float2 uv_MainTex;float3 worldPos;float3 worldNormal;};
        float hash(float3 p){p=frac(p*0.1031);p+=dot(p,p.yzx+33.33);return frac((p.x+p.y)*p.z);}
        float noise(float3 p)
        {
            float3 i=floor(p),f=frac(p);f=f*f*(3-2*f);
            return lerp(lerp(lerp(hash(i),hash(i+float3(1,0,0)),f.x),lerp(hash(i+float3(0,1,0)),hash(i+float3(1,1,0)),f.x),f.y),
                        lerp(lerp(hash(i+float3(0,0,1)),hash(i+float3(1,0,1)),f.x),lerp(hash(i+float3(0,1,1)),hash(i+float3(1,1,1)),f.x),f.y),f.z);
        }
        float filteredNoise(float3 p)
        {
            float footprint=max(length(ddx(p)),length(ddy(p)));
            return lerp(0.5,noise(p),1-smoothstep(0.25,0.8,footprint));
        }
        void surf(Input i,inout SurfaceOutputStandard o)
        {
            fixed3 tex=tex2D(_MainTex,i.uv_MainTex).rgb;
            float3 p=i.worldPos;
            float broad=filteredNoise(p*2.3),fine=filteredNoise(p*12);
            o.Albedo=_Color.rgb*lerp(float3(1,1,1),tex,_Wear);
            o.Smoothness=_Glossiness;o.Metallic=0;o.Alpha=1;
            if(_SurfaceKind>0.5&&_SurfaceKind<1.5)
            {
                float height=fmod(max(p.y,0),3.35);
                float base=exp(-height*6)*(0.22+0.23*broad);
                float rub=exp(-pow((height-0.85)*2.3,2))*smoothstep(0.48,0.76,broad)*0.11;
                o.Albedo*=1-_Age*(base+rub+(fine-0.5)*0.055);
            }
            if(_SurfaceKind>1.5&&_SurfaceKind<2.5)
            {
                // Many footsteps polish two loose lanes; variation stays subtle under the cell doors.
                float hall=(1-smoothstep(9,10.5,abs(p.x)))*smoothstep(0.9,1.7,p.z)*(1-smoothstep(6.4,7.2,p.z));
                float lanes=exp(-pow((p.z-2.4)*1.7,2))+exp(-pow((p.z-5.6)*1.7,2));
                float abrasion=smoothstep(0.66,0.83,filteredNoise(p*float3(7,1,2.5)));
                o.Albedo*=1+_Age*((broad-0.5)*0.11+hall*lanes*0.10-abrasion*0.045);
                o.Smoothness+=hall*lanes*0.04*_Age;
            }
            if(_SurfaceKind>2.5&&_SurfaceKind<3.5)
            {
                float chips=smoothstep(0.73,0.86,filteredNoise(p*12))*smoothstep(0.44,0.68,broad);
                o.Albedo=lerp(o.Albedo,float3(0.15,0.08,0.035),chips*_Age*0.8);
                o.Smoothness*=1-chips;
            }
            if(_SurfaceKind>3.5&&_SurfaceKind<4.5)
            {
                float grain=filteredNoise(p*float3(1.8,16,16));
                o.Albedo*=1+_Age*((grain-0.5)*0.28+(broad-0.5)*0.12);
            }
            o.Emission=_EmissionColor.rgb;
        }
        ENDCG
    }
    Fallback "Standard"
}
