// Problem #15 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
// Problem #12 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <string>
#include "../libs/MyInputLibrary.h"
#include "../libs/MyOthersStuffLibrary.h"

using namespace std;

enum daysOfTheWeek {
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


bool isLeapYear(short year) {
	return ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0));
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

Date ReadDate(string messageToDisplay) {
	Date date;

	cout << messageToDisplay;
	date.year = inputsFunctions::readPositiveInteger(" -> What year ?");
	date.month = inputsFunctions::readPositiveInteger(1, 12, " -> What month ?");
	date.day = inputsFunctions::readPositiveInteger(1, 31, " -> What day ?");

	return date;
}

bool isLastMonthInYear(short month) {
	return (month == 12);
}

bool isLastMonthInYear(string month) {
	month = others::lowerCaseAllString(month);

	return (month == "december" || month == "dec");
}

bool isLastDayInMonth(Date date) {
	return (daysPerMonth(date.year, date.month) == date.day);
}

string getStringFormatOfDate(Date date, char delimiter = '/') {
	return (to_string(date.day) + delimiter + to_string(date.month) + delimiter + to_string(date.year));
}

int main()
{
	Date date;
	short totalNumberOfDaysToAdd;

	while (true)
	{
		date = ReadDate(" -> what date to check ? \n");


		cout << " For the date [" << getStringFormatOfDate(date) << "] day [" << date.day << "] " << (isLastDayInMonth(date) ? "is" : "isn't") << " the last day in month [" << date.month << "]" << endl;
		cout << " For the date [" << getStringFormatOfDate(date) << "] month [" << date.month << "] " << (isLastMonthInYear(date.month) ? "is" : "isn't") << " the last month in year [" << date.year << "]" << endl;
		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}
