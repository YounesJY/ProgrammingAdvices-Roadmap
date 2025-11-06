// Problem #23 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <vector>

using namespace std;

string getStringLastWord(string text) {
	for (int i = (text.size() - 1); i >= 0; i--)
	{
		if (text[i] == ' ')
			return text.substr(i + 1, (text.size() - 1) - (i + 1) + 1);
	}
}

void getStringWords(string text, vector<string>& words) {

	for (int i = 0, wordFront = 0; i < text.size(); i++)
	{
		if (text[i] == ' ') {
			words.push_back(text.substr(wordFront, i - wordFront));
			while (i < text.size())
			{
				++i;
				if (text[i] == ' ')
					++i;
				else
					break;
			}
			wordFront = i;
		}
	}
	words.push_back(getStringLastWord(text));
}

void printStringWords(vector<string>& words) {
	for (const string& word : words)
		cout << word << "--> " << word.size() << endl;
}

void printWordFistLetter(string word) {
	cout << word[0] << endl;
}

void printFirstLetterOfStringWords(string text) {
	bool isFirstLetter = true;

	cout << " -> This is your original string: << " << text << " >>" << endl;
	vector<string> words;

	//getStringWords(text, words);
	//cout << " -> This is your string words: " << endl;
	//printStringWords(words);

	cout << " -> This is the first letters of each word in your string: " << endl;
	for (int i = 0; i < text.size(); i++)
	{
		if (text[i] != ' ' && isFirstLetter)
			cout << text[i] << endl;
		while (i < text.size())
		{
			if (text[i + 1] == ' ')
				++i;
			else
				break;
		}
		isFirstLetter = (text[i] == ' ');
	}
}

int main()
{
	printFirstLetterOfStringWords("Hi  there, I'm trying a new algorithm.  '  ; ");
}

