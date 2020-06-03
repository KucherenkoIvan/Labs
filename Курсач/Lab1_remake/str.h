#pragma once
#include <iostream>
#include "List.h"
class str
{
private:
	const int MAXLEN = 1000000; //������������ ����� ������, �� ������� ����� ���� ������ ������
	int cap_, delta_ = 10; //����������� ������ � ����� �� ����������
	const char placeholder_ = '\0'; //������, ������� �� ��������� ����������� ������
	void init_();					//
	void fill_(char* val);			//���������� ������ �������� placeholder_
	void fill_(std::string val);	//
public:
	char* value;
	str();
	str(std::string val);
	str(char* val);
	int len(); //�����
	int cap(); //�����������
	List<str> split(str delim, bool mode = true, bool rem = true); //���������� ������. mode = true - �� ���������, mode = false - �� �������� ���������; rem = ��������� �����������?
	
	bool contains(str sub);		//�������� �� ������ ��������� / ������?
	bool contains(std::string sub);
	bool contains(char* sub);
	bool contains(char sub);
	bool contains(const char sub[]);

	static str tostr(char); //�����������
	static str tostr(int);


	str operator +(str);		//����� ������� ������ ���������� ���������� + � +=, ������������ ��� ���������� ������ �� ��������� �������
	void operator +=(str);
	str operator +(std::string);
	void operator +=(std::string);
	str operator +(char*);
	void operator +=(char*);
	str operator +(const char[]);
	void operator +=(const char[]);
	str operator +(char);
	void operator +=(char);

	bool operator ==(str);		//������ ���������� ���������� == � !=, ����� ��, ��� ���������� ������ �� ��������� �������
	bool operator !=(str);
	bool operator ==(std::string);
	bool operator !=(std::string);
	bool operator ==(char*);
	bool operator !=(char*);
	bool operator ==(const char[]);
	bool operator !=(const char[]);

	//��������� ������������
	void operator =(std::string);
	void operator =(char*);
	void operator =(const char[]);
	str& operator =(const str&);
};

