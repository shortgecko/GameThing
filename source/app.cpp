#include <app.h>
#include <logger.h>
#include <imgui_backend.h>
#include <imgui/imgui.h>
#include <input.h>

using namespace Cartographer;

Texture2D* texture;
Vec2 position;

int App::initialize()
{
	texture = LoadTexture("assets/stick.png");
	Cartographer::ImGuiRenderer::Innit();
	return 1;
}

float speed = 1.0f;
void App::update()
{
	ImGuiRenderer::Update();
	if (Input::IsKeyDown(Keys::W))
		position.y -= speed;
	else if (Input::IsKeyDown(Keys::S))
		position.y += speed;
	if (Input::IsKeyDown(Keys::A))
		position.x -= speed;
	else if (Input::IsKeyDown(Keys::D))
		position.x += speed;
}

char* buf = new char();

void App::render()
{
	Batch::Clear(Color(255,255,255));

	ImGuiRenderer::NewFrame();
	ImGui::ShowDemoWindow();
	ImGui::Begin("Hello, world!");   
	ImGui::SetWindowSize(ImVec2(500, 500));
	ImGui::InputText("hello world", buf, 10);
	ImGui::End();
	ImGuiRenderer::EndFrame();

	Batch::Begin();
	Batch::Draw(*texture, position, Color::white);
	Batch::End();
}


void App::end()
{
	Cartographer::ImGuiRenderer::Destroy();
	DeleteTexture(*texture);
	delete texture;
}

void App::run()
{
	Backend::Intialize();
	App::initialize();

	while (!Backend::WindowShouldClose() && this->running)
	{
		Backend::Update();
		App::update();
		App::render();
	}

	App::end();
	Backend::End();
}
