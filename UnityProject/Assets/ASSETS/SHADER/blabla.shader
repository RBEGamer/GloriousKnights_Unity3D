Shader "Custom/Geometry_Shader_Test"
 {
     Properties 
     {
         _MainTex ("Base (RGBA)",2D) ="white"{}
         _Explode ("Outline width", Range (0.001, 0.03)) = 0.01
     }
 
     SubShader 
     {
         Pass                ///////////////////////PASS 1
         {
             Tags { "RenderType"="Opaque" }
             LOD 200
             Cull Off
               
             CGPROGRAM
                 #pragma target 4.0
                 #pragma vertex VS_Main
                 #pragma fragment FS_Main
                 #include "UnityCG.cginc" 
 
                 // **************************************************************
                 // Data structures                                                *
                 // **************************************************************
                struct appdata { 
     
             float4 vertex : POSITION; 
             float3 normal : NORMAL;
             float2 texcoord : TEXCOORD0;
         }; 
                 
                 struct fs_input          // also geometry shader input
                 {
                     float4    pos        : POSITION;
                     float2  tex      : TEXCOORD0;
                     float2 texcoord :TEXCOORD0;
                 };
 
 
 
                 // **************************************************************
                 // Vars                                                            *
                 // **************************************************************
                 sampler2D _MainTex;
                 float _Explode;
                 
                 
                 
                 // **************************************************************
                 // Shader Programs                                                *
                 // **************************************************************
 
                 // Vertex Shader ------------------------------------------------
                 fs_input VS_Main(appdata v)
                 {
                     fs_input output;
                     output.pos = mul(UNITY_MATRIX_MVP,v.vertex);
                     output.tex = v.texcoord;
 						output.texcoord = v.texcoord;
                     return output;
                 }
 
 
                 // Fragment Shader -----------------------------------------------
                 float4 FS_Main(fs_input input) : COLOR
                 {
                 	fixed4 col = tex2D(_MainTex, input.texcoord );
                 	float t = 00.5;	
                 	float4 fc = lerp(float4(0,0,0,1), col, t);
                     return float4(0,0,0,1);
                 }
 
             ENDCG
         }
         
         
                 Pass                  ///////////////////////// PASS 2
         { 
             Tags { "RenderType"="Opaque" }
             LOD 200
             Cull Off
               
             CGPROGRAM
                 #pragma target 4.0
                 #pragma vertex VS_Main
                 #pragma fragment FS_Main
                 #pragma geometry GS_Main
                 #include "UnityCG.cginc" 
 
                 // **************************************************************
                 // Data structures                                                *
                 // **************************************************************
                 
                  float _Explode;
                  
                      struct appdata { 
     
             float4 vertex : POSITION; 
             float3 normal : NORMAL;
             float2 texcoord : TEXCOORD0;
         }; 


                 struct vertexOutput          // also geometry shader input
                 {
                     float4    pos        : POSITION;
                     float3 norm			: NORMAL;
                     float2 texcoord : TEXCOORD0;
                 };
 
                 struct gs_output
                 {
                     float4    pos        : POSITION;
                     float2 texcoord : TEXCOORD0;
                    
                 };
 
                 // **************************************************************
                 // Shader Programs                                                *
                 // **************************************************************
 
                 // Vertex Shader ------------------------------------------------
                 vertexOutput VS_Main(appdata v)
                 {
                     vertexOutput output;
                     output.pos=  mul(_Object2World, v.vertex);
                     output.norm = mul (_Object2World, v.normal);
                     output.texcoord = v.texcoord;
                     return output;
                 }
                 // Geometry Shader -----------------------------------------------------
                 [maxvertexcount(12)]
                 void GS_Main(triangle vertexOutput p[3], inout TriangleStream<gs_output> triStream)
                 {
                       
                         gs_output pIn;
                         
               
                          	float3 faceEdgeA = p[1].pos - p[0].pos;
    						float3 faceEdgeB = p[2].pos - p[0].pos;
   						 	float3 faceNormal = normalize( cross(faceEdgeA, faceEdgeB) );
  						  	float3 ExplodeAmt = faceNormal*_Explode;
             
                         float4 v[3];   // the triangle to be generated
                         v[0] = float4(p[0].pos.xyz+ExplodeAmt, 1.0f);
                         v[1] = float4(p[1].pos.xyz+ExplodeAmt, 1.0f);
                         v[2] = float4(p[2].pos.xyz+ExplodeAmt, 1.0f);
                         
                      
    
                  
                         pIn.texcoord = p[0].texcoord;
                         pIn.pos.w = 1.0f;
                         pIn.pos.xyz = mul(_World2Object,v[0]).xyz;
                         pIn.pos = mul(UNITY_MATRIX_MVP,pIn.pos);
                         triStream.Append(pIn);
                         
                         pIn.texcoord = p[1].texcoord;
                         pIn.pos.w = 1.0f;
                         pIn.pos.xyz = mul(_World2Object,v[1]).xyz;
                         pIn.pos = mul(UNITY_MATRIX_MVP,pIn.pos);
                         triStream.Append(pIn);
                         
                         pIn.texcoord = p[2].texcoord;   
                         pIn.pos.w = 1.0f;
                         pIn.pos.xyz = mul(_World2Object,v[2]).xyz;
                         pIn.pos = mul(UNITY_MATRIX_MVP,pIn.pos);
                         triStream.Append(pIn);
 
                 }
                 
                 // Fragment Shader -----------------------------------------------
                 
                 uniform sampler2D _MainTex;
   
                 float4 FS_Main(gs_output input) : COLOR
                 {
                     fixed4 col = tex2D(_MainTex, input.texcoord ) ;	
                     return col;
                 }
 
             ENDCG
         }
     } 
 }
