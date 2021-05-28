#pragma once
#include <stdint.h>
using u8 = uint8_t;

namespace Cartographer
{

	struct Color
	{
		u8 r;
		u8 g;
		u8 b;
		u8 a;

		Color();
		Color(int rgb);
		Color(int rgb, float alpha);
		Color(u8 r, u8 g, u8 b);
		Color(u8 r, u8 g, u8 b, u8 a);


		// Lerps between two colors
		static Color lerp(Color a, Color b, float amount);

		// Mutliplties the Color
		Color operator*(float multiply) const;

		// assignment from int
		Color& operator= (const int rgb);

		// comparisons
		bool operator ==(const Color& rhs) const;
		bool operator !=(const Color& rhs) const;

		static const Color transparent;
		static const Color white;
		static const Color black;
		static const Color red;
		static const Color green;
		static const Color blue;
		static const Color yellow;
		static const Color orange;
		static const Color purple;
		static const Color teal;
	};
}
