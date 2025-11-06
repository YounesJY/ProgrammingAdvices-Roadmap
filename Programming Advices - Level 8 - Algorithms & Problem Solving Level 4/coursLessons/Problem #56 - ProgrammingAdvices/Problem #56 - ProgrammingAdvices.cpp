// Problem #56 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
// Problem #55 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#pragma warning(disable : 4996)
#include <iostream>
#include <string>
#include "../libs/MyInputLibrary.h"
#include "../libs/MyOthersStuffLibrary.h"

using namespace std;

enum comparedDatesResult { before = -1, equal = 0, after = 1 };

enum daysOfWeek {
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



Date ReadDate(string messageToDisplay) {
	Date date;

	cout << messageToDisplay;
	date.year = inputsFunctions::readPositiveInteger(" -> What year ? ");
	date.month = inputsFunctions::readPositiveInteger(1, 12, " -> What month ? ");
	date.day = inputsFunctions::readPositiveInteger(1, 31, " -> What day ? ");

	return date;
}

string getStringFormatOfDate(Date date, char delimiter = '/') {
	return (to_string(date.day) + delimiter + to_string(date.month) + delimiter + to_string(date.year));
}

void swapDates(Date& date1, Date& date2) {
	Date tempDateFroSwap;

	tempDateFroSwap = date1;
	date1 = date2;
	date2 = tempDateFroSwap;
}



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

bool isDatesAreEqual(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (((date1.day == date2.day)) ? true : false) : false) : false);
}

bool isDate1BeforeDate2(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (date1.day < date2.day) : (date1.month < date2.month)) : (date1.year < date2.year));
}

bool isDate1AfterDate2(Date date1, Date date2) {
	return (!isDate1BeforeDate2(date1, date2) && !isDatesAreEqual(date1, date2));
}


short getDayOfWeekOrderFromDate(short year, short month, short day) {
	short y, a, m, dayOrder;

	a = (14 - month) / 12;
	y = year - a;
	m = month + (12 * a) - 2;
	dayOrder = (day + y + (y / 4) - (y / 100) + (y / 400) + ((31 * m) / 12)) % 7;

	return dayOrder;
}

short getDayOfWeekOrderFromDate(Date date) {
	return getDayOfWeekOrderFromDate(date.year, date.month, date.day);
}

string getDayOfWeekName(short day) {
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

short compareDates(Date date1, Date date2) {
	return (isDate1BeforeDate2(date1, date2) ? comparedDatesResult::before : ((isDatesAreEqual(date1, date2)) ? comparedDatesResult::equal : comparedDatesResult::after));
}

int main() {
	Date date1, date2;
	while (true)
	{
		date1 = ReadDate(" -> Please enter date1: \n");
		date2 = ReadDate(" -> Please enter date2: \n");

		cout << endl;

		cout << "  - First date : " << getDayOfWeekName(getDayOfWeekOrderFromDate(date1)) << " " << getStringFormatOfDate(date1) << endl;
		cout << "  - Second date: " << getDayOfWeekName(getDayOfWeekOrderFromDate(date2)) << " " << getStringFormatOfDate(date2) << endl;
		cout << "  > Comaparistion result : [" << compareDates(date1, date2) << "]" << endl;

		cout << endl;

		system("pause");
		system("cls");
	}
}