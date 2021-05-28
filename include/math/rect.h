#pragma once
#include "vec2.h"

namespace Cartographer
{
	struct Rect
	{
		float x;
		float y;
		float w;
		float h;

		Rect();
		Rect(float rx, float ry, float rw, float rh);
		Rect(Vec2 pos, Vec2 size);

		Rect scale(float s);
		Rect scale(float sx, float sy);

		float left() const;
		float right() const;
		float top() const;
		float bottom() const;

		Vec2 center() const;

		bool contains(const Vec2& value) const;

		void inflate(float horizontalAmount, float verticalAmount);
		void inflate(float amount) { inflate(amount, amount);  }

		bool intersects(Rect& value) const;

		bool operator==(const Rect& rhs) const { return x == rhs.x && y == rhs.y && w == rhs.w && h == rhs.h; }
		bool operator!=(const Rect& rhs) const { return !(*this == rhs); }

		Rect operator+(const Vec2& rhs) const;
		Rect operator-(const Vec2& rhs) const;
		Rect& operator+=(const Vec2& rhs);
		Rect& operator-=(const Vec2& rhs);

	};
}