void GetMainLight_float(float3 worldPos, out float3 lightDir, out float3 lightColor)
{
    #ifdef SHADERGRAPH_PREVIEW
        lightDir = float3(0.5,0.5,0.5);
        lightColor = 1;
    #else
        Light mainLight = GetMainLight();
        lightDir = mainLight.direction;
        lightColor = mainLight.color;
    #endif
}