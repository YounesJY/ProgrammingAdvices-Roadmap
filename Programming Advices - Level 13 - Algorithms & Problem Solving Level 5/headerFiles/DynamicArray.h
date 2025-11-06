#pragma once

#include <iostream>
#include "DoubleLinkedList.h"

using namespace std;

//template <class T>
//class DynamicArray : public DoubleLinkedList<T> {
//public:
//	DynamicArray(int newSize) {
//		if (newSize <= 0)
//			return;
//
//		for (short i = 0; i < newSize; i++)
//			DoubleLinkedList<T>::insertAtEnd(new typename DoubleLinkedList<T>::Node::Node());
//	}
//
//	bool setItem(int index, T newValue) {
//		return DoubleLinkedList<T>::updateItem(index, newValue);
//	}
//
//};

template <class T>
class DynamicArray {
private:
	T* _array;
	int _size;

	void _swap(T& value1, T& value2) {
		T temp = value1;
		value1 = value2;
		value2 = temp;
	}

public:
	DynamicArray(int newSize = 0) {
		if (newSize <= 0) {
			_array = nullptr;
			_size = 0;
			return;
		}

		_array = new T[newSize];
		_size = newSize;
	}



	void printList() {
		for (int i = 0; i < _size; i++)
			cout << _array[i] << " ";
	}

	bool setItem(int index, T newValue) {
		if (index < 0 || index >= _size)
			return false;

		_array[index] = newValue;
		return true;
	}

	int size() {
		return this->_size;
	}

	bool isEmpty() {
		return (_size == 0);
	}

	void resize(int newSize) {
		if (newSize <= 0)
			return;

		T* tempArray = new T[newSize];

		// We only copy up to the smallest size to:
		// - avoid reading beyond the bounds of the old array (segmentation fault)
		// - avoid writing beyond the bounds of the new array
		// This safely handles both expanding and shrinking.

		int minSize = (newSize < _size) ? newSize : _size;

		for (int i = 0; i < minSize; i++)
			tempArray[i] = _array[i];

		delete[] _array;
		_array = tempArray;
		_size = newSize;
	}


	T getItem(int index) {
		if (index < 0 || index >= _size)
			exit(1);
		return _array[index];
	}

	void reverse() {
		int midOfArray = (_size / 2);

		for (int i = 0; i < midOfArray; i++)
			_swap(_array[i], _array[_size - 1 - i]);
	}

	T find(T value) {
		for (int i = 0; i < _size; i++) {
			if (_array[i] == value)
				return i;
		}

		return -1;
	}


	bool deleteItemAt(int index) {
		if (index < 0 || index >= _size)
			return false;

		T* tempArray = new T[_size - 1];

		//  Version 1: Single-loop (compact alternative)
		/*
		for (int i = 0, j = 0; i < _size; i++) {
			if (i != index) {
				tempArray[j] = _array[i];
				++j;
			}
		}
		*/

		//  Version 2: Two clear loops (adopted solution)
		//  This approach is often called the "copy before and copy after" or
		//  partitioned copy" strategy.It is clear, safe, and easy to understand.

		// Copy elements before the target index
		for (int i = 0; i < index; i++)
			tempArray[i] = _array[i];

		// Copy elements after the target index, shifted one position left
		for (int i = index + 1; i < _size; i++)
			tempArray[i - 1] = _array[i];

		delete[] _array;
		_array = tempArray;
		--_size;

		return true;
	}

	bool deleteItem(T value) {
		return deleteItemAt(find(value));
	}

	bool deleteFirstItem() {
		return deleteItemAt(0);
	}

	bool deleteLastItem() {
		return deleteItemAt(_size - 1);
	}


	void clear() {
		if (_array != nullptr) {
			delete[] _array;
			_array = nullptr;
			_size = 0;
		}
	}


	bool insertAt(int index, T value) {
		if (index < 0 || index > _size)
			return false;

		T* tempArray = new T[_size + 1];

		// Copy elements before the target index
		for (int i = 0; i < index; i++)
			tempArray[i] = _array[i];

		tempArray[index] = value;

		// Copy elements after the target index, shifted one position left
		for (int i = index; i < _size; i++)
			tempArray[i + 1] = _array[i];


		delete[] _array;
		_array = tempArray;
		++_size;

		return true;
	}

	bool insertAtBeginning(T value) {
		return insertAt(0, value);
	}

	bool insertAtEnd(T value) {
		return insertAt(_size, value);
	}

	bool insertBefore(int index, T value) {
		if (index <= 1)
			return insertAt(0, value);
		return insertAt(index - 1, value);
	}

	bool insertAfter(int index, T value) {
		if (index >= _size - 1)
			return insertAt(_size, value);
		return insertAt(index + 1, value);
	}

};