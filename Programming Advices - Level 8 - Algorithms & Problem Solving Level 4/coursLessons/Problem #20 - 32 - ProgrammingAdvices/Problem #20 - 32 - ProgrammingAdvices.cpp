// Problem #20 - 32 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
// Problem #18 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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


Date increaseDateByOneDay(Date date) {
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

Date increaseDateByXDays(Date date, short daysToAdd) {
	for (short day = 1; day <= daysToAdd; day++)
		date = IncreaseDateByOneDay(date);

	return date;
}

Date increaseDateByOneWeek(Date date) {
	for (short day = 1; day <= 7; day++)
		date = IncreaseDateByOneDay(date);

	return date;
}

Date increaseDateByXWeeks(Date date, short weeksToAdd) {
	for (short day = 1; day <= weeksToAdd; day++)
		date = IncreaseDateByOneWeek(date);

	return date;
}

Date increaseDateByOneMonth(Date date) {
	short lastDayInMonth;

	if (isLastMonthInYear(date.month))
	{
		++date.year;
		date.month = 1;
	}
	else
	{
		++date.month;

		lastDayInMonth = daysPerMonth(date.year, date.month);
		if (lastDayInMonth < date.day)
			date.day = lastDayInMonth;
	}

	return date;
}

Date increaseDateByXMonths(Date date, short monthsToAdd) {
	for (short month = 1; month <= monthsToAdd; month++)
		date = IncreaseDateByOneMonth(date);

	return date;
}

Date increaseDateByOneYear(Date date) {
	date.year++;

	return date;
}

Date increaseDateByXYears(Date date, short yearsToAdd) {
	for (short year = 1; year <= yearsToAdd; year++)
		date = IncreaseDateByOneYear(date);

	return date;
}

Date increaseDateByXYearsFaster(Date date, short yearsToAdd) {
	date.year += yearsToAdd;

	return date;
}

Date increaseDateByOneDecade(Date date) {
	date.year += 10;

	return date;
}

Date increaseDateByXDecades(Date date, short decadesToAdd) {
	for (short decade = 1; decade <= decadesToAdd; decade++)
		date = IncreaseDateByOneDecade(date);

	return date;
}

Date increaseDateByXDecadesFaster(Date date, short decadesToAdd) {
	date.year += (decadesToAdd * 10);

	return date;
}

Date increaseDateByOneCentury(Date date) {
	date.year += 100;

	return date;
}

Date increaseDateByOneMillennium(Date date) {
	date.year += 1000;

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

		cout << " -- Date after adding:" << endl << endl;

		date = increaseDateByOneDay(date);
		cout << "  - 01 - Adding one day             ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByXDays(date, 10);
		cout << "  - 02 - Adding 10 days             ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByOneWeek(date);
		cout << "  - 03 - Adding one week            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByXWeeks(date, 10);
		cout << "  - 04 - Adding 10 weeks            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByOneMonth(date);
		cout << "  - 05 - Adding 1 month             ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByXMonths(date, 5);
		cout << "  - 06 - Adding 5 months            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByOneYear(date);
		cout << "  - 07 - Adding one year            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByXYears(date, 10);
		cout << "  - 08 - Adding 10 years            ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByXYearsFaster(date, 10);
		cout << "  - 09 - Adding 10 years (faster)   ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByOneDecade(date);
		cout << "  - 10 - Adding one decade          ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByXDecades(date, 10);
		cout << "  - 11 - Adding 10 decades          ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByXDecadesFaster(date, 10);
		cout << "  - 12 - Adding 10 decades (faster) ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByOneCentury(date);
		cout << "  - 12 - Adding 1 century           ----> [" << getStringFormatOfDate(date) << "]" << endl;

		date = increaseDateByOneMillennium(date);
		cout << "  - 14 - Adding 1 millennium        ----> [" << getStringFormatOfDate(date) << "]" << endl;



		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}
