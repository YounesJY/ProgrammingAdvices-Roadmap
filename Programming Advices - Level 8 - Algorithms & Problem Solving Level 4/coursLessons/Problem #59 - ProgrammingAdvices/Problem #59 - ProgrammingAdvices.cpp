// Problem #59 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#pragma warning(disable : 4996)
#include <iostream>
#include <string>
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


Period ReadPeriod(string messageToDisplay) {
	Period period;

	cout << messageToDisplay;

	cout << " -> Start date:" << endl;
	period.startDate = ReadDate("");

	cout << endl;

	cout << " -> End date:" << endl;
	period.endtDate = ReadDate("");

	return period;
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

bool isDatesAreEqual(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (((date1.day == date2.day)) ? true : false) : false) : false);
}

bool isDate1BeforeDate2(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (date1.day < date2.day) : (date1.month < date2.month)) : (date1.year < date2.year));
}

bool isDate1AfterDate2(Date date1, Date date2) {
	return (!isDate1BeforeDate2(date1, date2) && !isDatesAreEqual(date1, date2));
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



short compareDates(Date date1, Date date2) {
	return (isDate1BeforeDate2(date1, date2) ? comparedDatesResult::before : ((isDatesAreEqual(date1, date2)) ? comparedDatesResult::equal : comparedDatesResult::after));
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


short getTheDifferenceBetweenTwoDates(Date date1, Date date2, bool isLastDayIncluded = false) {
	short differenceBetweenDates = 0;
	short swappingDatesflag = 1;

	if (isDate1AfterDate2(date1, date2))
	{
		swapDates(date1, date2);
		swappingDatesflag = -1;
	}

	while (isDate1BeforeDate2(date1, date2))
	{
		date1 = increaseDateByOneDay(date1);
		++differenceBetweenDates;
	}

	return ((isLastDayIncluded ? ++differenceBetweenDates : differenceBetweenDates) * swappingDatesflag);

}

short getPeriodLength(Period period, bool isLastDayIncluded = false) {
	return getTheDifferenceBetweenTwoDates(period.startDate, period.endtDate, isLastDayIncluded);
}

int main() {
	Period period;

	while (true)
	{
		period = ReadPeriod(" -> Please enter the first period: \n");
		cout << endl;

		cout << " -> Period length is: [ " << getPeriodLength(period) << " day(s)]" << endl;

		cout << endl;

		system("pause");
		system("cls");
	}

}