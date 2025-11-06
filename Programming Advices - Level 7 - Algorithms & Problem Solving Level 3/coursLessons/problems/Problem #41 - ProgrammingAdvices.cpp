// Problem #41 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

vector<string> splitStringWords(string text, string delimiter = " ") {
	short position = 0;
	string word = "";
	vector<string> textWords;

	while ((position = text.find(delimiter)) != string::npos)
	{
		word = text.substr(0, position);

		if (!word.empty())
			textWords.push_back(word);

		text.erase(0, position + delimiter.size());
	}

	if (!text.empty())
		textWords.push_back(text);

	return textWords;
}

void printStringWordsInOrder(vector<string>& words) {
	for (const string& word : words)
		cout << word << endl;
}

void printStringWordsInReversedOrder(vector<string>& words) {
	for (int i = (words.size() - 1); i >= 0; i--)
		cout << words.at(i) << endl;
}

void printStringWords(vector<string>& words, bool printInOrder = true) {
	if (printInOrder)
		printStringWordsInOrder(words);
	else
		printStringWordsInReversedOrder(words);
}

string joinStringWords(vector<string> textWords, string delimiter = " ", bool joinInOrder = true) {
	string fullText = "";

	if (joinInOrder)
	{
		for (const string& word : textWords)
			fullText += (word + delimiter);
	}
	else
	{
		vector<string>::iterator iter = textWords.end();
		while (iter != textWords.begin())
		{
			--iter;
			fullText += (*iter + delimiter);
		}
	}

	return fullText.substr(0, fullText.size() - delimiter.size());
	//return fullText.erase(fullText.size() - delimiter.size(), delimiter.size());
}

string reverseStringWords(string text, string delimiter = " ") {
	return joinStringWords(splitStringWords(text, delimiter), delimiter, false);
}

int main()
{
	string text, separator;
	vector<string> myTextWords;
	vector<string>::iterator iter;
	while (true)
	{
		text = readString(" -> Please enter something: ");
		separator = readString(" -> Please enter a sparator: ");
		myTextWords = splitStringWords(text, separator);

		cout << " -> Your text contains [ " << myTextWords.size() << " token(s) ]: " << endl;
		printStringWords(myTextWords);
		separator = readString(" -> please enter a sparator to join your text words: ");
		cout << " -> your new text after reversing is: [ " << reverseStringWords(text, separator) << " ]: " << endl;
	
		myTextWords.clear();
		cout << endl << endl;
	}
}