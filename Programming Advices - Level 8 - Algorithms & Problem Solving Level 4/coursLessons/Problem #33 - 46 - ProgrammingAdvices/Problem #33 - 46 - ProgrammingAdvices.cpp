// Problem #33 - 46 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
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

void swapDates(Date& date1, Date& date2) {
	Date tempDateFroSwap;

	tempDateFroSwap = date1;
	date1 = date2;
	date2 = tempDateFroSwap;
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
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (date1.day < date2.day) : (date1.month < date2.month)) : (date1.year < date2.year));
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


Date decreaseDateByOneDay(Date date) {
	if (date.day == 1)
	{
		if (date.month == 1)
		{
			--date.year;
			date.month = 12;
			date.day = 31;
		}
		else
		{
			--date.month;
			date.day = daysPerMonth(date.year, date.month);
		}
	}
	else
		--date.day;

	return date;
}

Date decreaseDateByXDays(Date date, short daysToSubrtract) {
	for (short day = 1; day <= daysToSubrtract; day++)
		date = decreaseDateByOneDay(date);

	return date;
}

Date decreaseDateByOneWeek(Date date) {
	for (short day = 1; day <= 7; day++)
		date = decreaseDateByOneDay(date);

	return date;
}


Date decreaseDateByXWeeks(Date date, short weeksToSubrtract) {
	for (short day = 1; day <= weeksToSubrtract; day++)
		date = decreaseDateByOneWeek(date);

	return date;
}


Date decreaseDateByOneMonth(Date date) {
	short lastDayInMonth;

	if (date.month == 1)
	{
		--date.year;
		date.month = 12;
	}
	else
	{
		--date.month;

		lastDayInMonth = daysPerMonth(date.year, date.month);
		if (lastDayInMonth < date.day)
			date.day = lastDayInMonth;
	}

	return date;
}
Date decreaseDateByXMonths(Date date, short monthsToSubrtract) {
	for (short month = 1; month <= monthsToSubrtract; month++)
		date = decreaseDateByOneMonth(date);

	return date;
}

Date decreaseDateByOneYear(Date date) {
	--date.year;

	return date;
}


Date decreaseDateByXYears(Date date, short yearsToSubrtract) {
	for (short year = 1; year <= yearsToSubrtract; year++)
		date = decreaseDateByOneYear(date);

	return date;
}
Date decreaseDateByXYearsFaster(Date date, short yearsToSubrtract) {
	date.year -= yearsToSubrtract;

	return date;
}


Date decreaseDateByOneDecade(Date date) {
	date.year -= 10;

	return date;
}

Date decreaseDateByXDecades(Date date, short decadesToSubrtract) {
	for (short decade = 1; decade <= decadesToSubrtract; decade++)
		date = decreaseDateByOneDecade(date);

	return date;
}

Date decreaseDateByXDecadesFaster(Date date, short decadesToSubrtract) {
	date.year -= (decadesToSubrtract * 10);

	return date;
}

Date decreaseDateByOneCentury(Date date) {
	date.year -= 100;

	return date;
}

Date decreaseDateByOneMillennium(Date date) {
	date.year -= 1000;

	return date;
}

int main()
{
	Date date;
	short differenceBetweenDates;

	while (true)
	{
		date = ReadDate(" -> What date to check? \n");

		cout << endl;

		cout << " -- Date after:" << endl << endl;

		date = decreaseDateByOneDay(date);
		cout << "  - 01 - Decreasing one day             ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByXDays(date, 10);
		cout << "  - 02 - Decreasing 10 days             ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByOneWeek(date);
		cout << "  - 03 - Decreasing one week            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByXWeeks(date, 10);
		cout << "  - 04 - Decreasing 10 weeks            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByOneMonth(date);
		cout << "  - 05 - Decreasing 1 month             ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByXMonths(date, 5);
		cout << "  - 06 - Decreasing 5 months            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByOneYear(date);
		cout << "  - 07 - Decreasing one year            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByXYears(date, 10);
		cout << "  - 08 - Decreasing 10 years            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByXYearsFaster(date, 10);
		cout << "  - 09 - Decreasing 10 years (faster)   ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByOneDecade(date);
		cout << "  - 10 - Decreasing one decade          ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByXDecades(date, 10);
		cout << "  - 11 - Decreasing 10 decades          ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByXDecadesFaster(date, 10);
		cout << "  - 12 - Decreasing 10 decades (faster) ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByOneCentury(date);
		cout << "  - 12 - Decreasing 1 century           ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = decreaseDateByOneMillennium(date);
		cout << "  - 14 - Decreasing 1 millennium        ----> [" << getStringFormatOfDate(date) << "]" << endl;



		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}