#pragma once
#include <iostream>
class str
{
private:
	const int MAXLEN = 10000; //������������ ����� ������, �� ������� ����� ���� ������ ������
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

	str operator +(str);		//����� ������� ������ ���������� ���������� + � +=, ������������ ��� ���������� ������ �� ��������� �������
	void operator +=(str);
	str operator +(std::string);
	void operator +=(std::string);
	str operator +(char*);
	void operator +=(char*);
	str operator +(const char[]);
	void operator +=(const char[]);

	bool operator ==(str);		//������ ���������� ���������� == � !=, ����� ��, ��� ���������� ������ �� ��������� �������
	bool operator !=(str);
	bool operator ==(std::string);
	bool operator !=(std::string);
	bool operator ==(char*);
	bool operator !=(char*);
	bool operator ==(const char[]);
	bool operator !=(const char[]);
};

