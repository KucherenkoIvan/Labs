#include "header.h"
#include <fstream>

const int MAXLEN = 1000;
str readFile(str path)
{
	std::ifstream input;
	input.open(path.value);
	str ret;
	char* buffer;
	while (!input.eof())
	{
		buffer = new char[MAXLEN];
		input.getline(buffer, MAXLEN);
		ret += str(buffer) + "\n";
		if (str(buffer) == "") break;
		delete[] buffer;
	}
	input.close();
	return ret;
}
void writeFile(str path, str text)
{
	std::ofstream s;
	s.open(path.value);
	s << (!&text ? "" : text.value);
	s.close();
}
void addToFile(str path, str text)
{
	std::ofstream s;
	s.open(path.value, std::ios::app);
	s << (!&text ? "" : text.value);
	s.close();
}