// Problem #63 - 64 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
// Problem #62 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
// Problem #61 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include <string>
#include <vector>
#include "../libs/MyInputLibrary.h"
#include "../libs/MyOthersStuffLibrary.h"

using namespace std;

enum comparedDatesResult { before = -1, equal = 0, after = 1 };

enum daysOfWeek {
	sunday,
	monday,
	tuesday,
	wednesday,
	thursday,
	friday,
	saturday
};

struct Date
{
	short year = 0;
	short month = 0;
	short day = 0;
};

struct Period
{
	Date startDate;
	Date endtDate;
};



Date ReadDate(string messageToDisplay) {
	Date date;

	cout << messageToDisplay;
	date.year = inputsFunctions::readPositiveInteger(" -> What year ? ");
	date.month = inputsFunctions::readPositiveInteger(1, 12, " -> What month ? ");
	date.day = inputsFunctions::readPositiveInteger(1, 31, " -> What day ? ");

	return date;
}


vector<string> splitStringWords(string text, string delimiter = " ", bool matchCase = true) {
	short position = 0;
	vector<string> textWords;
	string word = "";

	if (matchCase)
	{
		while ((position = text.find(delimiter)) != string::npos)
		{
			word = text.substr(0, position);

			if (!word.empty())
				textWords.push_back(word);

			text.erase(0, position + delimiter.size());
		}
	}
	else
	{
		delimiter = others::lowerCaseAllString(delimiter);
		string word = "";
		while ((position = others::lowerCaseAllString(text).find(delimiter)) != string::npos)
		{
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


short daysPerMonth(short year, short month) {
	if (month < 1 || month > 12)
		return 0;

	short daysPerMonths[] =
	{ 0,
		31,
		28,
		31,
		30,
		31,
		30,
		31,
		31,
		30,
		31,
		30,
		31
	};

	return (!isLeapYear(year) ? daysPerMonths[month] : ((month == 2) ? 29 : daysPerMonths[month]));
}


bool isAValidDate(Date date) {
	return ((1 <= date.month && date.month <= 12) ? (1 <= date.day && date.day <= daysPerMonth(date.year, date.month) ? true : false) : false);

	//return ((1 > date.month || date.month > 12) ? false : ((1 > date.day || date.day > daysPerMonth(date.year, date.month)) ? false : true));
}

string dateToString(Date date, char delimiter = '/') {
	if (!isAValidDate(date))
		return "";

	return (to_string(date.day) + delimiter + to_string(date.month) + delimiter + to_string(date.year));
}

Date stringToDate(string dateInStringFormat, string delimiter = "/") {
	Date date;
	vector<string> dateElements;

	dateElements = splitStringWords(dateInStringFormat, delimiter);

	date.day = stoi(dateElements[0]);
	date.month = stoi(dateElements[1]);
	date.year = stoi(dateElements[2]);


	return date;
}

bool isLeapYear(short year) {
	return ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0));
}


int main() {
	string dateStrFormat;
	Date date;
	while (true)
	{
		dateStrFormat = readString(" -> Please enter date in format dd/mm/yyyy: ");
		cout << endl;
		date = stringToDate(dateStrFormat, "/");

		cout << " -> Year : " << date.year << endl;
		cout << " -> Month: " << date.month << endl;
		cout << " -> Day  : " << date.day << endl;

		cout << endl;
		cout << "  - You entered [" << dateToString(date) << "]" << endl;
		cout << endl;

		system("pause");
		system("cls");
	}

}