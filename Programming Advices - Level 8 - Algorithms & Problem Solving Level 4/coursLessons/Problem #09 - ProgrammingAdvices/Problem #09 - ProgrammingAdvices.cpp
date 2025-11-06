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

short getCalendarFirstDayPosition(short year, short month) {
	short firstDay = getDayOfWeekOrderFromDate(year, month, 1);
	return (5 * (firstDay + 1));
}

void printMonthDaysForCalendar(short year, short month) {
	short numberOfMonthDays;
	short firstDayOrder;
	short firstDayPosition;
	short currentDay;

	numberOfMonthDays = daysPerMonth(year, month);
	firstDayOrder = getDayOfWeekOrderFromDate(year, month, 1);
	firstDayPosition = getCalendarFirstDayPosition(year, month);
	currentDay = firstDayOrder;

	cout << setw(firstDayPosition) << 1;

	for (short day = 2; day <= numberOfMonthDays; day++)
	{
		if (currentDay == daysOfTheWeek::saturday) {
			cout << endl;
			currentDay = daysOfTheWeek::sunday;
		}
		else
			++currentDay;

		cout << setw(5) << day;
	}

	cout << endl;
}

void printMonthCalendar(short year, short month) {
	cout << "  " << "---------------" << getMonthName(month) << "---------------" << endl;
	printWeekDaysHeader();
	printMonthDaysForCalendar(year, month);
	cout << "  " << "---------------------------------" << endl;


}

void printYearCalendar(short year) {

	cout << "  " << "=================================" << endl;
	cout << "  " << "         Calendar - " << year << "\t\t" << endl;
	cout << "  " << "=================================" << endl;

	for (short month = 1; month <= 12; month++)
	{
		cout << endl << endl;
		printMonthCalendar(year, month);
	}

}

int main()
{
	short year;

	while (true)
	{
		year = inputsFunctions::readPositiveInteger(0, 9999, " -> What year to check ? ");

		printYearCalendar(year);

		cout << endl;
		cout << endl;

		system("pause");
		system("cls");
	}



}
