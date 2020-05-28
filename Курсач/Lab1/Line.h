#pragma once
#include "Header.h"
class Line
{
public:
	int Length = 0;
	char* line;
	Line(char l[DEF_STRLEN])
	{
		line = l;
		for (; line[Length] != '\0'; Length++);
	}
	Line() 
	{
		line = new char[DEF_STRLEN];
	}
};

