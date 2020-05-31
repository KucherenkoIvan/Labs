#include "Header.h"

using namespace std;

const string tab = "    ", tab2 = tab + tab, tab3 = tab2 + tab, tab4 = tab2 + tab2, tab5 = tab4 + tab, tab6 = tab5 + tab, tab7 = tab6 + tab;

Char* chars = new Char[0];
Subword* subwords = new Subword[0];
Word* words = new Word[0];
Sentence* sentences = new Sentence[0];
Paragraph* paragraphs = new Paragraph[0];
int wLen = 0, cLen = 0, sCount = 0, rCount = 0, prCount = 0, sbCount = 0;

string Paragraph::ToString()
{
	string ret = "\nАбзац:";

	string s = "\n" + tab2;
	for (int i = 0; i < sCount; i++)
		s += sentences[i].ToString();
	string p = "\n" + tab2;
	for (int i = 0; i < pCount; i++)
		p += "\n" + tab3 + puncts[i].ToString();

	ret += "\n" + tab + "Текст: \"" + value + "\"";
	ret += "\n" + tab + "Анализ знаков пунктуации: " + p;
	ret += "\n" + tab + "Анализ предложений: " + s;

	return (ret);
}

string Sentence::ToString()
{
	string ret = "\n";
	string w = tab4;
	string p = tab4;
	for (int i = 0; i < wCount; i++)
		w += "\n" + (words[i].ToString());
	for (int i = 0; i < pCount; i++)
		p += "\n" + tab7 + puncts[i].ToString();

	ret += tab3 + "Предложение \"" + value + "\"\n";
	ret += tab3 + "Встретилось " + to_string(freq) + " раз\n";
	ret += tab3 + "Анализ слов:" + w;
	ret += tab3 + "Анализ знаков пунктуации: " + p;
	return (ret);
}
string Word::ToString()
{
	string ret = "\n", sbs = "", chs = "";
	for (int i = 0; i < subsCount; i++)
		sbs += "\n" + tab7 + subs[i].ToString();

	for (int i = 0; i < chCount; i++)
		chs += "\n" + tab7 + chars[i].ToString();
	ret += tab5 + "Слово \"" + value + "\"\n";
	ret += tab5 + "Встретилось " + to_string(freq) + " раз\n";
	ret += tab5 + "Анализ лексем: " + sbs + "\n";
	ret += tab5 + "Анализ символов: " + chs;
	return (ret + "\n");
}
string Subword::ToString()
{
	return ("Лексема \"" + (value) + "\" встретилась " + to_string(freq) + " раз");
}
string Char::ToString()
{
	string h = " ";
	h[0] = value;
	return ("Символ '" + h + "' встретился " + to_string(freq) + " раз");
}
int RowsCount()
{
	return rCount;
}
int WordsCount()
{
	return wLen;
}
int CharsCount()
{
	return cLen;
}
bool iscyrylic(char ch)
{
	char alph[] = "абвгдеёжзиёклмнопрстуфхцчшщъыьэюяЙЦУКЕНГШЩЗХЪЭЖДЛОРПАВЫФЯЧСМИТЬБЮЁ";
	for (int i = 0; i < 66; i++)
		if (alph[i] == ch)
			return true;
	return false;
}
string* ReadFile(string path) //одна строка == один абзац
{
	string* text = new string[0];
	string l = "";
	ifstream f(path);
	if (f.is_open())
	{
		while (getline(f, l))
		{
			l += "";
			string* s = new string[++rCount];
			for (int i = 0; i < rCount - 1; i++)
				s[i] = text[i];
			s[rCount - 1] = l;
			delete[] text;
			text = s;
			par_analysis(l);
		}
	}
	else cout << "wtf?";
	return text;
}
void Sort(Subword* arr, int len)
{
	for (int i = 1; i < len; i++)
	{
		for (int k = 0; k < i - 1; k++)
			if (arr[i - k].freq < arr[i - k - 1].freq)
			{
				Subword temp = arr[i - k];
				arr[i - k] = arr[i - k - 1];
				arr[i - k - 1] = temp;
			}
	}
}
void Compile()
{
	for (int i = 0; i < wLen; i++)
	{
		for (int j = 0; j < words[i].subsCount; j++)
		{
			bool flag = false;
			for (int k = 0; k < sbCount; k++)
			{
				if (words[i].subs[j].value == subwords[k].value)
				{
					subwords[k].freq += words[i].subs[j].freq;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Subword* newarr = new Subword[++sbCount];
				for (int k = 0; k < sbCount - 1; k++)
					newarr[k] = subwords[k];
				newarr[sbCount - 1] = words[i].subs[j];
				delete[] subwords;
				subwords = newarr;
			}
		}
	}
}
void WriteFile(string path)
{
	Compile();
	ofstream o(path);
	for (int i = 0; i < prCount; i++)
		o << paragraphs[i].ToString() << endl;
	for (int i = 0; i < cLen; i++)
		o << chars[i].ToString() << endl;
	Sort(subwords, sbCount);
	for (int i = 0; i < sbCount; i++)
		o << subwords[i].ToString() << endl;
}


Paragraph* par_analysis(string para)
{
	Paragraph* ret = new Paragraph;
	ret->value = para;
	ret->sCount = 0;
	ret->sentences = new Sentence[0];
	int count = 0, p = 0;

	//Анализ знаков пунктуации
	Char* pts = new Char[0];
	for (int i = 0; i < para.length(); i++)
	{
		if (ispunct(para[i]))
		{
			bool flag = false;
			for (int j = 0; j < p; j++)
				if (pts[j].value == para[i])
				{
					pts[j].freq++;
					flag = true;
					break;
				}
			if (!flag)
			{
				Char* ch = new Char;
				ch->value = para[i];
				ch->freq = 1;
				Char* newarr = new Char[++p];
				for (int k = 0; k < p - 1; k++)
					newarr[k] = pts[k];
				newarr[p - 1] = *ch;
				delete[] pts;
				pts = newarr;
			}
		}
	}
	ret->puncts = pts;
	ret->pCount = p;
	//Анализ предложений
	for (int start = 0, i; start < para.length();)
	{
		string stc = "";
		for (i = start; para[i] != '.' && para[i] != '!' && para[i] != '?' && i < para.length(); i++)
		{
			stc += para[i];
		}
		start = i + 1;
		Sentence* s = sentence_analysis(stc);
		bool flag = false;
		for (int j = 0; j < count; j++)
			if (s->value == ret->sentences[j].value)
			{
				ret->sentences[j].freq++;
				flag = true;
				break;
			}
		if (!flag)
		{
			Sentence* newarr = new Sentence[++count];
			for (int k = 0; k < count - 1; k++)
				newarr[k] = ret->sentences[k];
			newarr[count - 1] = *s;
			delete[] ret->sentences;
			ret->sentences = newarr;
		}
	}

	ret->sCount = count;

	Paragraph* newarr = new Paragraph[++prCount];
	for (int i = 0; i < prCount - 1; i++)
		newarr[i] = paragraphs[i];
	newarr[prCount - 1] = *ret;
	delete[] paragraphs;
	paragraphs = newarr;

	return ret;
}
Sentence* sentence_analysis(string stc)
{
	Sentence* ret = new Sentence;
	ret->value = stc;
	ret->wCount = 0;
	ret->words = new Word[0];
	ret->freq = 1;

	string b = "", puncts = "";

	int count = 0;

	for (int i = 0; i <= stc.length(); i++)
	{
		//Учет символов, не являющихся буквами, в общем числе
		if (i != stc.length() && (ispunct(stc[i])) && isprint(stc[i]))
		{
			puncts += stc[i];
			bool flag = false;
			for (int j = 0; j < cLen; j++)
				if (stc[i] == chars[j].value)
				{
					chars[j].freq++;
					flag = true;
					break;
				}
			if (!flag)
			{
				Char* ch = new Char;
				ch->value = stc[i];
				ch->freq = 1;
				Char* newarr = new Char[++cLen];
				for (int j = 0; j < cLen - 1; j++)
					newarr[j] = chars[j];
				newarr[cLen - 1] = *ch;
				delete[] chars;
				chars = newarr;
			}
		}
		else if (i != stc.length() && iscyrylic(stc[i]) || isalpha(stc[i]))
			b += stc[i];
		else
		{
			if (b.empty())
				continue;
			// подсчет слов
			cout << b << endl;
			Word w = *word_analysis(b);

			bool flag = false;

			for (int j = 0; j < ret->wCount; j++)
				if (w.value == ret[j].words->value)
				{
					ret->words[j].freq++;
					flag = true;
					break;
				}

			if (!flag)
			{
				Word* newarr = new Word[++count];
				for (int j = 0; j < count - 1; j++)
					newarr[j] = ret->words[j];
				newarr[count - 1] = w;
				delete[] ret->words;
				ret->words = newarr;
			}

			b = "";
		}
	}

	//подсчет общего числа таких же предложений
	bool flag = false;
	for (int i = 0; i < sCount; i++)
		if (ret->value == sentences[i].value)
		{
			sentences[i].freq++;
			flag = true;
			break;
		}

	//подсчет знаков пунктуации
	Char* temp = new Char[0];
	int tmp = 0;
	for (int i = 0; i < puncts.length(); i++)
	{
		bool flag = false;
		for (int j = 0; j < tmp; j++)
			if (puncts[i] == temp[j].value)
			{
				flag = true;
				temp[j].freq++;
				break;
			}
		if (!flag)
		{
			Char* newarr = new Char[++tmp];
			Char* ch = new Char;
			ch->value = puncts[i];
			ch->freq = 1;
			for (int j = 0; j < tmp - 1; j++)
				newarr[j] = temp[j];
			newarr[tmp - 1] = *ch;
			delete[] temp;
			temp = newarr;
		}
	}
	ret->pCount = tmp;
	ret->puncts = temp;
	ret->wCount = count;

	if (!flag)
	{
		Sentence* newarr = new Sentence[++sCount];
		for (int j = 0; j < sCount - 1; j++)
			newarr[j] = sentences[j];
		newarr[sCount - 1] = *ret;
		delete[] sentences;
		sentences = newarr;
	}
	return ret;
}
Word* word_analysis(string wrd)
{
	if (wrd == "") return NULL;
	for (int wlen = 0; wlen < wrd.length(); wlen++)
	{
		bool flag = false;
		for (int j = 0; j < cLen; j++)
			if (wrd[wlen] == chars[j].value)
			{
				chars[j].freq++;
				flag = true;
				break;
			}
		if (!flag)
		{
			Char* ch = new Char;
			ch->freq = 1;
			ch->value = wrd[wlen];
			Char* newarr = new Char[++cLen];
			for (int j = 0; j < cLen; j++)
				newarr[j] = chars[j];
			newarr[cLen - 1] = *ch;
			delete[] chars;
			chars = newarr;
		}
	}


	for (int i = 0; i < wLen; i++)
		if (words[i].value == wrd)
		{
			words[i].freq++;
			return &words[i];
		}

	Word* ret = new Word;
	ret->value = wrd;
	ret->freq = 1;
	ret->subs = new Subword[0];
	ret->chars = char_analysis(ret);

	int sCount = 0;
	for (int sub_len = 1; sub_len <= wrd.length(); sub_len++)
		for (int i = 0; i + sub_len <= wrd.length(); i++)
		{
			string sub = "";
			for (int j = 0; j < sub_len; j++)
				sub += wrd[i + j];
			bool flag = false;
			for (int j = 0; j < sCount; j++)
				if (ret->subs[j].value == sub)
				{
					ret->subs[j].freq++;
					flag = true;
					break;
				}
			if (!flag)
			{
				Subword* s = new Subword;
				s->freq = 1;
				s->value = sub;

				Subword* newarr = new Subword[++sCount];

				for (int j = 0; j < sCount - 1; j++)
					newarr[j] = ret->subs[j];
				newarr[sCount - 1] = *s;
				delete[] ret->subs;
				ret->subs = newarr;
			}
		}
	ret->subsCount = sCount;
	Word* newarr = new Word[++wLen];
	for (int i = 0; i < wLen - 1; i++)
		newarr[i] = words[i];
	newarr[wLen - 1] = *ret;
	delete[] words;
	words = newarr;

	return ret;
}

Char* char_analysis(Word* word)
{
	string wrd = (*word).value;
	if (wrd == "") return NULL;

	Char* ret = new Char[0];
	int count = 0;

	//Поиск в слове
	for (int i = 0; i < word->value.length(); i++)
	{
		bool flag = false;
		for (int j = 0; j < count; j++)
			if (wrd[i] == ret[j].value)
			{
				ret[j].freq++;
				flag = true;
				break;
			}
		if (!flag)
		{
			Char* newarr = new Char[++count];
			Char* ch = new Char;
			ch->value = wrd[i];
			ch->freq = 1;
			for (int j = 0; j < count; j++)
				newarr[j] = ret[j];
			newarr[count - 1] = *ch;
			delete[] ret;
			ret = newarr;
		}
	}
	(*word).chCount = count;
	return ret;
}