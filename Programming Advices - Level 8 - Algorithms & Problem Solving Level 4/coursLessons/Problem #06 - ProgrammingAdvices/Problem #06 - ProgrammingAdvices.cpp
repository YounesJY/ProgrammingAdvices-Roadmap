// Problem #06 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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


int main()
{
	short year, month;

	while (true)
	{
		year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");
		month = inputsFunctions::readPositiveInteger(1, 12, " -> What month to check ? ");

		cout << "  - Number of days    in the date [" << month << "/" << year << "] is: " << daysPerMonth(year, month) << endl;

		system("pause");
		system("cls");
	}


}
