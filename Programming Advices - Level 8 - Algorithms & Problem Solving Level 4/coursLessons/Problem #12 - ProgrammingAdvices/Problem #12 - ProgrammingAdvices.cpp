// Problem #12 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
#include "../libs/MyInputLibrary.h"

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

string getMonthName(short month) {
	string months[] =
	{ "",
	"Jan",
	"Feb",
	"Mar",
	"Apr",
	"May",
	"Jun",
	"Jul",
	"Aug",
	"Sep",
	"Oct",
	"Nov",
	"Dec",
	};

	return months[month];
}


short getDayOfWeekOrderFromDate(short year, short month, short day) {
	short y, a, m, dayOrder;

	a = (14 - month) / 12;
	y = year - a;
	m = month + (12 * a) - 2;
	dayOrder = (day + y + (y / 4) - (y / 100) + (y / 400) + ((31 * m) / 12)) % 7;

	return dayOrder;
}

void printWeekDaysHeader() {

	cout
		<< "  "
		<< "Sun" << "  "
		<< "Mon" << "  "
		<< "Tue" << "  "
		<< "Wed" << "  "
		<< "Thu" << "  "
		<< "Fri" << "  "
		<< "Sat" << "  "
		<< endl;

}


short getTotalNumberOfDaysFromThebeginningOfYear(short year, short currentMonth, short currentDay) {
	short totalNumberOfDays = 0;

	for (short month = 1; month < currentMonth; month++)
		totalNumberOfDays += daysPerMonth(year, month);

	totalNumberOfDays += currentDay;

	return totalNumberOfDays;
}

string getStringFormatOfDate(Date date, char delimiter = '/') {
	return (to_string(date.day) + delimiter + to_string(date.month) + delimiter + to_string(date.year));
}

Date getDateFromTotalNumberOfDays(short year, short totalNumberOfDays) {
	Date date;

	date.year = year;
	date.month = 1;

	while (totalNumberOfDays > daysPerMonth(year, date.month))
	{
		totalNumberOfDays -= daysPerMonth(year, date.month);
		date.month++;
	}
	date.day = totalNumberOfDays;

	return date;
}


Date addDaysToDate(Date date, short daysToAdd) {
	short totalDaysToAdd;
	short remainingDaysToAdd;
	short numberOfDaysOfMonth;

	totalDaysToAdd = daysToAdd + getTotalNumberOfDaysFromThebeginningOfYear(date.year, date.month, date.day);
	remainingDaysToAdd = totalDaysToAdd;

	date.month = 1;
	date.day = 1;

	while (true)
	{
		numberOfDaysOfMonth = daysPerMonth(date.year, date.month);

		if (remainingDaysToAdd > numberOfDaysOfMonth)
		{
			remainingDaysToAdd -= numberOfDaysOfMonth;
			++date.month;

			if (date.month > 12)
			{
				++date.year;
				date.month = 1;
			}
		}
		else

		{
			date.day = remainingDaysToAdd;
			break;
		}
	}

	return date;
}



int main()
{
	Date date;
	short totalNumberOfDaysToAdd;

	while (true)
	{
		date.year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");
		date.month = inputsFunctions::readPositiveInteger(0, 12, " -> What month  ? ");
		date.day = inputsFunctions::readPositiveInteger(1, 31, " -> What day to ? ");

		totalNumberOfDaysToAdd = inputsFunctions::readPositiveInteger(" -> How many days to add ? ");;

		cout << " -> Date after adding [ " << totalNumberOfDaysToAdd << " days] is : [" << getStringFormatOfDate(addDaysToDate(date, totalNumberOfDaysToAdd)) << "]" << endl;

		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}
