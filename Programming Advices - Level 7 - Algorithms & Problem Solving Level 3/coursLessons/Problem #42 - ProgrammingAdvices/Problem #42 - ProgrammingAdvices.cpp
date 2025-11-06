// Problem #42 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

string stringToLower(string text) {
	for (int i = 0; i < text.length(); i++)
		text[i] = tolower(text[i]);

	return text;
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

vector<string> splitStringWords(string text, string delimiter = " ", bool matchCase = true) {
	short position = 0;
	vector<string> textWords;

	if (matchCase)
	{
		while ((position = text.find(delimiter)) != string::npos)
		{
			string word = "";

			word = text.substr(0, position);

			if (!word.empty())
				textWords.push_back(word);

			text.erase(0, position + delimiter.size());
		}
	}
	else
	{
		delimiter = stringToLower(delimiter);
		while ((position = stringToLower(text).find(delimiter)) != string::npos)
		{
			string word = "";

			word = text.substr(0, position);

			if (!word.empty())
				textWords.push_back(word);

			text.erase(0, position + delimiter.size());

		}
	}

	if (!text.empty())
		textWords.push_back(text);

	return textWords;
}

string replaceStringWords(string text, string oldWord, string newWord, bool matchCase = true, bool usingBuiltInFunction = true) {
	//Teacher Method ! built-in functio is the best method
	if (usingBuiltInFunction)
	{
		int wordToReplacePosition = 0;
		if (matchCase)
		{
			while ((wordToReplacePosition = text.find(oldWord)) != string::npos)
				text.replace(wordToReplacePosition, oldWord.length(), newWord);
		}
		else
		{
			oldWord = stringToLower(oldWord);

			while ((wordToReplacePosition = stringToLower(text).find(oldWord)) != string::npos)
				text.replace(wordToReplacePosition, oldWord.length(), newWord);
		}

		return text;
	}
	else
	{
		if (false)
		{
			// Method ! by JY and teacher !! Only support spliting by space charachter
			vector<string> textWords = splitStringWords(text);

			for (string& word : textWords) {
				if (matchCase)
				{
					if (word == oldWord)
						word = newWord;
				}
				else
				{
					if (stringToLower(word) == stringToLower(oldWord))
						word = newWord;

				}
			}

			return joinStringWords(textWords);
			//return updatedText.substr(0, updatedText.size() - 1);
		}
		else
		{
			// Method 2 by JY --> works just like built-in function 
			if (matchCase)
				return joinStringWords(splitStringWords(text, oldWord), newWord);
			else
				return joinStringWords(splitStringWords(text, oldWord, false), newWord);
		}
	}
}

string replaceStringWordsJY(string text, string wordToReplace, string replaceToWord, bool matchCase = true) {
	string updatedText = "", textCopy = stringToLower(text);
	short wordBegin = 0, wordEnd = wordToReplace.length() - 1;
	short i = 0, j = 0;

	if (matchCase)
	{
		if (text.find(wordToReplace) == string::npos)
			return text;

		while (text.find(wordToReplace) != string::npos)
		{
			if (text[i] == wordToReplace[0])
			{
				j = 0;
				wordBegin = i;
				while (text[i] == wordToReplace[j])
				{
					if (j == wordEnd)
					{
						updatedText += text.substr(0, wordBegin) + replaceToWord;
						text.erase(0, wordBegin + wordToReplace.length());
						j = 0;
						i = -1;
						break;
					}
					i++;
					j++;
				}
			}
			i++;
		}
	}
	else
	{
		wordToReplace = stringToLower(wordToReplace);
		if (textCopy.find(wordToReplace) == string::npos)
			return text;

		while (textCopy.find(wordToReplace) != string::npos)
		{
			if (textCopy[i] == wordToReplace[0])
			{
				wordBegin = i;
				while (textCopy[i] == wordToReplace[j])
				{
					if (j == wordEnd)
					{
						updatedText += text.substr(0, wordBegin) + replaceToWord;
						text.erase(0, wordBegin + wordToReplace.length());
						textCopy.erase(0, wordBegin + wordToReplace.length());
						j = 0;
						i = -1;
						break;
					}
					i++;
					j++;
				}
			}
			i++;
		}
	}

	if (!text.empty())
		updatedText += text;
	return updatedText;
}

int main()
{
	string text, oldWord, newWord;
	while (true)
	{
		text = readString(" -> Please enter something: ");
		oldWord = readString(" -> Please enter a word to replace: ");
		newWord = readString(" -> Please enter a new  word to replace it: ");

		cout << " -> your original text: [ " << text << " ]" << endl;


		//cout << " -> your text after replacing words: [ " << replaceStringWords(text, oldWord, newWord) << " ]" << endl;
		cout << " -> your text after replacing words: [ " << replaceStringWords(text, oldWord, newWord,1,false) << " ]" << endl;
		//cout << " -> your text after replacing words: [ " << replaceStringWordsJY(text, oldWord, newWord, 1) << " ]" << endl;

		cout << endl << endl;
	}
}



