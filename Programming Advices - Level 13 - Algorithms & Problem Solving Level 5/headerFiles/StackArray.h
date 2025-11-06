#pragma once

#include <iostream>
#include "QueueArray.h"

using namespace std;

//===== Queue-Based Stack //===== 
// Best version: Reusability : DRY + Inheritence + Maintenance
template <class T>
class StackArray : public QueueArray<T> {
public:
	void push(T item) {
		QueueArray<T>::insertAtFront((T)item);
	}

	T top() {
		return QueueArray<T>::front();
	}

	T bottom() {
		return QueueArray<T>::back();
	}
};
