// Problem #04 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../libs/MyInputLibrary.h"


bool isLeapYear(short year) {
	return ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0));
}

short daysPerYear(short year) {
	return (isLeapYear(year) ? 366 : 365);
}

int hoursPerYear(short year) {
	return (24 * daysPerYear(year));
}

int minutesPerYear(short year) {
	return (60 * hoursPerYear(year));
}

int secondsPerYear(short year) {
	return (60 * minutesPerYear(year));
}

int main()
{
	short year;
	while (true)
	{
		year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");
		
		cout << "  - Number of days    in the year [" << year << "] is: " << daysPerYear(year)<< endl;
		cout << "  - Number of hours   in the year [" << year << "] is: " << hoursPerYear(year)<< endl;
		cout << "  - Number of minutes in the year [" << year << "] is: " << minutesPerYear(year)<< endl;
		cout << "  - Number of seconds in the year [" << year << "] is: " << secondsPerYear(year)<< endl;
		
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
