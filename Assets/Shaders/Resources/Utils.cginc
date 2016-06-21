
float HowManyAround (sampler2D bitmap, float2 p, float2 resolution)
{
	int count = 0;
	count += step(0.2, Luminance(tex2D(bitmap, p + float2(-1.0, -1.0) / resolution)));
	count += step(0.2, Luminance(tex2D(bitmap, p + float2( 0.0, -1.0) / resolution)));
	count += step(0.2, Luminance(tex2D(bitmap, p + float2( 1.0, -1.0) / resolution)));
	count += step(0.2, Luminance(tex2D(bitmap, p + float2(-1.0,  0.0) / resolution)));
	count += step(0.2, Luminance(tex2D(bitmap, p + float2( 1.0,  0.0) / resolution)));
	count += step(0.2, Luminance(tex2D(bitmap, p + float2(-1.0,  1.0) / resolution)));
	count += step(0.2, Luminance(tex2D(bitmap, p + float2( 0.0,  1.0) / resolution)));
	count += step(0.2, Luminance(tex2D(bitmap, p + float2( 1.0,  1.0) / resolution)));
	return count;
}