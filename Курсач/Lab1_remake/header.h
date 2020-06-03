#pragma once
#include <clocale>
#include "str.h"
#include "structs.h"
#include <iostream>

//fileio.cpp
str readFile(str);
void writeFile(str, str);
void addToFile(str, str);

//analytics.cpp
List<Paragraph> getParagraps(str);
Paragraph analyse(Paragraph);
Paragraph analyse(str);