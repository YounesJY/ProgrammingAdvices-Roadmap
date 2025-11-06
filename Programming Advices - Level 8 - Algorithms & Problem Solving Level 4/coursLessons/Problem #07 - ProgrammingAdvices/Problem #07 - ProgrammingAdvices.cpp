// Problem #07 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../libs/MyInputLibrary.h"


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

short getDayOfWeekOrderFromDate(short year, short month, short day) {
	short y, a, m, dayOrder;

	a = (14 - month) / 12;
	y = year - a;
	m = month + (12 * a) - 2;
	dayOrder = (day + y + (y / 4) - (y / 100) + (y / 400) + ((31 * m) / 12)) % 7;

	return dayOrder;
}

string getDayOfWeekNameFromDate(short day) {
	string daysNames[] =
	{
	"Sunday",
	"Monday",
	"Tuesday",
	"Wednesday",
	"Thursday",
	"Friday",
	"Saturday"
	};

	if (day < 0 || day > 6)
		return "";

	return daysNames[day];
}
string getDayOfWeekNameFromDate(short year, short month, short day) {
	return getDayOfWeekNameFromDate(getDayOfWeekOrderFromDate(year, month, day));
}



string getStringFormatOfDate(short year, short month, short day, char delimiter = '/') {
	return (to_string(day) + delimiter + to_string(month) + delimiter + to_string(year));
}


int main()
{
	short year, month, day;

	while (true)
	{
		year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");
		month = inputsFunctions::readPositiveInteger(1, 12, " -> What month to check ? ");
		day = inputsFunctions::readPositiveInteger(1, 31, " -> What day to check ? ");

		cout << "---------------------------------------------" << endl;
		cout << " -- Date       : " << getStringFormatOfDate(year, month, day) << endl;
		cout << " -- Day order  : " << getDayOfWeekOrderFromDate(year, month, day) << endl;
		cout << " -- Day name   : " << getDayOfWeekNameFromDate(year, month, day) << endl;
		cout << "---------------------------------------------" << endl;

		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}


}
