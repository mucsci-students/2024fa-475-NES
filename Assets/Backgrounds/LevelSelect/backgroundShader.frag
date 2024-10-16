#version 150 core

uniform sampler2D paperTexture;
uniform sampler2D backgroundTexture;
uniform sampler2D detailTexture;
uniform float scale;

in vec3 worldCoord;

out vec4 out_Color;

void main(void)
{	
	vec4 textureData = texture(backgroundTexture,worldCoord.xy);

	if (scale > 10240.0)
	{
		textureData = texture(paperTexture,worldCoord.xy*131072.0f);
	}
	else if (scale > 6400.0)
	{
		float mixAmount = (scale - 6400.0) / 3840.0;
		textureData = mix(texture(paperTexture,worldCoord.xy*8192.0f),texture(paperTexture,worldCoord.xy*131072.0f), mixAmount);
	}
	else if (scale > 640.0)
	{
		textureData = texture(paperTexture,worldCoord.xy*8192.0f);
	}
	else if (scale > 400.0)
	{
		float mixAmount = (scale - 400.0) / 240.0;
		textureData = mix(texture(paperTexture,worldCoord.xy*512.0f),texture(paperTexture,worldCoord.xy*8192.0f), mixAmount);
	}
	else if (scale > 40.0)
	{
		textureData = texture(paperTexture,worldCoord.xy*512.0f);
	}
	else if (scale > 25.0)
	{
		float mixAmount = (scale - 25.0) / 15.0;
		textureData = mix(texture(paperTexture,worldCoord.xy*32.0f),texture(paperTexture,worldCoord.xy*512.0f), mixAmount);
	}
	else if (scale > 5.0)
	{
		textureData = texture(paperTexture,worldCoord.xy*32.0f);
	}
	else if (scale > 2.8)
	{
		float mixAmount = (scale - 2.8) / 2.2;
		textureData = mix(textureData,texture(paperTexture,worldCoord.xy*32.0f),mixAmount);
	}
  	out_Color = textureData;
}