// Problem #29 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

enum countingPattern { smallLetters = 1, capitalLetters = 2, all = 3 };

short countStringCapitalLetters(string text) {
	short counter = 0;

	for (int i = 0; i < text.length(); i++)
	{
		if (isupper(text[i]))
			++counter;
	}

	return counter;
}

short countStringSmallLetters(string text) {
	short counter = 0;

	for (int i = 0; i < text.length(); i++)
	{
		if (islower(text[i]))
			++counter;
	}

	return counter;
}

short countStringPattern(string text, countingPattern patternToCount = countingPattern::all) {
	switch (patternToCount)
	{
		case countingPattern::smallLetters:
			return countStringSmallLetters(text);
		case countingPattern::capitalLetters:
			return countStringCapitalLetters(text);
		case countingPattern::all:
			return text.length();
		default:
			return text.length();
	}
}

int main()
{
	string text = readString(" -> Please enter something: ");
	countingPattern patterToCount = (countingPattern)readPositiveInteger(1, 3, " -> Please enter what pattern to count: \n\t 1 -> small letters \n\t 2 -> capitall letters \n\t 3 -> all letters \n\t what to count? ");

	cout << " -> Counting result: " << countStringPattern(text, patterToCount) << endl;

}
