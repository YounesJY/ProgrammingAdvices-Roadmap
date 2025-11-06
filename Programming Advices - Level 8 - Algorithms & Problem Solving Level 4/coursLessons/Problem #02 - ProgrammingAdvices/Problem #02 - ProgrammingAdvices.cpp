// Problem #02 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../libs/MyInputLibrary.h"


bool isLeapYear(short year) {
	return ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0));
}

int main()
{
	short year;
	while (true)
	{
		year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");
		cout << "  - The year [" << year << "] is  {" << (isLeapYear(year) ? "a Leap" : "not leap") << "} year" << endl;
		system("pause");
		system("cls");
		
	}
}
