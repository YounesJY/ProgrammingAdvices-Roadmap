#pragma once

#include <iostream>
#include "DoubleLinkedList.h"

using namespace std;

template <class T>
class Queue
{
private:
	DoubleLinkedList<T> _list;

public:
	void push(T item)
	{
		this->_list.insertAtEnd((T)item);
	}

	int size()
	{
		return this->_list.size();
	}

	bool isEmpty() {
		return this->_list.isEmpty();
	}

	T front() {
		return this->_list.getItem(0);
	}

	T back() {
		return this->_list.getItem(_list.size() - 1);
	}

	void pop()
	{
		_list.deleteFirstNode();
	}

	void print() {
		_list.printList();
	}

	T getItem(int index) {
		return this->_list.getItem(index);
	}

	void reverse() {
		this->_list.reverse();
	}

	bool updateItem(int index, T newValue) {
		return this->_list.updateItem(index, newValue);
	}

	bool insertAfter(int index, T value) {
		return this->_list.insertAfter(index, value);
	}

	void insertAtFront(T value) {
		this->_list.insertAtBeginning(value);
	}

	void insertAtBack(T value) {
		this->_list.insertAtEnd(value);
	}

	void clear() {
		this->_list.clear();
	}
};
