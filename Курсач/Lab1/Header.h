#pragma once
#include "Text.h"
#include "Line.h"

using namespace std;

const int DEF_STRLEN = 1000;

struct Char
{
	char value;
	int freq; //частота встречаемости символа
};
struct Subword
{
	char* value;
	int freq; //частота встречаемости буквосочетани€
};
struct Word
{
	char* value;
	Subword* subs; //массив буквосочетаний слова
};

//struct Char
//{
//	char value;
//	int freq; //„астота встречаемости символа
//};
//
//struct Line
//{
//	char chars[DEF_STRLEN];
//	int Length;
//};
//
//struct Text
//{
//	Line* lines;
//	int Length = 0;
//};