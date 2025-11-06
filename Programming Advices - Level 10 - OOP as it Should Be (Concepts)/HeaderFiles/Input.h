#pragma once

#include <iostream>
#include <string>
#include "String.h"

using namespace std;

class Input {
private:
	enum choice {
		yes = 1,
		no = 2
	};

	Input() {}

	static string getAnswer(string messageToDisplay = "") {
		string answer = "";

		do
		{
			answer = String::stringToLower(readString(messageToDisplay));

			if (answer != string("yes") && answer != string("no"))
			{
				cout << " !! Invalid value [" << answer << "] !, " << endl;
				cout << "  - Try again: \n" << endl;
			}

		} while (answer != "yes" && answer != "no");

		return answer;
	}

public:
	static double readNumber(string message = "") {
		double number;
		cout << message;
		cin >> number;

		while (cin.fail()) {
			cin.clear();
			cin.ignore(std::numeric_limits < std::streamsize>::max(), '\n');
			cout << "  Invalid Number!" << endl;
			cout << "  - Try again: \n" << endl;

			cout << message;
			cin >> number;
		}

		return number;
	}

	static int readInteger(string messageToDisplay = "") {
		return floor(readNumber(messageToDisplay));
	}

	static int readPositiveInteger(string message = "") {
		return readPositiveInteger(1, message);
	}

	static int readPositiveInteger(int from, string message = "") {
		int number;
		do {
			number = readInteger(message);
			if (number < from) {
				cout << " !! Invalid value [" << number << "] !" << endl;
				cout << "  - Try again: \n" << endl;
			}
		} while (number < from);
		return number;
	}

	static int readPositiveInteger(int from, int to, string messageToDisplay = "") {
		int number;
		do {
			number = readPositiveInteger(messageToDisplay);
			if (number < from || number > to) {
				cout << " !! Invalid value [" << number << "] !" << endl;
				cout << "  - Try again: \n" << endl;
			}
		} while (number < from || number > to);
		return number;
	}

	static string readString(string message = "") {
		string userInput;

		cout << message;
		getline(cin >> ws, userInput);

		return userInput;
	}

	static enum choice getChoice(string messageToDisplay = "", bool defaultForm = true) {
		if (defaultForm) {
			cout << messageToDisplay;
			cout << "  > [1] Yes" << endl;
			cout << "  > [2] No" << endl;
			cout << " => Your choice ? ";
			return (enum choice)readPositiveInteger(1, 2);
		}
		else {
			return ((getAnswer(messageToDisplay) == "yes") ? choice::yes : choice::no);
		}
	}

	static char readCharacter(string message = "") {
		char character;

		cout << message;
		cin >> character;

		return character;
	}
};


