Shader "Unlit_Outlineshader" {
  Properties {
    _MainTex ("Texture", 2D) = "white" {}
	_OutlineColor ("Outline Color", Color) = (0,0,0,1) 
	_MainColor ("MainColor", Color) = (1,1,1,1) 
    _Outline ("Outline width", Range (0.002, 0.1)) = 0.01 
    _EmitMap ("Emission Texture", 2D) = "black" {}
    _EmitColor( "Emit Color", Color ) = ( 0.0, 0.0, 0.0, 0.0 )
    _EmitConst_1("EmitConst_1", Range(0.0, 3.0))= 1.0
  }
  SubShader {
    Tags { "RenderType" = "Opaque" }
	Lighting Off Fog { Mode Off }
    ColorMask RGB
	
	Pass {
		CGPROGRAM
		#pragma vertex vert_vct
		#pragma fragment frag_mult
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		float4 _MainColor;
		uniform sampler2D _EmitMap;
		uniform float4 _EmitMap_ST;
		uniform float4 _EmitColor;
		uniform float _EmitConst_1;
		
		
		struct vin_vct 
		{
			float4 vertex : POSITION;
			float4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		struct vout_vct
		{
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		vout_vct vert_vct(vin_vct v)
		{
			vout_vct o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.color = v.color;
			o.texcoord = v.texcoord;
			return o;
		}

		fixed4 frag_mult(vout_vct i) : COLOR
		{
		//lerp
			float zeit = abs(_SinTime.z);
			float4 texE = tex2D( _EmitMap, i.texcoord.xy * _EmitMap_ST.xy + _EmitMap_ST.zw );
			fixed4 col = tex2D(_MainTex, i.texcoord) * i.color *_MainColor;
			col += texE * _EmitColor * _EmitConst_1 * zeit;
			return col;
		}

		ENDCG
	}
	Pass {
		Name "OUTLINE" 
         Tags { "LightMode" = "Always" } 
          
         CGPROGRAM
         #pragma vertex vert
		 #pragma fragment frag_mult
		 #pragma fragmentoption ARB_precision_hint_fastest
		 #include "UnityCG.cginc"

         struct appdata { 
             float4 vertex : POSITION; 
             float3 normal : NORMAL;
         }; 

         struct v2f { 
            float4 pos : POSITION; 
            float4 color : COLOR;
         }; 
		 
         float _Outline; 
         float4 _OutlineColor; 

         v2f vert(appdata v) { 
            v2f o; 
            o.pos = mul(UNITY_MATRIX_MVP, v.vertex); 
            float3 norm = mul ((float3x3)UNITY_MATRIX_MV, v.normal); 
		   	float2 offset = TransformViewToProjection(norm.xy);
 			o.pos.xy += offset  * _Outline;
			o.color = _OutlineColor; 
            return o; 
         }
		 
		 fixed4 frag_mult(v2f i) : COLOR
		{
			return i.color;
		}
         ENDCG 
          
         Cull Front 
         ZWrite On 
         //ColorMask RGB 
         Blend SrcAlpha OneMinusSrcAlpha //macht Transperenz moeglich
         //  
         SetTexture [_MainTex] { combine primary } 
	}
  }
  Fallback "Diffuse"
}