// Problem #13 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

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


bool isDate1LessThanDate2(Date date1, Date date2) {
	// Teacher's Method || Or use shortHand if
	
	if (date1.year == date2.year)
	{
		if (date1.month == date2.month)
		{
			if (date1.day == date2.day)
				return false;
			else
				return date1.day < date2.day;
		}
		else
			return date1.month < date2.month;
	}
	else
		return date1.year < date2.year;

	//	My method || Use shortHand if
	//if (date1.year == date2.year)
	//	return (getTotalNumberOfDaysFromThebeginningOfYear(date1.year, date1.month, date1.day) < getTotalNumberOfDaysFromThebeginningOfYear(date2.year, date2.month, date2.day));
	//else
	//	return date1.year < date2.year;
}

int main()
{
	std::cout << "Hello World!\n";
}
