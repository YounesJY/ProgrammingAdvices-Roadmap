// Problem #40 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";
#include "../MyArrayLibrary.h";

using namespace std;
using namespace input;
using namespace array;


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

void printStringWords(vector<string>& words) {
	for (const string& word : words)
		cout << word << endl;
}

string joinStringWords(vector<string> textWords, string delimiter = " ") {
	string fullText = "";

	for (const string& word : textWords)
		fullText += (word + delimiter);

	return fullText.substr(0, fullText.size() - delimiter.size());
	//return fullText.erase(fullText.size() - delimiter.size(), delimiter.size());
}

string joinStringWords(string words[], int size, string delimiter = " ") {
	string fullText = "";

	for (int i = 0; i < size; i++)
		fullText += (words[i] + delimiter);

	return fullText.substr(0, fullText.size() - delimiter.size());
	//return fullText.erase(fullText.size() - delimiter.size(), delimiter.size());
}


int main()
{
	string text, separator;
	string words[] = { "hello", "there", "you" };
	vector<string> myTextWords;

	while (true)
	{
		text = readString(" -> Please enter something: ");
		separator = readString(" -> Please enter a sparator: ");
		myTextWords = splitStringWords(text, separator);

		cout << " -> Your text contains [ " << myTextWords.size() << " token(s) ]: " << endl;
		printStringWords(myTextWords);

		separator = readString(" -> Please enter a sparator to join your text words: ");
		cout << " -> Your new text is: [ " << joinStringWords(myTextWords, separator) << " ]: " << endl;

		myTextWords.clear();
		cout << endl << endl;
	}

	//while (true)
	//{


	//	separator = readString(" -> Please enter a sparator to join your text words: ");
	//	cout << " -> Your new text is: [ " << joinStringWords(words, 3 , separator) << " ]" << endl;

	//	cout << endl << endl;
	//}
}