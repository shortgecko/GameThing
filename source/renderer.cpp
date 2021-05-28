#pragma once
#include <renderer.h>
#include <logger.h>
using namespace Cartographer;

void Batch::Clear(Color& color)
{
	Backend::ClearBackbuffer(color);
}

void Batch::Begin()
{
	Backend::DrawBegin();
}

void Batch::Draw(Texture2D& texture, Vec2& position, Color color)
{
	Rect rect(position.x, position.y, texture.Width, texture.Height);
	Rect clip(0, 0, texture.Width, texture.Height);
	Backend::Draw(texture, rect, clip, color);
}

void Batch::Draw(Texture2D& texture, Rect& rect, Color color)
{
	Rect clip(0, 0, texture.Width, texture.Height);
	Backend::Draw(texture, rect, clip, color);
}

void Batch::Draw(Texture2D& texture, Rect& rect, Rect& clip, Color color)
{
	Backend::Draw(texture, rect, clip, color);
}

void Batch::End()
{
	Backend::DrawEnd();
}