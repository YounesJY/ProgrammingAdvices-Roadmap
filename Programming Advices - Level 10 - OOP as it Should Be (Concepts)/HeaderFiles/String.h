#pragma once

#include <iostream>
#include <string>
#include <vector>

using namespace std;

class String {
private:
	string _value;

public:
	String(string value) {
		_value = value;
	}

	String() {
		String(string());
	}

	string getValue() { return _value; }

	void setValue(string value) { _value = value; }

	__declspec(property(get = getValue, put = setValue)) string value;


	static string stringToLower(string text) {
		for (int i = 0; i < text.length(); i++)
			text[i] = tolower(text[i]);

		return text;
	}

	string stringToLower() {
		return stringToLower(_value);
	}


	static string joinStringWords(vector<string> textWords, string delimiter = " ", bool joinInOrder = true) {
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


	static vector<string> splitStringWords(string text, string delimiter = " ", bool matchCase = true) {
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

	vector<string> splitStringWords(string delimiter = " ", bool matchCase = true) {
		return splitStringWords(_value, delimiter, matchCase);
	}


	static string replaceStringWords(string text, string oldWord, string newWord, bool matchCase = true, bool usingBuiltInFunction = true) {
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
			// Method 2 by JY --> works just like built-in function 
			string updatedText = "";

			if (matchCase)
			{
				updatedText = joinStringWords(splitStringWords(text, oldWord), newWord);
				if (text.substr(0, oldWord.length()) == oldWord)
					updatedText = newWord + updatedText;

				if (text.erase(0, text.length() - oldWord.length()) == oldWord)
					updatedText += newWord;
			}
			else
			{
				updatedText = joinStringWords(splitStringWords(text, oldWord, false), newWord);
				if (stringToLower(text.substr(0, oldWord.length())) == stringToLower(oldWord))
					updatedText = newWord + updatedText;

				if (stringToLower(text.erase(0, text.length() - oldWord.length())) == stringToLower(oldWord))
					updatedText += newWord;
			}

			return updatedText;
		}
	}

	string replaceStringWords(string oldWord, string newWord, bool matchCase = true, bool usingBuiltInFunction = true) {
		return replaceStringWords(_value, oldWord, newWord, matchCase, usingBuiltInFunction);
	}
};

