// Problem #16 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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
	date.year = inputsFunctions::readPositiveInteger(" -> What year ? ");
	date.month = inputsFunctions::readPositiveInteger(1, 12, " -> What month ? ");
	date.day = inputsFunctions::readPositiveInteger(1, 31, " -> What day ? ");

	return date;
}

string getStringFormatOfDate(Date date, char delimiter = '/') {
	return (to_string(date.day) + delimiter + to_string(date.month) + delimiter + to_string(date.year));
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

Date IncreaseDateByOneDay(Date date) {
	if (isLastDayInMonth(date))
	{
		if (isLastMonthInYear(date.month))
		{
			++date.year;
			date.month = 1;
			date.day = 1;;
		}
		else
		{
			++date.month;
			date.day = 1;
		}
	}
	else
		++date.day;

	return date;
}


int main()
{
	Date date;
	short totalNumberOfDaysToAdd;

	while (true)
	{
		date = ReadDate(" -> What date to check ? \n");

		cout << " The date [" << getStringFormatOfDate(date) << "] fater adding 1 day becomes [" << getStringFormatOfDate(IncreaseDateByOneDay(date)) << "]" << endl;

		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}
