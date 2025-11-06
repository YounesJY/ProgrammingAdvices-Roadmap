#pragma once

#include <iostream>
#include "DynamicArray.h"

using namespace std;

/*
	Design Note: Why Composition is used instead of Inheritance

	This QueueArray class wraps around DynamicArray using composition
	rather than public inheritance for the following reasons:

	1. **Encapsulation and Abstraction**:
	   - Public inheritance would expose all public methods of DynamicArray,
		 including operations that break the FIFO (queue) abstraction,
		 such as insertAt(index), reverse(), or getItem(index).
	   - This leads to a confusing and redundant interface where the queue
		 behaves like a general-purpose array.

	2. **Protected or Private Inheritance**:
	   - Using protected/private inheritance hides the base class's public API,
		 but still forces you to manually expose needed methods (e.g., size(), clear()).
	   - This results in the same amount of work as composition,
		 but with less flexibility and more coupling.

	3. **Composition Advantage**:
	   - Composition allows QueueArray to fully control which methods of DynamicArray
		 are exposed, enforcing a strict queue interface (push, pop, front, back, etc.).
	   - This makes the design cleaner, more maintainable, and better aligned with
		 object-oriented principles like "favor composition over inheritance."

	Summary:
	   In this context, inheritance provides no real benefit,
	   while composition ensures abstraction, control, and extensibility.
*/


////==== Inheretnece-Based
//template <class T>
//class QueueArray : private DynamicArray<T> {
//public:
//	void print() {
//		_array.printList();
//	}
//
//	void push(T item)
//	{
//		_array.insertAtEnd(item);
//	}
//
//
//	T front() {
//		return _array.getItem(0);
//	}
//
//	T back() {
//		return _array.getItem(_array.size() - 1);
//	}
//
//
//	int size()
//	{
//		return _array.size();
//	}
//
//	T getItem(int index) {
//		return _array.getItem(index);
//	}
//
//	void reverse() {
//		_array.reverse();
//	}
//
//	bool updateItem(int index, T newValue) {
//		return _array.setItem(index, newValue);
//	}
//
//
//	bool insertAfter(int index, T value) {
//		return _array.insertAfter(index, value);
//	}
//
//	void insertAtFront(T value) {
//		_array.insertAtBeginning(value);
//	}
//
//	void insertAtBack(T value) {
//		_array.insertAtEnd(value);
//	}
//
//	void pop()
//	{
//		_array.deleteFirstItem();
//	}
//
//	void clear() {
//		_array.clear();
//	}
//};
//


// ==== Composition-Based
template <class T>
class QueueArray {
private:
	DynamicArray<T> _array;
public:
	void push(T item) {
		_array.insertAtEnd(item);
	}

	int size() {
		return _array.size();
	}

	T front() {
		return _array.getItem(0);
	}

	T back() {
		return _array.getItem(_array.size() - 1);
	}

	void pop() {
		_array.deleteFirstItem();
	}

	void print() {
		_array.printList();
	}

	T getItem(int index) {
		return _array.getItem(index);
	}


	void reverse() {
		_array.reverse();
	}

	bool updateItem(int index, T newValue) {
		return _array.setItem(index, newValue);
	}

	bool isEmpty() {
		return _array.isEmpty();
	}

	bool insertAfter(int index, T value) {
		return _array.insertAfter(index, value);
	}

	void insertAtFront(T value) {
		_array.insertAtBeginning(value);
	}

	void insertAtBack(T value) {
		_array.insertAtEnd(value);
	}


	void clear() {
		_array.clear();
	}
};
