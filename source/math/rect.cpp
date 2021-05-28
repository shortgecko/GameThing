#include <math/rect.h>
using namespace Cartographer;

Rect::Rect()
{
	x = y = w = h = 0;
};

Rect::Rect(float rx, float ry, float rw, float rh)
{
	x = rx;
	y = ry;
	w = rw;
	h = rh;
}

Rect::Rect(Vec2 pos, Vec2 size)
{
	x = pos.x;
	y = pos.y;
	w = size.x;
	h = size.y;
}

Rect Rect::scale(float s)
{
	x = (x * s);
	y = (y * s);
	w = (w * s);
	h = (h * s);
	return *this;
}

Rect Rect::scale(float sx, float sy)
{
	x = (x * sx);
	y = (y * sy);
	w = (w * sx);
	h = (h * sy);
	return *this;
}

float Rect::left() const
{
	return x;
}

float Rect::right() const
{
	return x + w;
}

float Rect::top() const
{
	return y;
}

float Rect::bottom() const
{
	return y + h;
}

Vec2 Rect::center() const
{
	return Vec2(x + w / 2, y + h / 2);
}

bool Rect::contains(const Vec2& value) const
{
	return ((((this->x <= value.x) && (value.x < (this->x + this->w))) && (this->y <= value.y)) && (value.y < (this->y + this->h)));
}


void Rect::inflate(float horizontalAmount, float verticalAmount) 
{
	x -= (int)horizontalAmount;
	y -= (int)verticalAmount;
	w += (int)horizontalAmount * 2;
	h += (int)verticalAmount * 2;
}

bool Rect::intersects(Rect& value) const
{
	return value.left() < right() &&
		left() < value.right() &&
		value.top() < bottom() &&
		top() < value.bottom();
}


Rect Rect::operator+(const Vec2& rhs) const { return Rect(x + rhs.x, y + rhs.y, w, h); }
Rect Rect::operator-(const Vec2& rhs) const { return Rect(x - rhs.x, y - rhs.y, w, h); }
Rect& Rect::operator+=(const Vec2& rhs) { x += rhs.x; y += rhs.y; return *this; }
Rect& Rect::operator-=(const Vec2& rhs) { x -= rhs.x; y -= rhs.y; return *this; }