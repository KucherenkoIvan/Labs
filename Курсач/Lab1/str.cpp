#include "str.h"

//private:
void str::init_()
{
	for (int i = 0; i < cap_; i++)
		value[i] = placeholder_;
}
void str::fill_(char* val)
{
	for (int i = 0; i < cap_ - delta_; i++)
		value[i] = val[i];
}
void str::fill_(std::string val)
{
	for (int i = 0; i < val.length(); i++)
		value[i] = val[i];
}

//конструкторы
str::str()
{
	value = new char[delta_];
	cap_ = delta_;
	init_();
}
str::str(std::string val)
{
	cap_ = val.length() + delta_;
	value = new char[cap_];
	init_();
	fill_(val);
}
str::str(char* val)
{
	cap_ = strnlen_s(val, MAXLEN) + delta_;
	value = new char[cap_];
	init_();
	fill_(val);
}


//public:
int str::len()
{
	int ret = cap_ - 1;
	while (value[--ret] == placeholder_);
	return ret + 1;
}

int str::cap()
{
	return cap_;
}


//операторы
str str::operator+(str val)	//str + str

//Oсновная перегрузка, вокруг которой строится вся работа операторов (вся логика здесь)

{
	int c = val.cap() + cap_; // рассчет выделяемой под новую строку памяти
	int l = len();
	int vl = val.len();

	str* s = new str(); //создание новой строки
	s->cap_ = c;
	s->value = new char[c];
	s->init_();

	for (int i = 0; i < l; i++)
		s->value[i] = value[i]; //перенос строки this
	for (int i = 0; i < vl; i++) //перенос строки val
		s->value[l + i] = val.value[i];

	return *s;
}

void str::operator+=(str val) //str += str
{
	str res =*this + val;
	this->value = res.value;
	this->cap_ = res.cap_;
}

str str::operator+(std::string val) //str + string
{
	return *this + str(val);
}

void str::operator+=(std::string val) //str += string
{
	str res = *this + val;
	this->value = res.value;
	this->cap_ = res.cap_;
}

str str::operator+(char* val) //str + char*
{
	return *this + str(val);
}

void str::operator+=(char* val) //str += char*
{
	str res = *this + val;
	this->value = res.value;
	this->cap_ = res.cap_;
}

str str::operator+(const char val[]) //str + const char[]
{
	std::string s = "";
	s += val;
	return *this + str(s);
}

void str::operator+=(const char val[]) //str += const char[]
{
	str res = *this + val;
	this->value = res.value;
	this->cap_ = res.cap_;
}

//Еще операторы
bool str::operator==(str val) //str == str

//Oсновная перегрузка, вокруг которой строится вся работа операторов (вся логика здесь)

{
	if (this->len() != val.len()) return false;
	bool flag = true;
	int l = len();
	for (int i = 0; flag && i < l; i++)
		flag = value[i] == val.value[i];
	return flag;
}

bool str::operator!=(str val) //str != str
{
	return !(*this == val);
}

bool str::operator==(std::string val) //str == string
{
	return *this == str(val);
}

bool str::operator!=(std::string val) //str != string
{
	return !(*this == str(val));
}

bool str::operator==(char* val) //str == char*
{
	return *this == str(val);
}

bool str::operator!=(char* val) //str != char*
{
	return !(*this == str(val));
}

bool str::operator==(const char val[]) //str == const char[]
{
	return *this == str(val);
}

bool str::operator!=(const char val[]) //str != const char
{
	return !(*this == str(val));
}
