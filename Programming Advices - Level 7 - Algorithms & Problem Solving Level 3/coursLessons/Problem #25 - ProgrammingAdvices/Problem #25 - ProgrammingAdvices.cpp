// Problem #25 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

bool isLowerLetter(char letter) {
	return (letter >= 'a' && letter <= 'z');
}

char toLowerLetter(char letter) {
	if (isLowerLetter(letter))
		return letter;
	return (char)(letter + ('a' - 'A'));
}

string lowerFirstLetterOfStringWords(string text) {
	bool isFirstLetter = true;

	cout << " -> This is your original string: << " << text << " >>" << endl;
	for (int i = 0; i < text.size(); i++)
	{
		if (text[i] != ' ' && isFirstLetter)
			text[i] = toLowerLetter(text[i]);

		while (i < text.size())
		{
			if (text[i + 1] == ' ')
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
	cout <<  (text.size() == text.length())<< endl;
	cout << lowerFirstLetterOfStringWords(text);
}
