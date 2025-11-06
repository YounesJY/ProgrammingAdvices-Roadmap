#pragma once

#include <iostream>

using namespace std;

template <class T>
class DoubleLinkedList {
public:
	class Node {
	public:
		T _value;
		Node* _next;
		Node* _prev;

		Node(T value, Node* next, Node* prev) {
			this->_value = value;
			this->_next = next;
			this->_prev = prev;
		}

		Node(T value) : Node(value, nullptr, nullptr) {}

		Node() {}

	};

private:
	int _size = 0;

	void _swap(Node* node) {
		Node* temp = node->_next;
		node->_next = node->_prev;
		node->_prev = temp;
	}

public:
	Node* head = nullptr;

	DoubleLinkedList() {
		this->head = nullptr;
	}

	void printList() {
		Node* temp = head;

		while (temp != nullptr) {
			cout << temp->_value << " ";
			temp = temp->_next;
		}
	}

	Node* find(T targetValue) {
		Node* temp = head;

		while (temp != nullptr) {
			if (temp->_value == targetValue)
				return temp;
			temp = temp->_next;
		}

		return nullptr;
	}


	void insertAtBeginning(Node* newNode) {
		++_size;

		newNode->_next = head;
		newNode->_prev = nullptr;

		if (head != nullptr)
			head->_prev = newNode;
		head = newNode;

	}

	void insertAtBeginning(T value) {
		this->insertAtBeginning(new Node(value));
	}

	void insertAtEnd(Node* newNode) {
		++_size;

		newNode->_next = nullptr;
		newNode->_prev = nullptr;

		if (head == nullptr) {
			head = newNode;
			return;
		}

		Node* temp = head;
		while (temp->_next != nullptr)
			temp = temp->_next;
		temp->_next = newNode;
		newNode->_prev = temp;
	}

	void insertAtEnd(T value) {
		this->insertAtEnd(new Node(value));
	}


	bool insertAfter(Node* targetNode, Node* newNode) {
		if (targetNode == nullptr)
			return false;

		++_size;

		newNode->_next = targetNode->_next;
		newNode->_prev = targetNode;

		if (targetNode->_next != nullptr)
			targetNode->_next->_prev = newNode;
		targetNode->_next = newNode;

		return true;
	}

	bool insertAfter(int index, T value) {
		DoubleLinkedList<T>::Node* node = getNode(index);

		if (node == nullptr)
			return false;

		this->insertAfter(node, new Node(value));
		return  true;
	}


	void deleteFirstNode() {
		if (head == nullptr)
			return;

		--_size;

		Node* toDelete = head;
		head = head->_next;
		if (head != nullptr)
			head->_prev = nullptr;
		delete toDelete;
	}

	void deleteLastNode() {
		if (head == nullptr)
			return;

		--_size;

		if (head->_next == nullptr) {
			delete head;
			head = nullptr;
			return;
		}

		Node* temp = head;
		while (temp->_next != nullptr)
			temp = temp->_next;
		temp->_prev->_next = nullptr;
		delete temp;
	}

	void deleteNode(T targetValue) {
		Node* targetNode = find(targetValue);
		if (!targetNode)
			return;

		--_size;

		if (targetNode->_prev != nullptr)
			targetNode->_prev->_next = targetNode->_next;
		else
			head = targetNode->_next;
		if (targetNode->_next != nullptr)
			targetNode->_next->_prev = targetNode->_prev;
		delete targetNode;
	}


	void clear() {
		// Version 2 (Preferred): Reuses deleteFirstNode() for safety & maintainability
		while (_size > 0) {
			deleteFirstNode();
		}

		/*
		// Version 1 (Faster but duplicates logic):
		void clear() {
			while (head != nullptr) {
				Node* next = head->_next;
				delete head;
				head = next;
			}
			_size = 0;
		}
		*/
	}

	void reverse() {
		if (size() < 1)
			return;

		Node* current = head;

		while (current != nullptr) {
			head = current;
			current = current->_next;
			_swap(head);
		}
	}


	int size() {
		return _size;
	}

	bool isEmpty() {
		return (_size == 0);
	}


	Node* getNode(int index) {
		if (_size == 0 || index >= _size || index < 0)
			return nullptr;

		Node* current = head;
		int i = 0;

		while (current != nullptr) {
			if (i == index)
				return current;

			current = current->_next;
			++i;
		}

		return nullptr;
	}

	T getItem(int index) {
		DoubleLinkedList<T>::Node* node = getNode(index);

		if (node == NULL)
			exit(1);

		return node->_value;

		//	return  ((node == nullptr) ? NULL : node->_value);
		// this must not be used, because returning NULL for non found value resulting in 0,
		// so 0 is the actual value or the casted NULL (for numeric values)? (ambiguity)
	}


	bool updateItem(int index, T newValue) {
		DoubleLinkedList<T>::Node* node = getNode(index);
		if (node == nullptr)
			return false;

		node->_value = newValue;
		return true;
	}


	~DoubleLinkedList() {
		clear();
	}
};
