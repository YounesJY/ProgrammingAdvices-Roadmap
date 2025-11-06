// ProgrammingAdvices.com
// Mohammed Abu-Hadhoud

#pragma once

#include <iostream>
#include <vector>

using namespace std;

class StringFullEdition {
private:
	string _value;

public:
	StringFullEdition() {
		_value = "";
	}

	StringFullEdition(string value) {
		_value = value;
	}

	void setValue(string value) {
		_value = value;
	}

	string getValue() {
		return _value;
	}

	__declspec(property(get = getValue, put = setValue)) string value;

	static short length(string s1) {
		return s1.length();
	}

	short length() {
		return _value.length();
	}

	static short countWords(string s1) {
		string delim = " "; // delimiter
		short counter = 0;
		short pos = 0;
		string word; // define a string variable

		// use find() function to get the position of the delimiters
		while ((pos = s1.find(delim)) != string::npos) {
			word = s1.substr(0, pos); // store the word
			if (!word.empty()) {
				counter++;
			}

			// erase() until position and move to next word
			s1.erase(0, pos + delim.length());
		}

		if (!s1.empty()) {
			counter++; // it counts the last word of the string
		}

		return counter;
	}

	short countWords() {
		return countWords(_value);
	}

	static string upperFirstLetterOfEachWord(string s1) {
		bool isFirstLetter = true;

		for (short i = 0; i < s1.length(); i++) {
			if (s1[i] != ' ' && isFirstLetter)
				s1[i] = toupper(s1[i]);

			isFirstLetter = (s1[i] == ' ');
		}

		return s1;
	}

	void upperFirstLetterOfEachWord() {
		_value = upperFirstLetterOfEachWord(_value);
	}

	static string lowerFirstLetterOfEachWord(string s1) {
		bool isFirstLetter = true;

		for (short i = 0; i < s1.length(); i++) {
			if (s1[i] != ' ' && isFirstLetter)
			{
				s1[i] = tolower(s1[i]);
			}

			isFirstLetter = (s1[i] == ' ');
		}

		return s1;
	}

	void lowerFirstLetterOfEachWord() {
		_value = lowerFirstLetterOfEachWord(_value);
	}

	static string upperAllString(string s1) {
		for (short i = 0; i < s1.length(); i++)
			s1[i] = toupper(s1[i]);

		return s1;
	}

	void upperAllString() {
		_value = upperAllString(_value);
	}

	static string lowerAllString(string s1) {
		for (short i = 0; i < s1.length(); i++)
			s1[i] = tolower(s1[i]);
		return s1;
	}

	void lowerAllString() {
		_value = lowerAllString(_value);
	}

	static char invertLetterCase(char ch) {
		return isupper(ch) ? tolower(ch) : toupper(ch);
	}

	static string invertAllLettersCase(string s1) {
		for (short i = 0; i < s1.length(); i++)
			s1[i] = invertLetterCase(s1[i]);

		return s1;
	}

	void invertAllLettersCase() {
		_value = invertAllLettersCase(_value);
	}

	enum enWhatToCount { smallLetters = 0, capitalLetters = 1, all = 3 };

	static short countLetters(string s1, enWhatToCount whatToCount = enWhatToCount::all) {
		short counter = 0;

		if (whatToCount == enWhatToCount::all)
			return s1.length();

		for (short i = 0; i < s1.length(); i++) {
			if (whatToCount == enWhatToCount::capitalLetters && isupper(s1[i]))
				counter++;

			if (whatToCount == enWhatToCount::smallLetters && islower(s1[i]))
				counter++;
		}

		return counter;
	}

	static short countCapitalLetters(string s1) {
		short counter = 0;

		for (short i = 0; i < s1.length(); i++) {
			if (isupper(s1[i]))
				counter++;
		}

		return counter;
	}

	short countCapitalLetters() {
		return countCapitalLetters(_value);
	}

