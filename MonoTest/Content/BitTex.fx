#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

extern float2 TexSize;
extern float2 Param;
extern float4 TintColor;
extern float2 Viewport;
extern float2 MousePos;

Texture2D OverlayTexture;
sampler2D OverlayTextureSampler = sampler_state
{
    Texture = <OverlayTexture>;
};

Texture2D SpriteTexture;
sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
    //float2 TextureSize : COLOR1;
};


float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 s = tex2D(OverlayTextureSampler, input.TextureCoordinates);
    float v = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    return s + v;
    //float4 c = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    //float seperationLine = 0.4;
    //float isShape = step(0.01, c.w);
    //float isDown = step(seperationLine, input.TextureCoordinates.y);
    //float w = c.w;
    ////c.w = (1 - input.TextureCoordinates.y) * isDown;
    ////c.w = ((1 - input.TextureCoordinates.y)) * isDown;
    ////c.x = 1;
    ////c.y = 0;
    ////c.z = 0;
    ////c.w = 0.01;
    //if(isShape == 1)
    //{
        
    //    c.x = 0.2f;
    //    if(isDown == 1)
    //    {
    //        float4 _c = float4(0, 0, 0, 1-(input.TextureCoordinates.y-seperationLine));
    //        c.w = _c.w;
    //        //c.w = ((1 - input.TextureCoordinates.y)) * isDown;
    //        //c.xyz = float3(1, 1, 1);
    //    }
    //}
    ////c.w = (isShape * w);
    //return c;
    //static int size = int(Param.x);
    //if (size <= 0)
    //{
    //    return tex2Dlod(SpriteTextureSampler, float4(input.TextureCoordinates.xy, 0, 4));
    //}
    
    //int kernel_window_size = size * 2 + 1;
    //int samples = kernel_window_size * kernel_window_size;
    
    //float4 fragColor = float4(0, 0, 0, 0);
    //float4 color = float4(0, 0, 0, 0);
    //float wsum = 0.0;
    
    //for (int ry = -size; ry <= size; ++ry)
    //    for (int rx = -size; rx <= size; ++rx)
    //    {
    //        float w = 1.0;
    //        wsum += w;
    //        color += tex2Dlod(SpriteTextureSampler, float4((input.TextureCoordinates + (float2(rx, ry) * Param.y) / TexSize).xy, 0, 1)) * w;
    //    }
    
    //fragColor = color / wsum;
    //return fragColor;
}


float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    return float4(1, 0, 0, 1);
}

technique SpriteDrawing
{
    pass P0
    {
        //VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};