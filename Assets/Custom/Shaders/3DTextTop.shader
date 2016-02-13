Shader "GUI/3D Text Shader Top" { 
Properties { 
   _MainTex ("Font Texture", 2D) = "white" {} 
   _Color ("Text Color", Color) = (1,1,1,1) 
   _GlowTex ("Glow", 2D) = "" {}
   _GlowColor ("Glow Color", Color)  = (1,1,1,1)
   _GlowStrength ("Glow Strength", Float) = 1.0
} 

SubShader { 
   Tags { "Queue"="Transparent+1" "IgnoreProjector"="True" "RenderType"="Glow11Transparent" "RenderEffect"="Glow11Transparent" } 
   Lighting Off ZWrite Off Fog { Mode Off } 
   Blend SrcAlpha OneMinusSrcAlpha 
   Pass { 
      Color [_Color] 
      SetTexture [_MainTex] { 
         combine primary, texture * primary 
      } 
   } 
} 
CustomEditor "GlowMatInspector"
}