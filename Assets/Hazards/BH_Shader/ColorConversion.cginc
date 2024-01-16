// Convert RGB to HSV
float3 RGBtoHSV(float3 color)
{
    float maxVal = max(max(color.r, color.g), color.b);
    float minVal = min(min(color.r, color.g), color.b);
    float delta = maxVal - minVal;

    float3 hsv = float3(0, 0, maxVal);

    if (maxVal > 0)
    {
        hsv.y = (maxVal > 0) ? delta / maxVal : 0;

        if (delta > 0)
        {
            hsv.x = (color.r == maxVal) ? (color.g - color.b) / delta : ((color.g == maxVal) ? 2 + (color.b - color.r) / delta : 4 + (color.r - color.g) / delta);
            hsv.x = (hsv.x < 0) ? hsv.x + 6 : hsv.x;
            hsv.x = hsv.x * 60;
        }
    }

    return hsv;
}

// Convert HSV to RGB
float3 HSVtoRGB(float3 hsv)
{
    float c = hsv.y * hsv.z;
    float h = (hsv.x / 60.0) % 6;
    float x = c * (1.0 - abs(h - 2 * floor(h / 2) - 1.0));

    float3 rgb = float3(0, 0, 0);

    if (h >= 0 && h < 1)
        rgb = float3(c, x, 0);
    else if (h >= 1 && h < 2)
        rgb = float3(x, c, 0);
    else if (h >= 2 && h < 3)
        rgb = float3(0, c, x);
    else if (h >= 3 && h < 4)
        rgb = float3(0, x, c);
    else if (h >= 4 && h < 5)
        rgb = float3(x, 0, c);
    else if (h >= 5 && h < 6)
        rgb = float3(c, 0, x);

    float minVal = hsv.z - c;
    rgb += minVal;

    return rgb;
}