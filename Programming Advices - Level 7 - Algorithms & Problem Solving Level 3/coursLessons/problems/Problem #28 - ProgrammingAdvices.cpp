// Problem #28 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

char invertLetterCase(char letter) {
	return ((islower(letter)) ? toupper(letter) : tolower(letter));
}

string invertAllStringLettersCase(string text) {
	for (int i = 0; i < text.size(); i++)
		text[i] = invertLetterCase(text[i]);
	return text;
}

int main()
{
	string text = readString(" -> Please enter a string: ");

	cout << " -> Your original text: <<" << text << ">>" << endl;
	cout << " -> Your text in with inverted case letters: <<" << invertAllStringLettersCase(text) << ">>" << endl;

}
