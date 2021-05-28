#pragma once
namespace Cartographer
{
	
	class Texture2D
	{
	public:
		void* Data;
		int Width;
		int Height;
		~Texture2D();

	};

	void DeleteTexture(Texture2D& texture);
	Texture2D* LoadTexture(const char* file);


}