// Problem #34 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

bool isVowel(char character) {
	char vowels[] = { 'a', 'e', 'i', 'o', 'u' };

	for (char vowel : vowels)
	{
		if (tolower(character) == vowel)
			return true;
	}

	return false;
}

void printStringVowels(string text) {
	for (int i = 0; i < text.length(); i++)
	{
		if (isVowel(text[i]))
			cout << "\t" << text[i];
	}
}

int main()
{
	while (true)
	{

		string text = readString(" -> Please enter something: ");
		cout << " -> Vowels in your text are: ";
		printStringVowels(text);
		cout << endl;
	}
}

