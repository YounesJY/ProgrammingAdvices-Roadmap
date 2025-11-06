// Problem #54 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#pragma warning(disable : 4996)
#include <iostream>
#include <string>
#include "../libs/MyInputLibrary.h"
#include "../libs/MyOthersStuffLibrary.h"

using namespace std;

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


bool isDate1LessThanDate2(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (date1.day < date2.day) : (date1.month < date2.month)) : (date1.year < date2.year));
}

bool isDatesAreEqual(Date date1, Date date2) {
	return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (((date1.day == date2.day)) ? true : false) : false) : false);
}


bool isLastMonthInYear(short month) {
	return (month == 12);
}

bool isLastMonthInYear(string month) {
	month = others::lowerCaseAllString(month);

	return (month == "december" || month == "dec");
}

bool isLastDayInMonth(Date date) {
	return (daysPerMonth(date.year, date.month) == date.day);
}


Date increaseDateByOneDay(Date date) {
	if (isLastDayInMonth(date))
	{
		if (isLastMonthInYear(date.month))
		{
			++date.year;
			date.month = 1;
			date.day = 1;;
		}
		else
		{
			++date.month;
			date.day = 1;
		}
	}
	else
		++date.day;

	return date;
}

short getTheDifferenceBetweenTwoDates(Date date1, Date date2, bool isLastDayIncluded = false) {
	short differenceBetweenDates = 0;
	short swappingDatesflag = 1;

	if (!isDate1LessThanDate2(date1, date2))
	{
		swapDates(date1, date2);
		swappingDatesflag = -1;
	}

	while (isDate1LessThanDate2(date1, date2))
	{
		date1 = increaseDateByOneDay(date1);
		++differenceBetweenDates;
	}

	return ((isLastDayIncluded ? ++differenceBetweenDates : differenceBetweenDates) * swappingDatesflag);

}


Date getSyestemDate() {
	Date date;

	time_t t = time(0);
	tm* now = localtime(&t);

	date.year = now->tm_year + 1900;
	date.month = now->tm_mon + 1;
	date.day = now->tm_mday;


	return date;
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

bool isEndOfWeek(Date date) {
	return getDayOfWeekOrderFromDate(date) == daysOfWeek::saturday;
}

bool isWeekend(Date date) {
	short dayOrder = getDayOfWeekOrderFromDate(date);
	return  (dayOrder == daysOfWeek::friday || dayOrder == daysOfWeek::saturday);
}

bool isBusinessDay(Date date) {
	return !isWeekend(date);
}

short getVacationPeriod(Date vacationStartDate, Date vacationEndDate) {
	Date currentDate = vacationStartDate;
	short vacationPeriod = 0;

	while (isDate1LessThanDate2(currentDate, vacationEndDate))
	{
		if (isBusinessDay(currentDate))
			++vacationPeriod;

		currentDate = increaseDateByOneDay(currentDate);
	}

	return vacationPeriod;
}

Date getVacationReturnDate(Date vacationStartDate, short vacationPeriod) {
	Date currentDate = vacationStartDate;
	Date returnDate;

	while (vacationPeriod != 0)
	{
		if (isBusinessDay(currentDate))
			--vacationPeriod;
		currentDate = increaseDateByOneDay(currentDate);
	}

	returnDate = currentDate;

	return returnDate;
}

int main() {
	Date vacationStartDate;
	Date returnDate;
	short vacationPeriod;

	vacationStartDate = ReadDate(" -> Please enter vacation start date: \n");
	vacationPeriod = inputsFunctions::readPositiveInteger(" -> Please enter vacation period: ");

	cout << "  - Vacation from: " << getDayOfWeekName(getDayOfWeekOrderFromDate(vacationStartDate)) << " " << getStringFormatOfDate(vacationStartDate) << endl;

	returnDate = getVacationReturnDate(vacationStartDate, vacationPeriod);
	cout << " -- Return date  : " << getDayOfWeekName(getDayOfWeekOrderFromDate(returnDate)) << " " << getStringFormatOfDate(returnDate) << endl;
}