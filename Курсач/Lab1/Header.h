#pragma once
#include "Text.h"
#include "Line.h"

using namespace std;

const int DEF_STRLEN = 1000;

struct Char
{
	char value;
	int freq; //������� ������������� �������
};
struct Subword
{
	char* value;
	int freq; //������� ������������� ��������������
};
struct Word
{
	char* value;
	Subword* subs; //������ �������������� �����
};

//struct Char
//{
//	char value;
//	int freq; //������� ������������� �������
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