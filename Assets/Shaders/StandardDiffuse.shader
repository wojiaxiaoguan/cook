shader "CookbookShaders/StandardDiffuse"{
    Properties {
        _Color("Color",Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)",2D) = "white" {}
        _Glossiness("Smoothness",Range(0,1)) = 0.5
        _Metallic("Metallic",Range(0,1)) = 0.0
    }

    SubShader{
        Tags {"RenderType" = "Opaque"}

        LOD 200

        CGPROGRAM

        //基于物理的标准照明模型，并允许所有光线类型的阴影
        #pragma surface surf Standard fullforwardshadows

        // 使用3.0的着色器 获得更好的光照效果
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input{
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
     
        void surf (Input IN,inout SurfaceOutputStandard o){
            fixed4 c = tex2D(_MainTex,IN.uv_MainTex) * _Color;
            // fixed4 c = _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}