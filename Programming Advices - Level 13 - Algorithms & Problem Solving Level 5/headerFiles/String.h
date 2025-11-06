#pragma once

#include <iostream>
#include "Stack.h"

using namespace std;

/*
	===============================================
	Undo/Redo-Enabled String Class (Design Summary)
	===============================================

	 Purpose:
	Provides a string wrapper with undo and redo capabilities, useful for implementing text editors, form inputs, etc.

	 Design Rationale:
	- `_currentValue` stores the current state of the string.
	- `_undoStack` keeps track of previous string states (history).
	- `_redoStack` stores states that were undone and can be restored.

	 Why this structure?
	This design clearly separates:
	- the *current* value (held in `_currentValue`)
	- the *undo history* (held in `_undoStack`)
	- the *redo path* (held in `_redoStack`)

	This avoids the confusion of treating the current value as part of the history,
	and makes undo/redo logic simpler and safer.

	 Key Rule:
	Setting a new value clears the redo stack — just like in real applications.
	Once you change something new after an undo, the redo path becomes invalid.
*/

class String {
private:
	string _currentValue;          // Holds the current string state
	Stack<string> _undoStack;      // Holds history of previous values
	Stack<string> _redoStack;      // Holds values that were undone

public:
	// Constructor with initial value
	String(string value) {
		this->_currentValue = value;
	}

	// Default constructor initializes with empty string
	String() : String("") {}

	// Getter for the current value
	string getValue() {
		return this->_currentValue;
	}

	// Setter that pushes the old value to undo stack,
	// updates the value, and clears the redo history
	void setValue(string value) {
		_undoStack.push(_currentValue);  // Save current state before change
		_currentValue = value;           // Apply new value
		_redoStack.clear();              // Invalidate redo path
	}

	// Allows `myString.value = "newValue"` syntax in Visual Studio
	__declspec(property(get = getValue, put = setValue)) string value;

	// Undo operation: restores previous value and stores current in redo stack
	void undo() {
		if (!_undoStack.isEmpty()) {
			_redoStack.push(_currentValue);          // Save current to redo
			_currentValue = _undoStack.top();        // Restore previous value
			_undoStack.pop();                         // Remove from undo history
		}
	}

	// Redo operation: restores the last undone value and adds current to undo
	void redo() {
		if (!_redoStack.isEmpty()) {
			_undoStack.push(_currentValue);          // Save current to undo
			_currentValue = _redoStack.top();        // Restore last undone value
			_redoStack.pop();                         // Remove from redo history
		}
	}
};


//class String {
//private:
//	string _currentValue;
//	Stack<string> _undoStack;
//	Stack<string> _redoStack;
//public:
//	String(string value) {
//		this->_currentValue = value;
//		this->_undoStack.push(this->_currentValue);
//	}
//
//	String() : String("") {}
//
//	string getValue() {
//		return this->_currentValue;
//	}
//
//	void setValue(string value) {
//		this->_currentValue = value;
//		this->_undoStack.push(this->_currentValue);
//		this->_redoStack.clear();
//	}
//
//	__declspec(property(get = getValue, put = setValue)) string value;
//
//
//	void undo() {
//		if (_undoStack.size() > 1) {
//			_redoStack.push(_undoStack.top());
//
//			_undoStack.pop();
//			this->_currentValue = _undoStack.top();
//		}
//	}
//
//	void redo() {
//		if (!_redoStack.isEmpty()) {
//			string lastRedo = _redoStack.top();
//
//			this->_currentValue = lastRedo;
//			_undoStack.push(lastRedo);
//
//			_redoStack.pop();
//		}
//	}
//};