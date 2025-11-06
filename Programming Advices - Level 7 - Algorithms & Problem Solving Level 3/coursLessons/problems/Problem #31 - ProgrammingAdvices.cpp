// Problem #31 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

char invertLetterCase(char letter) {
	return ((islower(letter)) ? toupper(letter) : tolower(letter));
}

short countLetter(string text, char letterToCount, bool isCaseSensitive = true) {
	short counter = 0;

	if (isCaseSensitive)
	{
		for (int i = 0; i < text.length(); i++)
		{
			if (text[i] == letterToCount)
				++counter;
		}
	}
	else
	{
		for (int i = 0; i < text.length(); i++)
		{
			if (text[i] == letterToCount || text[i] == invertLetterCase(letterToCount))
				++counter;
			//if (tolower(text[i]) == tolower((letterToCount))
			//	++counter;
		}
	}

	return counter;
}

int main()
{
	string text = readString(" -> Please enter something: ");
	char letterToCount = readString(" -> Please enter the caracter you want to count: ")[0];


	cout << " -> Letter \"" << letterToCount << "\" count = " << countLetter(text, letterToCount) << endl;
	cout << " -> Letter \"" << letterToCount << "\" or \"" << invertLetterCase(letterToCount) << "\" count = " << countLetter(text, letterToCount, false) << endl;
}

