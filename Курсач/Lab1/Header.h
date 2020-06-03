#pragma once
#include "str.h"
#include <string>
#include <fstream>
#include <iostream>
#include <clocale>

using namespace std;

const int DEF_STRLEN = 100;

struct Char
{
	char value;
	int freq;
	str ToString();
};
struct Subword
{
	str value;
	int freq;
	str ToString();
};
struct Word
{
	str value;
	Subword* subs;
	Char* chars;
	int freq;
	int subsCount;
	int chCount;
	str ToString();
};
struct Sentence
{
	str value;
	Word* words;
	Char* puncts;
	int wCount;
	int freq;
	int pCount;
	str ToString();
};
struct Paragraph
{
	Char* puncts;
	str value;
	Sentence* sentences;
	int sCount;
	int pCount;
	str ToString();
};
str* ReadFile(str path);
int WordsCount();
int CharsCount();
int RowsCount();
void test();

Paragraph* par_analysis(str para);
bool iscyrylic(char ch);
Word* word_analysis(str wrd);
Char* char_analysis(Word* word);
Sentence* sentence_analysis(str stc);
void WriteFile(str path);