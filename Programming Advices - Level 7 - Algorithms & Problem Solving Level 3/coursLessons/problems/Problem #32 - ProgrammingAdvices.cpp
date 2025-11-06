// Problem #32 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

char invertLetterCase(char letter) {
	return ((islower(letter)) ? toupper(letter) : tolower(letter));
}


bool isVowel(char character) {
	char vowels[] = { 'a', 'e', 'i', 'o', 'u'};

		for (char vowel : vowels)
		{
			if (tolower(character) == vowel)
				return true;
		}

	return false;
}

int main()
{
	while (true)
	{

	char character = readCharacter(" -> Please enter the caracter you want: ");
	cout << " -> Letter \"" << character << "\" " << ((isVowel(character)) ? "is a vowel!" : " isn't a vowel!") << endl;
	}
}

