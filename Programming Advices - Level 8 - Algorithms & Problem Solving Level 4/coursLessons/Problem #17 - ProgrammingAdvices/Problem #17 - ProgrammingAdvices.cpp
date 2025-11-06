// Problem #17 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
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


bool isDate1LessThanDate2(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (date1.year < date2.year) : (date1.month < date2.month)) : (date1.year < date2.year));
}

bool isDatesAreEqual(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (((date1.day == date2.day)) ? true : false) : false) : false);
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

short getTheDifferenceBetweenTwoDates(Date date1, Date date2, bool isLastDayIncluded = false) {
	short differenceBetweenDates = 0;

	while (isDate1LessThanDate2(date1, date2))
	{
		date1 = IncreaseDateByOneDay(date1);
		++differenceBetweenDates;
	}

	return (isLastDayIncluded ? ++differenceBetweenDates : differenceBetweenDates);
}

int main()
{
	Date date1, date2;
	short differenceBetweenDates;

	while (true)
	{
		date1 = ReadDate(" -> What date to check (date1)? \n");
		cout << endl;
		date2 = ReadDate(" -> What date to check (date2)? \n");

		differenceBetweenDates = getTheDifferenceBetweenTwoDates(date1, date2, true);
		cout << " The difference between the 2 dates [" << getStringFormatOfDate(date1) << "] and [" << getStringFormatOfDate(date2) << "] is ["
			<< (differenceBetweenDates == 0 ? "0 --> First date must be less than the second" : to_string(differenceBetweenDates)) << "]" << endl;

		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}
