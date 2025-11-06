// Problem #38 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

string trimLeft(string text, char delimiter = ' ') {
	//   jy   
	for (int i = 0; i < text.size(); i++)
	{
		if (text[i] != delimiter)
		{
			return text.substr(i, text.size() - i);
		}
	}
	return "";
}

string trimRight(string text, char delimiter = ' ') {
	for (int i = (text.size() - 1); i >= 0; i--)
	{
		if (text[i] != delimiter)
		{
			return text.substr(0, i + 1);
		}
	}
	return "";
}

string trimAll(string text, char delimiter = ' ') {
	return trimRight(trimLeft(text, delimiter), delimiter);
}

int main()
{
	string text, trimedString;
	char separator;

	while (true)
	{
		text = readString(" -> Please enter something: ");
		//separator = readCharacter(" -> Please enter a sparator: ");

		trimedString = trimRight(text);
		cout << " -> Your original text: [" << text << "] --> [size = " << text.size() << "]" << endl;
		cout << " -> Your string after triming it from right: [" << trimedString << "] --> [size = " << trimedString.size() << "]" << endl;
		trimedString = trimLeft(text);
		cout << " -> Your string after triming it from left : [" << trimedString << "] --> [size = " << trimedString.size() << "]" << endl;
		trimedString = trimAll(text);
		cout << " -> Your string after triming it totaly : [" << trimedString << "] --> [size = " << trimedString.size() << "]" << endl;

		cout << endl << endl;
	}
}
