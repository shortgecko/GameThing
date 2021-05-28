#include <graphics/color.h>

using namespace Cartographer;

char const hex[16] = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
#define LT_HEX_VALUE(n) ((n >= '0' && n <= '9') ? (n - '0') : ((n >= 'A' && n <= 'F') ? (10 + n - 'A') : ((n >= 'a' && n <= 'f') ? (10 + n - 'a') : 0)))

Color::Color()
	: r(0)
	, g(0)
	, b(0)
	, a(0) {}

Color::Color(int rgb)
	: r((u8)((rgb & 0xFF0000) >> 16))
	, g((u8)((rgb & 0x00FF00) >> 8))
	, b((u8)(rgb & 0x0000FF))
	, a(255) {}

Color::Color(int rgb, float alpha)
	: r((int)(((u8)((rgb & 0xFF0000) >> 16))* alpha))
	, g((int)(((u8)((rgb & 0x00FF00) >> 8))* alpha))
	, b((int)(((u8)(rgb & 0x0000FF))* alpha))
	, a((int)(255 * alpha)) {}

Color::Color(u8 r, u8 g, u8 b)
	: r(r)
	, g(g)
	, b(b)
	, a(255) {}

Color::Color(u8 r, u8 g, u8 b, u8 a)
	: r(r)
	, g(g)
	, b(b)
	, a(a) {}



Color Color::lerp(Color a, Color b, float amount)
{
	if (amount < 0) amount = 0;
	if (amount > 1) amount = 1;

	return Color(
		(u8)(a.r + (b.r - a.r) * amount),
		(u8)(a.g + (b.g - a.g) * amount),
		(u8)(a.b + (b.b - a.b) * amount),
		(u8)(a.a + (b.a - a.a) * amount)
	);
}

Color Color::operator*(float multiply) const
{
	return Color(
		(int)(r * multiply),
		(int)(g * multiply),
		(int)(b * multiply),
		(int)(a * multiply));
}

Color& Color::operator=(const int rgb)
{
	r = (u8)((rgb & 0xFF0000) >> 16);
	g = (u8)((rgb & 0x00FF00) >> 8);
	b = (u8)(rgb & 0x0000FF);
	a = 255;
	return *this;
}

bool Color::operator ==(const Color& rhs) const { return r == rhs.r && g == rhs.g && b == rhs.b && a == rhs.a; }
bool Color::operator !=(const Color& rhs) const { return r != rhs.r || g != rhs.g || b != rhs.b || a != rhs.a; }

const Color Color::transparent = Color(0, 0, 0, 0);
const Color Color::white = Color(255, 255, 255, 255);
const Color Color::black = Color(0, 0, 0, 255);
const Color Color::red = Color(255, 0, 0, 255);
const Color Color::green = Color(0, 255, 0, 255);
const Color Color::blue = Color(0, 0, 255, 255);
const Color Color::yellow = Color(255, 255, 0, 255);
const Color Color::purple = Color(255, 0, 255, 255);
const Color Color::teal = Color(0, 255, 255, 255);