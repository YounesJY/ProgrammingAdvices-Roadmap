// Problem #35 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

void getStringWords(string text, vector<string>& words, string se = " ") {
	// Method 1 by JY
	//            hellhhhhhhhho           there and
	// g              .
	//for (int wordhead = 0, wordtail = 0 ; wordtail < text.size(); wordtail++)
	//{
	//	while (text[wordhead] == ' ') 
	//	{
	//		++wordhead;
	//		++wordtail;
	//	}

	//	if (text[wordtail] == ' ') {
	//		words.push_back(text.substr(wordhead, wordtail - wordhead));
	//		wordhead = wordtail + 1;
	//		while (wordtail < text.size())
	//		{
	//			if (text[wordhead] != ' ')
	//			{
	//				wordtail = wordhead;
	//				break;
	//			}
	//			else
	//			{
	//				++wordtail;
	//				++wordhead;
	//			}
	//		}
	//	}

	//	if (wordtail == text.size() - 1)
	//	{
	//		words.push_back(text.substr(wordhead, wordtail - wordhead + 1));
	//	}
	//}


	// Method 2 by JY
	int wordHead = 0, wordTail = 0;
	char delimiter = ' ';
	bool isFirstLetter = true;

	//            hellhhhhhhhho           there and
	// g              .
	for (int i = 0; i < text.size(); i++)
	{
		if (text[i] != delimiter && isFirstLetter)
		{
			wordHead = i;
			while (i < text.size())
			{
				if (text[i + 1] != delimiter)
					++i;
				else
				{
					++i;
					wordTail = i;
					break;
				}
			}
			words.push_back(text.substr(wordHead, wordTail - wordHead));
			// hellhhhhhhhho           there and
			while ((i + 1) < text.size()) {
				if (text[i + 1] == delimiter)
					++i;
				else
					break;
			}
		}

		isFirstLetter = (text[i] == delimiter);
	}

	// Method 3 by Teacher

	//short position = 0;
	//string word = "";

	////	he           there and
	//while ((position = text.find(delimiter)) != string::npos)
	//{
	//	word = text.substr(0, position);

	//	if (!word.empty())
	//		cout << word << endl;

	//	text.erase(0, position + delimiter.size());
	//}

	//if (!text.empty())
	//	cout << text << endl;

}

void printStringWords(vector<string>& words) {
	for (const string& word : words)
		cout << word << " --> " << word.size() << endl;
}

int main()
{
	string text;
	vector<string> myTextWords;

	while (true)
	{

		text = readString(" -> Please enter something: ");
		cout << " -> Your text words are: " << endl;

		getStringWords(text, myTextWords);
		printStringWords(myTextWords);
		myTextWords.clear();
		cout << endl << endl;
	}
}