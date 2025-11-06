// Problem #10 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
// Problem #09 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

int main()
{
	short year, month, day;

	while (true)
	{
		year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");
		month = inputsFunctions::readPositiveInteger(0, 12, " -> What month  ? ");
		day = inputsFunctions::readPositiveInteger(1, 31, " -> What day to ? ");

		cout << " -> Totale days from the beginning of the year is: [ " << getTotalNumberOfDaysFromThebeginningOfYear(year, month, day) << " days]" << endl;

		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}
