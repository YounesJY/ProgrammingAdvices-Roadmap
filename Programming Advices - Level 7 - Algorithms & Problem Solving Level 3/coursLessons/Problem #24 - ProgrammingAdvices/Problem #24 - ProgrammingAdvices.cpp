// Problem #24 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

bool isUpperLetter(char letter) {
	return (letter >= 'A' && letter <= 'Z');
}

char toUpperLetter(char letter) {
	if (isUpperLetter(letter))
		return letter;
	return (char)(letter - ('a' - 'A'));
}

string upperFirstLetterOfStringWords(string text) {
	bool isFirstLetter = true;
	char nextChar;
	cout << " -> This is your original string: << " << text << " >>" << endl;
	for (int i = 0; i < text.size(); i++)
	{
		if (text[i] != ' ' && isFirstLetter)
			text[i] = toUpperLetter(text[i]);
		while (i < text.size())
		{
			nextChar = text[i + 1];
			if (nextChar == ' ')
				++i;
			else
				break;
		}
		isFirstLetter = (text[i] == ' ');
	}
	return text;
}
int main()
{
	string text = readString(" -> Please enter something: ");
	cout << upperFirstLetterOfStringWords(text);
}
