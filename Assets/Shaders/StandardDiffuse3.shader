shader "CookbookShaders/StandardDiffuse3"{
    Properties {
        _MainTex("2D",2D) = "white"{}
        _Color("Color",Color) = (1,1,1,1)
        _AmbientColor("Ambient Color",Color) = (1,1,1,1)
        _MySliderValue("This is Slider",Range(0,10)) = 2
    }
    SubShader {
        Tags {"RenderType" = "Opaque"}

        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        float4 _AmbientColor;
        float _MySliderValue;
        sampler2D _MainTex;

        void surf(Input IN,inout SurfaceOutputStandard o){
            fixed4 c1 = tex2D(_MainTex,IN.uv_MainTex) ;//* _Color;
            fixed4 c = pow((c1 + _AmbientColor),
                _MySliderValue);
            
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
}