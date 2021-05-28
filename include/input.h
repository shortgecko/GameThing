#pragma once
#include <keys.h>
#include <boolean.h>
#include <math/vec2.h>
#include <vector>

namespace Cartographer
{
	namespace Input
	{
		bool IsKeyDown(Keys key);
		bool IsKeyUp(Keys key);
	}
}
