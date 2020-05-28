#include <fstream>
#include <iostream>
#include <clocale>
#include "Header.h"
#include <list>

using namespace std;

list<Char> chars;
list<Word> words;

void Analysis(Text txt)
{
	for (int i = 0; i < txt.Length; i++) //цикл по строкам. В продвинутых редакторах строки оканчиваются &nobr;
	{									 //так что тут конец строки == конец абзаца
		Line l = txt.lines[i];
		bool whitespace = false;
		for (int j = 0; j < l.Length; j++)
		{
			if (chars.search)
		}

}

//
//Text vvod(int type = 1, char mark = NULL, int len = DEF_STRLEN) 
//{ //type тип ввода
//	fstream f; //Фаловый поток
//	int i = 0, countWord = 0, flag = 1, length = 0;
//	char ch;
//	char s[DEF_STRLEN];
//
//	f.open("input.txt", ios::in);
//	//fout.open("output.txt");
//	if (!f.is_open())
//	{
//		throw new exception("No such file in directory");
//		//cout << "Файл не найден";
//	}
//	while (!f.eof())
//	{
//		while (f.get(ch) && ch !='\n' && i < DEF_STRLEN)
//			s[i++] = ch;
//		Line n;
//		
//	}
	//
	//else {
	//	//?
	//	if (type == 3) 
	//	{
	//		f >> mark;
	//		cout << mark << " -маркер\n";
	//	}
	//	else if (type == 4) 
	//	{
	//		f >> len;
	//		cout << len << " -длина\n";
	//	}

	//	do 
	//	{
	//		i++; //iterator?
	//		f.get(ch);
	//		f.getline(s, 1000);
	//		fout << ch;
	//		s[i] = ch;
	//		if (flag == 1) 
	//		{
	//			if (ch != ' ') 
	//			{
	//				countWord++;
	//				flag = 0;
	//			}
	//		}
	//		else 
	//		{
	//			if (ch == ' ') flag = 1;
	//		}
	//		if (type == 1 && !(i < len)) break;
	//		else if (type == 2 && (sy == mark)) break;
	//		else if (type > 2 && (!(i < len) || sy == mark)) break;
	//	} while (!f.eof());
	//	fout << endl;
	//}
	//f.close();
	//Text text;
	//text.lines = new Line[countWord];
	//length = i;
	//for (i = 0; i < length; i++) {
	//	cout << s[i];
	//}
	//int k, j = 0;
	//for (i = 0; i < countWord; i++) {
	//	while (s[j] == ' ') {
	//		j++;
	//	}
	//	for (k = 0; s[j + k] != ' ' && j + k < leng; k++) 
	//	{
	//		text.lines[i].chars[k].value = s[j + k];
	//		if (k > 0) 
	//		{
	//			text.lines[i].chars[k].l = k - 1;
	//			text.word[i].a[k - 1].r = k;
	//		}
	//	}
	//	j = j + k;
	//	text.lines[i].Length = k;
	//	text.Length += k;
	//}
//	text.kol = countWord;
//	return text;
//}