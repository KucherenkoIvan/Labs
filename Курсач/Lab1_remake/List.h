#pragma once
#include <iostream>
template<typename T>
class List
{
private:
	int count_;
public:
	T value;
	List<T>* next;
	List() //деф. конструктор
	{
		count_ = 0;
		next = NULL;
	}
	List(T value) //конструктор с параметром
	{
		count_ = 1;
		next = NULL;
		this->value = value;
	}
	int count() //количество элементов в списке
	{
		return count_;//this->next == NULL ? 1 : this->next->count() + 1;
	}
	void add(T val) //добавление
	{
		if (count_ == 0)
		{
			this->value = val;
			count_++;
			return;
		}
		List<T>* last = this;
		while (last->next != NULL)
			last = last->next;
		last->next = new List<T>(val);
		count_++;
	}
	int indexOf(T val) //поиск
	{
		int index = 0;
		List<T>* last = this;
		while (last->next != NULL && last->value != val)
		{
			last = last->next;
			index++;
		}
		return last->value == val ? index : -1;
	}
	void removeAt(int index) //удаление
	{
		if (index < 0 && index > count - 1) return;
		List<T>* last = this;
		List<T>* prev;
		while (last->next != NULL && index-- != 0)
			last = (prev = last)->next;
		prev->next = last->next;
		delete last;
	}
	void remove(T val) //удаление
	{
		removeAt(indexOf(val));
	}
	List<T>* at(int index) //доступ
	{
		if (index < 0 || index > count_ - 1) return NULL;
		List<T>* curr = this;
		while (index-- > 0)
			curr = curr->next;
		return curr;
	}
};

