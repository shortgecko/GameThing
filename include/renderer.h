#pragma once
#include <backend.h>
#include <graphics/texture.h>
#include <graphics/color.h>
#include <math/vec2.h>

namespace Cartographer
{
	namespace Batch
	{
		void Clear(Color& color);
		void Begin();
		void Draw(Texture2D& texture, Vec2& position, Color color);
		void Draw(Texture2D& texture, Rect& rect, Color color);
		void Draw(Texture2D& texture, Rect& rect, Rect& clip, Color color);
		void End();
	}
}