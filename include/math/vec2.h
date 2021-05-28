#pragma once
namespace Cartographer
{
	struct Vec2
	{
		float x;
		float y;

		Vec2();
		Vec2(float v);
		Vec2(float vx, float vy);

		Vec2 operator +(const Vec2 rhs) const;
		Vec2 operator -(const Vec2 rhs) const;
		Vec2 operator /(const float rhs) const;
		Vec2 operator *(const float rhs) const;
		Vec2 operator -() const;

		Vec2& operator +=(const Vec2& rhs);
		Vec2& operator -=(const Vec2& rhs);
		Vec2& operator /=(const Vec2& rhs);
		Vec2& operator *=(const Vec2& rhs);
		Vec2& operator /=(float rhs);
		Vec2& operator *=(float rhs);

		bool operator ==(const Vec2& rhs) const;
		bool operator !=(const Vec2& rhs) const;


		// Returns the Normalized Vector
		// If the length is 0, the resulting Vector is 0,0
		Vec2 normal() const;


		// Returns the length of the Vector
		float length() const;

		// Returns the squared length of the Vector
		float length_squared() const;

		// Lerps between two Vectors
		static Vec2 lerp(Vec2 start, Vec2 end, float t);

		// (1, 0)
		static const Vec2 unit_x;

		// (0, 1)
		static const Vec2 unit_y;

		// (1, 0)
		static const Vec2 right;

		// (0, -1)
		static const Vec2 up;

		// (0, 1)
		static const Vec2 down;

		// (-1, 0)
		static const Vec2 left;

		// (0, 0)
		static const Vec2 zero;

		// (1, 1)
		static const Vec2 one;

		// (0.707, -0.707)
		static const Vec2 up_right;

		// (-0.707, -0.707)
		static const Vec2 up_left;

		// (0.707, 0.707)
		static const Vec2 down_right;

		// (-0.707, 0.707)
		static const Vec2 down_left;
	};
}