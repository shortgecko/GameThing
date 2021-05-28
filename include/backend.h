#pragma once
#include <math/rect.h>
#include <graphics/texture.h>
#include <graphics/color.h>
#include <input.h>


static int width = 320 * 4;
static int height = 180 * 4;


namespace Cartographer
{
	namespace Backend
	{
		int Intialize();
		void Update();
		void End();

		void ClearBackbuffer(Color& color);

		void DrawBegin();
		void Draw(Cartographer::Texture2D& texture, Cartographer::Rect& rect, Cartographer::Rect& clipRect, Color color);
		void DrawEnd();

		int Width();
		int Height();

		const char* Title();
		bool WindowShouldClose();
	}
}
