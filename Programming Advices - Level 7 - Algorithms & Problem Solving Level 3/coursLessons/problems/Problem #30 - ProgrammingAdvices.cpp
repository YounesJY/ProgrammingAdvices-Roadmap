// Problem #30 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

short countLetter(string text, char letterToCount) {

	short counter = 0;

	for (int i = 0; i < text.length(); i++)
	{
		if (text[i] == letterToCount)
			++counter;
	}

	return counter;
}

int main()
{
	string text = readString(" -> Please enter something: ");
	char letterToCount = readString(" -> Please enter the caracter you want to count: ")[0];

	
	cout << " -> Letter \"" << letterToCount << "\" count = " << countLetter(text, letterToCount) << endl;
}

