#pragma once
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
	string ToString();
};
struct Subword
{
	string value;
	int freq;
	string ToString();
};
struct Word
{
	string value;
	Subword* subs;
	Char* chars;
	int freq;
	int subsCount;
	int chCount;
	string ToString();
};
struct Sentence
{
	string value;
	Word* words;
	Char* puncts;
	int wCount;
	int freq;
	int pCount;
	string ToString();
};
struct Paragraph
{
	Char* puncts;
	string value;
	Sentence* sentences;
	int sCount;
	int pCount;
	string ToString();
};
string* ReadFile(string path);
int WordsCount();
int CharsCount();
int RowsCount();
void test();

Paragraph* par_analysis(string para);
bool iscyrylic(char ch);
Word* word_analysis(string wrd);
Char* char_analysis(Word* word);
Sentence* sentence_analysis(string stc);
void WriteFile(string path);