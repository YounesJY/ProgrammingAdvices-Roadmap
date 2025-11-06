// Problem #14 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>

using namespace std;

struct Date
{
	short year;
	short month;
	short day;
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

short getTotalNumberOfDaysFromThebeginningOfYear(short year, short currentMonth, short currentDay) {
	short totalNumberOfDays = 0;

	for (short month = 1; month < currentMonth; month++)
		totalNumberOfDays += daysPerMonth(year, month);

	totalNumberOfDays += currentDay;

	return totalNumberOfDays;
}


bool isDatesAreEqual(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (((date1.day == date2.day)) ? true : false) : false) : false);
}

int main()
{
	Date date1, date2;
	
	date1.year = 2024;
	date2.year = 2024;
	
	date1.month = 8;
	date2.month = 8;
	
	date1.day = 19;
	date2.day = 18;

	cout << (isDatesAreEqual(date1, date2) ? " Yes they are equal." : " No they aren't equal.") << endl;

}
