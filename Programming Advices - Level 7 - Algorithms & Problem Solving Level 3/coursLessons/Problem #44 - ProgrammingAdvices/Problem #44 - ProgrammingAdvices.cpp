// Problem #44 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

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

string removeStringPunctuations(string text) {
	for (int i = 0; i < text.length(); i++)
	{
		if (ispunct(text[i]))
			text.erase(i, 1);
		//if (!ispunct(text[i]))
		//	textWithoutPuncs+= text[i];
	}

	return text;
	//return textWithoutPuncs;
}

int main()
{
	string text;
	while (true)
	{
		text = readString(" -> Please enter something: ");

		cout << " -> your original text: [ " << text << " ]" << endl;
		cout << " -> your text after removing punctuations: [ " << removeStringPunctuations(text) << " ]" << endl;

		cout << endl << endl;
	}
}
