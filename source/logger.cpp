#include <logger.h>
#define NEWLINE printf("\n")

using namespace Cartographer;

void Cartographer::Log(const char* in)
{
	printf(in); NEWLINE;
}
void Cartographer::Log(float in)
{
	printf("%9.6f", in);
	NEWLINE;
}
void Cartographer::Log(int in)
{
	printf("%d", in);
	NEWLINE;
}
