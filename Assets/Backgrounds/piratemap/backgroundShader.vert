#version 150 core

uniform mat3 screenViewMatrix;
uniform mat3 toWorldMatrix;

in vec2 position;
in vec2 uv;
in vec4 colour;

out vec3 worldCoord;

void main(void)
{  	
	worldCoord = toWorldMatrix*vec3(position,1.0);
	gl_Position = vec4(screenViewMatrix*vec3(position,1.0),1.0);
}