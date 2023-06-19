Shader "Custom/ExcludeFromFog" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
        _EmissionColor ("Emission Color", Color) = (0, 0, 0, 1)
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows exclude_path:prepass
        #pragma target 3.5
        
        #include "UnityCG.cginc"
        
        struct Input {
            float2 uv_MainTex;
        };
        
        fixed4 _Color;
        float _Metallic;
        float _Smoothness;
        fixed4 _EmissionColor;
        
        void surf (Input IN, inout SurfaceOutputStandard o) {
            o.Albedo = _Color.rgb;
            
            o.Normal = float3(0, 0, 1); // Flat surface normal
            
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            
            o.Occlusion = 1; // Maximum occlusion
            
            o.Emission = _EmissionColor.rgb;
        }
        ENDCG
    }
}