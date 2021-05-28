#include <SFML/Window.hpp>
int main()
{
    sf::Window window(sf::VideoMode(800, 600), "My window");
    while (window.isOpen())
    {
        window.display();
    }
    return 0;
}