#include "structs.h"

str Char::ToString()
{
	str ret("Символ '");
	ret += str::tostr(value) + "' встретился " + str::tostr(freq) + " раз(a)";
	return ret;
}
str Subword::ToString()
{
	str ret("Лексема '");
	ret += value + "' встретилась " + str::tostr(freq) + " раз(a)";
	return ret;
}
str Word::ToString()
{
	str ret("Слово '");
	ret += value + "' встретилось " + str::tostr(freq) + " раз(a)";
	return ret;
}
str Sentence::ToString()
{
	str ret("Предложение '");
	ret += value + "' встретилось " + str::tostr(freq) + " раз(a)";
	return ret;
}
str Paragraph::ToString()
{
	str tab("    ");
	str tab2 = tab + tab;
	str ret("Абзац: \n");
	ret += value + "\n";

	str stc;
	List<Sentence>* currstc = &sentences;
	while (currstc->next != NULL)
	{
		stc += tab + currstc->value.ToString() + "\n";
		currstc = currstc->next;
	}

	str wrd;
	List<Word>* currwrd = &words;
	while (currwrd->next != NULL)
	{
		wrd += tab + currwrd->value.ToString() + "\n";
		currwrd = currwrd->next;
	}

	str sub;
	List<Subword>* currsub = &subwords;
	while (currsub != NULL)
	{
		sub += tab + currsub->value.ToString() + "\n";
		currsub = currsub->next;
	}

	str ch;
	List<Char>* currch = &chars;
	while (currch->next != NULL)
	{
		ch += tab + currch->value.ToString() + "\n";
		currch = currch->next;
	}
	ret += stc + "\n" + wrd + "\n" + sub + "\n" + ch;
	return ret;
}





//operators

//paragraph
bool Paragraph::operator==(Paragraph val)
{
	return value == val.value;
}
bool Paragraph::operator!=(Paragraph val)
{
	return value != val.value;
}
bool Paragraph::operator==(str val)
{
	return value == val;
}
bool Paragraph::operator!=(str val)
{
	return value != val;
}


//sentence
bool Sentence::operator==(Sentence val)
{
	return value == val.value;
}
bool Sentence::operator!=(Sentence val)
{
	return value != val.value;
}
bool Sentence::operator==(str val)
{
	return value == val;
}
bool Sentence::operator!=(str val)
{
	return value != val;
}

//word
bool Word::operator==(Word val)
{
	return value == val.value;
}
bool Word::operator!=(Word val)
{
	return value != val.value;
}
bool Word::operator==(str val)
{
	return value == val;
}
bool Word::operator!=(str val)
{
	return value != val;
}

//subword
bool Subword::operator==(Subword val)
{
	return value == val.value;
}
bool Subword::operator!=(Subword val)
{
	return value != val.value;
}
bool Subword::operator==(str val)
{
	return value == val;
}
bool Subword::operator!=(str val)
{
	return value != val;
}

//char
bool Char::operator==(Char val)
{
	return value == val.value;
}
bool Char::operator!=(Char val)
{
	return value != val.value;
}
bool Char::operator==(char val)
{
	return value == val;
}
bool Char::operator!=(char val)
{
	return value != val;
}