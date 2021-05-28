#pragma once
#include <math/vec2.h>
#include <math.h>

using namespace Cartographer;

Vec2::Vec2()
	: x(0)
	, y(0)
{}

Vec2::Vec2(float vx, float vy)
	: x(vx)
	, y(vy)
{}

Vec2::Vec2(float v)
	:x(v)
	,y(v)
{}


Vec2 Vec2::operator +(const Vec2 rhs) const
{
	return Vec2(x + rhs.x, y + rhs.y);
}

Vec2 Vec2::operator -(const Vec2 rhs) const
{
	return Vec2(x - rhs.x, y - rhs.y);
}

Vec2 Vec2::operator /(const float rhs) const
{
	return Vec2(x / rhs, y / rhs);
}

Vec2 Vec2::operator *(const float rhs) const
{
	return Vec2(x * rhs, y * rhs);
}

Vec2 Vec2::operator-() const
{
	return Vec2(-x, -y);
}

Vec2& Vec2::operator +=(const Vec2& rhs)
{
	x += rhs.x; y += rhs.y; return *this;
}

Vec2& Vec2::operator -=(const Vec2& rhs)
{
	x -= rhs.x; y -= rhs.y; return *this;
}

Vec2& Vec2::operator /=(const Vec2& rhs)
{
	x /= rhs.x; y /= rhs.y; return *this;
}

Vec2& Vec2::operator *=(const Vec2& rhs)
{
	x *= rhs.x; y *= rhs.y; return *this;
}

Vec2& Vec2::operator/=(float rhs)
{
	x /= rhs; y /= rhs; return *this;
}

Vec2& Vec2::operator*=(float rhs)
{
	x *= rhs; y *= rhs; return *this;
}

bool Vec2::operator ==(const Vec2& rhs) const
{
	return x == rhs.x && y == rhs.y;
}

bool Vec2::operator !=(const Vec2& rhs) const
{
	return x != rhs.x || y != rhs.y;
}


Vec2 Vec2::normal() const
{
	if (x == 0 && y == 0)
		return zero;
	float length = this->length();
	return Vec2(x / length, y / length);
}

float Vec2::length_squared() const
{
	return x * x + y * y;
}

float Vec2::length() const
{
	return sqrtf(x * x + y * y);
}

Vec2 Vec2::lerp(Vec2 a, Vec2 b, float t)
{
	if (t == 0)
		return a;
	else if (t == 1)
		return b;
	else
		return a + (b - a) * t;
}

const Vec2 Vec2::unit_x = Vec2(1, 0);
const Vec2 Vec2::unit_y = Vec2(0, 1);
const Vec2 Vec2::right = Vec2(1, 0);
const Vec2 Vec2::up = Vec2(0, -1);
const Vec2 Vec2::down = Vec2(0, 1);
const Vec2 Vec2::left = Vec2(-1, 0);
const Vec2 Vec2::zero = Vec2(0, 0);
const Vec2 Vec2::one = Vec2(1, 1);

#define DIAGONAL_UNIT 0.70710678118f
const Vec2 Vec2::up_right = Vec2(DIAGONAL_UNIT, -DIAGONAL_UNIT);
const Vec2 Vec2::up_left = Vec2(-DIAGONAL_UNIT, -DIAGONAL_UNIT);
const Vec2 Vec2::down_right = Vec2(DIAGONAL_UNIT, DIAGONAL_UNIT);
const Vec2 Vec2::down_left = Vec2(-DIAGONAL_UNIT, DIAGONAL_UNIT);
#undef DIAGONAL_UNIT