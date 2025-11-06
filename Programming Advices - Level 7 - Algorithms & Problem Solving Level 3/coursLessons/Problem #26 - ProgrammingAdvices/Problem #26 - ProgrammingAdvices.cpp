// Problem #26 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include "../MyOthersStuffLibrary.h";

using namespace std;
using namespace input;

string upperCaseAllString(string text) {
	for (int i = 0; i < text.size(); i++)
		text[i] = toupper(text[i]);
	return text;
}

string lowerCaseAllString(string text) {
	for (int i = 0; i < text.size(); i++)
		text[i] = tolower(text[i]);
	return text;
}
int main()
{
	string text = readString(" -> Please enter something: ");

	cout << " -> Your original text: <<" << text << ">>" << endl;
	cout << " -> Your text in lower case: <<" << lowerCaseAllString(text) << ">>" << endl;
	cout << " -> Your text in upper case: <<" << upperCaseAllString(text) << ">>" << endl;
}