	static short countSmallLetters(string s1) {
		short counter = 0;

		for (short i = 0; i < s1.length(); i++) {
			if (islower(s1[i]))
				counter++;
		}

		return counter;
	}

	short countSmallLetters() {
		return countSmallLetters(_value);
	}

	static short countSpecificLetter(string s1, char letter, bool matchCase = true) {
		short counter = 0;

		for (short i = 0; i < s1.length(); i++) {
			if (matchCase) {
				if (s1[i] == letter)
					counter++;
			}
			else {
				if (tolower(s1[i]) == tolower(letter))
					counter++;
			}
		}

		return counter;
	}

	short countSpecificLetter(char letter, bool matchCase = true) {
		return countSpecificLetter(_value, letter, matchCase);
	}

	static bool isVowel(char ch) {
		ch = tolower(ch);

		return (ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u');
	}

	static short countVowels(string s1) {
		short counter = 0;

		for (short i = 0; i < s1.length(); i++) {
			if (isVowel(s1[i]))
				counter++;
		}

		return counter;
	}

	short countVowels() {
		return countVowels(_value);
	}

	static vector<string> split(string s1, string delim) {
		vector<string> vString;
		short pos = 0;
		string word;

		while ((pos = s1.find(delim)) != string::npos) {
			word = s1.substr(0, pos);
			if (!word.empty())
				vString.push_back(word);

			s1.erase(0, pos + delim.length());
		}

		if (!s1.empty())
			vString.push_back(s1);

		return vString;
	}

	vector<string> split(string delim) {
		return split(_value, delim);
	}

	static string trimLeft(string s1) {
		for (short i = 0; i < s1.length(); i++) {
			if (s1[i] != ' ')
				return s1.substr(i, s1.length() - i);
		}

		return "";
	}

	void trimLeft() {
		_value = trimLeft(_value);
	}

	static string trimRight(string s1) {
		for (short i = s1.length() - 1; i >= 0; i--) {
			if (s1[i] != ' ')
				return s1.substr(0, i + 1);
		}
		return "";
	}

	void trimRight() {
		_value = trimRight(_value);
	}

	static string trim(string s1) {
		return trimLeft(trimRight(s1));
	}

	void trim() {
		_value = trim(_value);
	}

	static string joinString(vector<string> vString, string delim) {
		string s1 = "";

		for (string& s : vString)
			s1 = s1 + s + delim;

		return s1.substr(0, s1.length() - delim.length());
	}

	static string joinString(string arrString[], short length, string delim) {
		string s1 = "";

		for (short i = 0; i < length; i++)
			s1 = s1 + arrString[i] + delim;

		return s1.substr(0, s1.length() - delim.length());
	}

	static string reverseWordsInString(string s1) {
		vector<string> vString = split(s1, " ");
		string s2 = "";
		vector<string>::iterator iter = vString.end();


		while (iter != vString.begin()) {
			--iter;
			s2 += *iter + " ";
		}

		s2 = s2.substr(0, s2.length() - 1); // remove last space

		return s2;
	}

	void reverseWordsInString() {
		_value = reverseWordsInString(_value);
	}

	static string replaceWord(string s1, string stringToReplace, string replaceTo, bool matchCase = true)
	{
		vector<string> vString = split(s1, " ");

		for (string& s : vString) {
			if (matchCase) {
				if (s == stringToReplace)
					s = replaceTo;
			}
			else {
				if (lowerAllString(s) == lowerAllString(stringToReplace))
					s = replaceTo;
			}
		}

		return joinString(vString, " ");
	}

	string replaceWord(string stringToReplace, string replaceTo) {
		return replaceWord(_value, stringToReplace, replaceTo);
	}

	static string removePunctuations(string s1) {
		string s2 = "";

		for (short i = 0; i < s1.length(); i++) {
			if (!ispunct(s1[i]))
				s2 += s1[i];
		}

		return s2;
	}

	void removePunctuations() {
		_value = removePunctuations(_value);
	}
};