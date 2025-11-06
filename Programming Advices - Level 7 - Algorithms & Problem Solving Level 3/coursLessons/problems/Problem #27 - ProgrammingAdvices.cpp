// Problem #27 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

using namespace std;

char invertLetterCase(char letter) {
	return ((islower(letter)) ? toupper(letter) : tolower(letter));
}

int main()
{
	cout << invertLetterCase('y');
	cout << invertLetterCase('j') << endl;
}
