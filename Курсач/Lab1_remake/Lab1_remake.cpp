#include "header.h"

using namespace std;
int main()
{
	setlocale(LC_ALL, "Russian");

	str temp = readFile(str("input.txt"));

	cout << "Исходный текст:\n" << temp.value << endl;

	writeFile(str("output.txt"), str("Исходный текст:\n") + temp); //Чтение из файла

	List<Paragraph> P = getParagraps(temp);//Разбиение на параграфы

	for (int i = 0; i < P.count(); i++) //анализ каждого параграфа и сохранение в файл
	{
		temp = analyse(P.at(i)->value).ToString();
		addToFile(str("output.txt"), temp.value);
		cout << temp.value;
	}

	/*
		Структура списка P:

		P:	[0]--------------------------------------------------------------------------[1]-------------------------------------------------[...]
			 |_Sentences:  [0]--[1]--[...]  - предложения абзаца						  |_Sentences:  [0]--[1]--[...] 
			 |_Words:   [0]--[1]--[...]   - все слова абзаца							  |_Words:   [0]--[1]--[...]
			 |_Subwords:   [0]--[1]--[...]   - все лексемы всех слов абзаца				  |_Subwords:   [0]--[1]--[...]
			 |_Chars:   [0]--[1]--[...]   - все символы всех слов абзаца				  |_Chars:   [0]--[1]--[...]

			 Каждый указанный элемент - список, вложенный в объект, хранящийся в P
	*/
	return 0;
}