#include "Text.h"

void Text::ReadFile(char* path)
{
	ifstream f(path);
	char line[DEF_STRLEN];
	while (!f.eof())
	{
		f.getline(line, DEF_STRLEN);
		Line l(line);
		AddLine(l);
		Length++;
	}
}
void Text::AddLine(Line line)
{
	Line* newLines = new Line[Length + 1];
	for (int i = 0; i < Length - 1; i++)
		newLines[i] = lines[i];
	newLines[Length - 1] = line;
	lines = newLines;
}
char* Text::LineAt(int index)
{
	if (index < Length)
		return lines[index].line;
	else return NULL;
}