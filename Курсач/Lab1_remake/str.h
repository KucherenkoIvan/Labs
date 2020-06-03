#pragma once
#include <iostream>
#include "List.h"
class str
{
private:
	const int MAXLEN = 1000000; //максимальная длина строки, из которой может быть создан объект
	int cap_, delta_ = 10; //вместимость строки и запас на расширение
	const char placeholder_ = '\0'; //символ, которым по умолчанию заполняется строка
	void init_();					//
	void fill_(char* val);			//заполнение строки символом placeholder_
	void fill_(std::string val);	//
public:
	char* value;
	str();
	str(std::string val);
	str(char* val);
	int len(); //длина
	int cap(); //вместимость
	List<str> split(str delim, bool mode = true, bool rem = true); //разделение строки. mode = true - по подстроке, mode = false - по символам подстроки; rem = сохранять разделители?
	
	bool contains(str sub);		//Содержит ли строка подстроку / символ?
	bool contains(std::string sub);
	bool contains(char* sub);
	bool contains(char sub);
	bool contains(const char sub[]);

	static str tostr(char); //конвертация
	static str tostr(int);


	str operator +(str);		//Далее следуют парные перегрузки операторов + и +=, используемые для облегчения работы со строчными данными
	void operator +=(str);
	str operator +(std::string);
	void operator +=(std::string);
	str operator +(char*);
	void operator +=(char*);
	str operator +(const char[]);
	void operator +=(const char[]);
	str operator +(char);
	void operator +=(char);

	bool operator ==(str);		//Парные перегрузки операторов == и !=, опять же, для облегчения работы со строчными данными
	bool operator !=(str);
	bool operator ==(std::string);
	bool operator !=(std::string);
	bool operator ==(char*);
	bool operator !=(char*);
	bool operator ==(const char[]);
	bool operator !=(const char[]);

	//операторы присваивания
	void operator =(std::string);
	void operator =(char*);
	void operator =(const char[]);
	str& operator =(const str&);
};

