#pragma once
#pragma once

#include <iostream>
#include <string>
#include "String.h"

using namespace std;

class Input {
private:
	Input() {}

	static string _getAnswer(string messageToDisplay = "") {
		string answer = "";

		do
		{
			answer = String::lowerAllString(readString(messageToDisplay));

			if (answer != string("yes") && answer != string("no"))
			{
				cout << " !! Invalid value [" << answer << "] !" << endl;
				cout << "  - Try again: \n" << endl;
			}

		} while (answer != "yes" && answer != "no");

		return answer;
	}

	static double _readNumber(string message = "") {
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

public:
	enum choice {
		yes = 1,
		no = 2
	};

	static bool isNumberBetween(int number, int from, int to) {
		return (number >= from && number <= to);
	}

	static bool isNumberBetween(double number, double from, double to) {
		return (number >= from && number <= to);
	}

	static double readDouble(string message = "") {
		return _readNumber(message);
	}

	static int readDoubleBetween(int from, int to, string messageToDisplay = "") {
		double number;
		do {
			number = _readNumber(messageToDisplay);
			if (number < from || number > to) {
				cout << " !! Invalid value [" << number << "] !" << endl;
				cout << "  - Try again: \n" << endl;
			}
		} while (number < from || number > to);
		return number;
	}


	static int readInteger(string messageToDisplay = "") {
		return floor(_readNumber(messageToDisplay));
	}

	static int readPositiveInteger(string message = "") {
		return readIntegerFrom(1, message);
	}

	static int readIntegerFrom(int from, string message = "") {
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

	static int readIntegerBetween(int from, int to, string messageToDisplay = "") {
		int number = readInteger(messageToDisplay);

		while (!isNumberBetween(number, from, to)) {
			cout << " !! Invalid value [" << number << "] !" << endl;
			cout << "  - Try again: \n" << endl;
			number = readInteger(messageToDisplay);
		}

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

			return (enum choice)readIntegerBetween(1, 2);
		}
		else {
			return ((_getAnswer(messageToDisplay) == "yes") ? choice::yes : choice::no);
		}
	}

	static char readCharacter(string message = "") {
		char character;

		cout << message;
		cin >> character;

		return character;
	}
};


