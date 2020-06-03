#pragma once
#include "str.h"
#include "List.h"

struct Char
{
	char value;
	int freq;
	str ToString();
	bool operator ==(Char);
	bool operator !=(Char);
	bool operator ==(char);
	bool operator !=(char);
};
struct Subword
{
	str value;
	int freq;
	str ToString();
	bool operator ==(Subword);
	bool operator !=(Subword);
	bool operator ==(str);
	bool operator !=(str);
};
struct Word
{
	str value;
	int freq;
	str ToString();
	bool operator ==(Word);
	bool operator !=(Word);
	bool operator ==(str);
	bool operator !=(str);
};
struct Sentence
{
	str value;
	int freq;
	str ToString();
	bool operator ==(Sentence);
	bool operator !=(Sentence);
	bool operator ==(str);
	bool operator !=(str);
};
struct Paragraph
{
	str value;
	List<Char> chars;
	List<Sentence> sentences;
	List<Word> words;
	List<Subword> subwords;
	str ToString();
	bool operator ==(Paragraph);
	bool operator !=(Paragraph);
	bool operator ==(str);
	bool operator !=(str);
};