// Project #07 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
// ProgrammingAdvices.com
// Mohammed Abu-Hadhoud

#include <iostream>
#include "../../headerFiles/String.h"

using namespace std;

int main()
{
	cout << "\n\n\t\t\t\t\t\t Undo/Redo Project\n\n";

	String string1;

	cout << "\nInitial state:";
	cout << "\nString1 = " << string1.value << "\n";

	// Make some changes
	string1.value = "Mohammed";
	string1.value = "Mohammed2";
	string1.value = "Mohammed3";
	string1.value = "Mohammed4";

	cout << "\nAfter changes:";
	cout << "\nString1 = " << string1.value << "\n";

	// Undo multiple times
	cout << "\n\nUndo Operations:";
	cout << "\n_________________\n";

	string1.undo(); cout << "After undo: " << string1.value << "\n";
	string1.undo(); cout << "After undo: " << string1.value << "\n";
	string1.undo(); cout << "After undo: " << string1.value << "\n";

	// Redo once
	cout << "\n\nRedo Operation:";
	cout << "\n_________________\n";

	string1.redo(); cout << "After redo: " << string1.value << "\n";

	// New change after undo (this should clear the redo stack)
	cout << "\n\nNew Change After Undo (Break Redo Chain):";
	cout << "\n_________________\n";

	string1.value = "Mohammed_New";  // should clear redo stack
	cout << "New value set: " << string1.value << "\n";

	// Attempt redo again — should do nothing
	cout << "\nAttempting Redo After New Change:";
	cout << "\n_________________\n";

	string1.redo(); cout << "After redo: " << string1.value << " (should not change)\n";
	string1.redo(); cout << "After redo: " << string1.value << " (should not change)\n";

	system("pause>0");
	return 0;
}
