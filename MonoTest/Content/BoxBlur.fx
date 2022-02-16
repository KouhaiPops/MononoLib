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


Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

struct VertexShaderInput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR;
    float2 TextureCoordinates : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
    //float2 TextureSize : COLOR1;
};


//VertexShaderOutput MainVS(VertexShaderInput input)
//{
//    VertexShaderOutput output;
//    output.Color = input.Color;
//    output.Position = input.Position;
//    output.TextureCoordinates = input.TextureCoordinates;
//    float2 textureSize;
//    float s;
//    SpriteTexture.GetDimensions
//    output.TextureSize = textureSize;
//    return output;
//}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    static int size = int(Param.x);
    if (size <= 0)
    {
        return tex2Dlod(SpriteTextureSampler, float4(input.TextureCoordinates.xy, 0, 4));
    }
    
    int kernel_window_size = size * 2 + 1;
    int samples = kernel_window_size * kernel_window_size;
    
    float4 fragColor = float4(0,0,0,0);
    float4 color = float4(0,0,0,0);
    float wsum = 0.0;
    
    for (int ry = -size; ry <= size; ++ry)
        for (int rx = -size; rx <= size; ++rx)
        {
            float w = 1.0;
            wsum += w;
            color += tex2Dlod(SpriteTextureSampler, float4((input.TextureCoordinates + (float2(rx, ry) * Param.y) / TexSize).xy, 0, 1)) * w;
        }
    
    fragColor = color / wsum;
    return fragColor;
    
    //float4 fragColor = float4(0, 0, 0, 0);
    //static int size = int(Param.x);
    //if(size <= 0)
    //{
    //    return tex2Dlod(SpriteTextureSampler, float4(input.TextureCoordinates.xy, 0, 4));
    //}
    
    //float seperation = Param.y;
    //float count;
    //seperation = max(seperation, 1);
    
    //for (int i = -size; i <= size; ++i)
    //{
    //    for (int j = -size; j <= size; ++j)
    //    {
    //        float2 s = float2(i, j) * seperation;
    //        float2 newCoords = input.TextureCoordinates + s;
            
    //        float4 sam = float4(newCoords.xy, 0, 0);
    //        fragColor += tex2Dlod(SpriteTextureSampler, sam);
    //        count++;
    //    }

    //}
    //fragColor /= count++; 
    //return fragColor;
    //pow(size * 2 + 1, 2);
    
    
    //float2 texCoord = tex2D(SpriteTextureSampler, input.TextureCoordinates).xy / xy;
    
    
    //float4 pixelColor = tex2D(SpriteTextureSampler, input.TextureCoordinates) * float4(input.TextureCoordinates.y, input.TextureCoordinates.x, 0, 1);
    //float2 normalized = MousePos / Viewport;
    //return pixelColor * float4(normalized, pixelColor.zw);
    //return pixelColor;

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