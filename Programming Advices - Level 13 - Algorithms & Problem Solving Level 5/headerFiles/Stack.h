#pragma once

#include <iostream>
#include "Queue.h"

using namespace std;

//===== Queue-Based Stack //===== 
// Best version: Reusability : DRY + Inheritence + Maintenance
template <class T>
class Stack : public Queue<T> {
public:
	void push(T item) {
		Queue<T>::insertAtFront((T)item);
	}

	T top() {
		return Queue<T>::front();
	}

	T bottom() {
		return Queue<T>::back();
	}
};

//===== LinkedList-Based Stack //===== 
//
// 
//template <class T>
//class Stack
//{
//private:
//	DoubleLinkedList<T> _list;
//
//public:
//	void push(T item)
//	{
//		this->_list.insertAtBeginning((T)item);
//	}
//
//	int size()
//	{
//		return this->_list.size();
//	}
//
//	T top() {
//		return this->_list.getItem(0);
//	}
//
//	T bottom() {
//		return this->_list.getItem(_list.size() - 1);
//	}
//
//	void pop()
//	{
//		_list.deleteFirstNode();
//	}
//
//	void print() {
//		_list.printList();
//	}
//
//	T getItem(int index) {
//		return this->_list.getItem(index);
//	}
//
//	void reverse() {
//		this->_list.reverse();
//	}
//
//	bool updateItem(int index, T newValue) {
//		return this->_list.updateItem(index, newValue);
//	}
//
//	bool insertAfter(int index, T value) {
//		return this->_list.insertAfter(index, value);
//	}
//
//	void insertAtFront(T value) {
//		this->_list.insertAtBeginning(value);
//	}
//
//	void insertAtBack(T value) {
//		this->_list.insertAtEnd(value);
//	}
//
//	void clear() {
//		this->_list.clear();
//	}
//};
//
