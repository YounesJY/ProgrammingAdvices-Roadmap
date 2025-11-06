// Problem #36 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

void getStringWords(string text, string delimiter = " ") {
	short position = 0;
	string word = "";

	while ((position = text.find(delimiter)) != string::npos)
	{
		word = text.substr(0, position);

		if (!word.empty())
			cout << word << endl;

		text.erase(0, position + delimiter.size());
	}

	if (!text.empty())
		cout << text << endl;
}

short countStringWords(string text, string delimiter = " ") {
	short position = 0, counter = 0;
	string word = "";

	while ((position = text.find(delimiter)) != string::npos)
	{
		word = text.substr(0, position);

		if (!word.empty())
			++counter;

		text.erase(0, position + delimiter.size());
	}

	if (!text.empty())
			++counter;

	return counter;
}

void printStringWords(vector<string>& words) {
	for (const string& word : words)
		cout << word << endl;
}

int main()
{
	string text;
	vector<string> myTextWords;

	while (true)
	{

		text = readString(" -> Please enter something: ");

		cout << "--> The number of words in your text is: " << countStringWords(text) << endl;
		cout << " -> Your text words are: " << endl;
		getStringWords(text);

		myTextWords.clear();
		cout << endl << endl;
	}
}