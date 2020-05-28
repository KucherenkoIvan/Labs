#pragma once
#include "Line.h"
#include "Header.h"
#include <fstream>
using namespace std;

class Text
{
	
public:

	int Length = 0;
	Line* lines;
	void AddLine(Line line);
	void ReadFile(char* path);
	char* LineAt(int index);
};

