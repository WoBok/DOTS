Shader "URP Shader/HybridShader" {
    Properties {
        _BaseMap ("Albedo", 2D) = "white" { }
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags { "RenderPipeline" = "UniversalPipeline" }

        Pass {
            HLSLPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct Varyings {
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float4 positionCS : SV_POSITION;
            };
            
            sampler2D _BaseMap;
            CBUFFER_START(UnityPerMaterial)
            float4 _BaseMap_ST;
            half4 _Color;
            CBUFFER_END
            half4 _ColorDOTS;

            #if defined(DOTS_INSTANCING_ON)
                UNITY_DOTS_INSTANCING_START(MaterialPropertyMetadata)
                UNITY_DOTS_INSTANCED_PROP_OVERRIDE_SUPPORTED(float4, _ColorDOTS)
                UNITY_DOTS_INSTANCING_END(MaterialPropertyMetadata)
                #define UNITY_ACCESS_HYBRID_INSTANCED_PROP(var, type) UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(type, var)
            #else
                #define UNITY_ACCESS_HYBRID_INSTANCED_PROP(var, type) var
            #endif

            Varyings Vertex(Attributes input) {

                Varyings output;
                
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);

                output.uv = input.texcoord.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;

                return output;
            }

            half4 Fragment(Varyings input) : SV_Target {

                half4 albedo = tex2D(_BaseMap, input.uv);

                half4 diffuse = albedo * (dot(input.normalWS, normalize(_MainLightPosition.xyz)) * 0.5 + 0.5);

                #if defined(DOTS_INSTANCING_ON)
                    half4 color = UNITY_ACCESS_HYBRID_INSTANCED_PROP(_ColorDOTS, float4);
                #else
                    half4 color = _Color;
                #endif

                return diffuse * color;
            }
            ENDHLSL
        }
    }
}