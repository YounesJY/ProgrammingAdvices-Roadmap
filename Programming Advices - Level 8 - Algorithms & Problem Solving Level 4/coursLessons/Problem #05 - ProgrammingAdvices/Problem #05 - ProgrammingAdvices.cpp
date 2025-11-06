// Problem #05 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
// Problem #04 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../libs/MyInputLibrary.h"


bool isLeapYear(short year) {
	return ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0));
}

short daysPerMonth(short year, short month) {
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

	if (isLeapYear(year))
		daysPerMonths[2] = 29;

	return daysPerMonths[month];
}

int hoursPerMonth(short year, short month) {
	return (24 * daysPerMonth(year, month));
}

int minutesPerMonth(short year, short month) {
	return (60 * hoursPerMonth(year, month));
}

int secondsPerMonth(short year, short month) {
	return (60 * minutesPerMonth(year, month));
}

int main()
{
	short year, month;

	while (true)
	{
		year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");
		month = inputsFunctions::readPositiveInteger(1, 12, " -> What month to check ? ");

		cout << "  - Number of days    in the date [" << month << "/" << year << "] is: " << daysPerMonth(year, month) << endl;
		cout << "  - Number of hours   in the date [" << month << "/" << year << "] is: " << hoursPerMonth(year, month) << endl;
		cout << "  - Number of minutes in the date [" << month << "/" << year << "] is: " << minutesPerMonth(year, month) << endl;
		cout << "  - Number of seconds in the date [" << month << "/" << year << "] is: " << secondsPerMonth(year, month) << endl;

		system("pause");
		system("cls");
	}


}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
