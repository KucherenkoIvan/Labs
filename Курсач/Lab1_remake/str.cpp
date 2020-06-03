#include "str.h"
#include "List.h"

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

List<str> str::split(str delim, bool mode, bool rem) //разделение строки на подстроки
{
	List<str> ret;
	int l = len();
	str buffer;
	for (int i = 0; i < l; i++)
	{
		if (mode ? value[i] != delim.value[0] : !delim.contains(value[i])) //пока не дошли до разделителя
			buffer += value[i];
		else
		{
			if (l - i < delim.len() && mode) //если длина строки меньше длины разделителя - возвращаем остаток строки
			{
				buffer += value[i];
				continue;
			}

			bool flag = true;
			int j = 0;

			for (; j < delim.len() && flag && mode; j++) //до первого несовпадающего символа
				flag = value[i + j] == delim.value[j];
			for (;										//split::removeEmptyEntryes
				(i + j + (mode ? delim.len() : 1)) < l &&
				((str("\n\r\t").contains(value[i + j + (mode ? delim.len() : 1)])) &&
					!delim.contains(value[i + j + (mode ? delim.len() : 1)]));)
				j++;

			if (flag) //если встречен в точности разделитель
			{
				if (buffer == "")
				{
					i += mode ? j - 1 : j;
					continue;
				}
				ret.add(buffer + (rem ? str() : (mode ? delim.value : str::tostr(value[i]))));
				buffer = "";
				i += mode ? j - 1 : j;
			}
			else buffer += value[i];
		}
	}
	ret.add(buffer);
	return ret;
}

//содержит ли строка подстроку
bool str::contains(str sub)
{
	bool ret = true;
	for (int i = 0; i + sub.len() - 1 < len(); i++)
	{
		ret = true;
		for (int j = 0; j < sub.len(); j++)
			ret &= sub.value[j] == value[i + j];
		if (ret) return true;
	}
	return false;
}

bool str::contains(std::string sub)
{
	return contains(str(sub));
}
bool str::contains(char sub)
{
	str t;
	t.value[0] = sub;
	return contains(t);
}
bool str::contains(char* sub)
{
	return contains(str(sub));
}
bool str::contains(const char sub[])
{
	return contains(str(sub));
}



str str::tostr(char val)
{
	str tmp;
	tmp.value[0] = val;
	return tmp;
}
str str::tostr(int val)
{
	char buffer[10]; 
	_itoa_s(val, buffer, 10);
	return str(buffer);
}


//операторы
str str::operator+(str val)	//str + str

//Oсновная перегрузка, вокруг которой строится вся работа операторов (вся логика здесь)

{
	int l = len();
	int vl = val.len();
	int c = l + vl < cap_ - 1 ? cap_ : val.cap() + cap_; // рассчет выделяемой под новую строку памяти

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

str str::operator+(char val) //str + char
{
	str ret(" ");
	ret.value[0] = val;
	return *this + ret;
}

void str::operator+=(char val) //str += char
{
	str temp = *this + val;
	this->cap_ = temp.cap_;
	this->value = temp.value;
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


//операторы присваивания
void str::operator=(std::string val)
{
	delete[] value;
	str temp(val);
	cap_ = temp.cap_;
	value = temp.value;
}
void str::operator=(char* val)
{
	delete[] value;
	str temp(val);
	cap_ = temp.cap_;
	value = temp.value;
}
void str::operator=(const char val[])
{
	delete[] value;
	str temp(val);
	cap_ = temp.cap_;
	value = temp.value;
}

str& str::operator=(const str& val)
{
	*this = val.value;
	return *this;
}
