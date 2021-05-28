#pragma once
#include <backend.h>
#include <renderer.h>

namespace Cartographer
{
	class App
	{
	public:
		bool running = true;
		int initialize();
		void update();
		void render();
		void end();
		void run();
	};
}