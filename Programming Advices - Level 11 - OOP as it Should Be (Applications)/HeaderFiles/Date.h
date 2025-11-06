// ProgrammingAdivces.com
// Mohammed Abu-Hadhoud
#pragma warning(disable : 4996)
#pragma once

#include <iostream>
#include <string>
#include "String.h"

using namespace std;

class Date {
private:
	short _day = 1;
	short _month = 1;
	short _year = 1900;

public:
	enum enDateCompare { Before = -1, Equal = 0, After = 1 };

	Date() {
		*this = getSystemDate();
	}

	Date(string stringDate) {
		*this = dateToString(stringDate);
	}

	Date(short day, short month, short year) {
		_day = day;
		_month = month;
		_year = year;
	}

	Date(short dateOrderInYear, short year) {
		Date date1 = getDateFromDayOrderInYear(dateOrderInYear, year);
		_day = date1.day;
		_month = date1.month;
		_year = date1.year;
	}


	void setDay(short day) {
		_day = day;
	}

	short getDay() {
		return _day;
	}

	__declspec(property(get = getDay, put = setDay)) short day;


	void setMonth(short month) {
		_month = month;
	}

	short getMonth() {
		return _month;
	}

	__declspec(property(get = getMonth, put = setMonth)) short month;


	void setYear(short year) {
		_year = year;
	}

	short getYear() {
		return _year;
	}

	__declspec(property(get = getYear, put = setYear)) short year;


	void print() {
		cout << dateToString() << endl;
	}

	static Date getSystemDate() {
		time_t t = time(0);
		tm* now = localtime(&t);
		short day, month, year;
		year = now->tm_year + 1900;
		month = now->tm_mon + 1;
		day = now->tm_mday;
		return Date(day, month, year);
	}

	static string GetSystemDateTimeString() {
		//system datetime string
		time_t t = time(0);
		tm* now = localtime(&t);
		short Day, Month, Year, Hour, Minute, Second;

		Year = now->tm_year + 1900;
		Month = now->tm_mon + 1;
		Day = now->tm_mday;
		Hour = now->tm_hour;
		Minute = now->tm_min;
		Second = now->tm_sec;

		return (to_string(Day) + "/"
			+ to_string(Month) + "/"
			+ to_string(Year) + " - "
			+ to_string(Hour) + ":" + to_string(Minute)
			+ ":" + to_string(Second));
	}

	static bool isValidDate(Date date) {
		if (date.day < 1 || date.day > 31)
			return false;
		if (date.month < 1 || date.month > 12)
			return false;
		if (date.month == 2) {
			if (isLeapYear(date.year)) {
				if (date.day > 29)
					return false;
			}
			else {
				if (date.day > 28)
					return false;
			}
		}
		short daysInMonth = numberOfDaysInAMonth(date.month, date.year);
		if (date.day > daysInMonth)
			return false;
		return true;
	}

	bool isValid() {
		return isValidDate(*this);
	}


	static string dateToString(Date date) {
		return to_string(date.day) + "/" + to_string(date.month) + "/" + to_string(date.year);
	}

	string dateToString() {
		return dateToString(*this);
	}


	static bool isLeapYear(short year) {
		return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
	}

	bool isLeapYear() {
		return isLeapYear(_year);
	}


	static short numberOfDaysInAYear(short year) {
		return isLeapYear(year) ? 366 : 365;
	}

	short numberOfDaysInAYear() {
		return numberOfDaysInAYear(_year);
	}


	static short numberOfHoursInAYear(short year) {
		return numberOfDaysInAYear(year) * 24;
	}

	short numberOfHoursInAYear() {
		return numberOfHoursInAYear(_year);
	}


	static int numberOfMinutesInAYear(short year) {
		return numberOfHoursInAYear(year) * 60;
	}

	int numberOfMinutesInAYear() {
		return numberOfMinutesInAYear(_year);
	}


	static int numberOfSecondsInAYear(short year) {
		return numberOfMinutesInAYear(year) * 60;
	}

	int numberOfSecondsInAYear() {
		return numberOfSecondsInAYear(_year);
	}


	static short numberOfDaysInAMonth(short month, short year) {
		if (month < 1 || month > 12)
			return 0;
		int days[12] = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
		return (month == 2) ? (isLeapYear(year) ? 29 : 28) : days[month - 1];
	}

	short numberOfDaysInAMonth() {
		return numberOfDaysInAMonth(_month, _year);
	}


	static short numberOfHoursInAMonth(short month, short year) {
		return numberOfDaysInAMonth(month, year) * 24;
	}

	short numberOfHoursInAMonth() {
		return numberOfDaysInAMonth(_month, _year) * 24;
	}


	static int numberOfMinutesInAMonth(short month, short year) {
		return numberOfHoursInAMonth(month, year) * 60;
	}

	int numberOfMinutesInAMonth() {
		return numberOfHoursInAMonth(_month, _year) * 60;
	}


	static int numberOfSecondsInAMonth(short month, short year) {
		return numberOfMinutesInAMonth(month, year) * 60;
	}

	int numberOfSecondsInAMonth() {
		return numberOfMinutesInAMonth(_month, _year) * 60;
	}


	static short dayOfWeekOrder(short day, short month, short year) {
		short a, y, m;
		a = (14 - month) / 12;
		y = year - a;
		m = month + (12 * a) - 2;
		return (day + y + (y / 4) - (y / 100) + (y / 400) + ((31 * m) / 12)) % 7;
	}

	short dayOfWeekOrder() {
		return dayOfWeekOrder(_day, _month, _year);
	}


	static string dayShortName(short dayOfWeekOrder) {
		string arrDayNames[] = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
		return arrDayNames[dayOfWeekOrder];
	}

	static string dayShortName(short day, short month, short year) {
		string arrDayNames[] = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
		return arrDayNames[dayOfWeekOrder(day, month, year)];
	}

	string dayShortName() {
		string arrDayNames[] = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
		return arrDayNames[dayOfWeekOrder(_day, _month, _year)];
	}


	static string monthShortName(short monthNumber) {
		string months[12] = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
		return (months[monthNumber - 1]);
	}

	string monthShortName() {
		return monthShortName(_month);
	}


	static void printMonthCalendar(short month, short year) {
		int numberOfDays;
		int current = dayOfWeekOrder(1, month, year);
		numberOfDays = numberOfDaysInAMonth(month, year);
		printf("\n  _______________%s_______________\n\n", monthShortName(month).c_str());
		printf("  Sun  Mon  Tue  Wed  Thu  Fri  Sat\n");
		int i;
		for (i = 0; i < current; i++)
			printf("     ");
		for (int j = 1; j <= numberOfDays; j++) {
			printf("%5d", j);
			if (++i == 7) {
				i = 0;
				printf("\n");
			}
		}
		printf("\n  _________________________________\n");
	}

	void printMonthCalendar() {
		printMonthCalendar(_month, _year);
	}


	static void printYearCalendar(int year) {
		printf("\n  _________________________________\n\n");
		printf("           Calendar - %d\n", year);
		printf("  _________________________________\n");
		for (int i = 1; i <= 12; i++) {
			printMonthCalendar(i, year);
		}
	}

	void printYearCalendar() {
		printf("\n  _________________________________\n\n");
		printf("           Calendar - %d\n", _year);
		printf("  _________________________________\n");
		for (int i = 1; i <= 12; i++) {
			printMonthCalendar(i, _year);
		}
	}


	static short daysFromTheBeginningOfTheYear(short day, short month, short year) {
		short totalDays = 0;
		for (int i = 1; i <= month - 1; i++) {
			totalDays += numberOfDaysInAMonth(i, year);
		}
		totalDays += day;
		return totalDays;
	}

	short daysFromTheBeginningOfTheYear() {
		short totalDays = 0;
		for (int i = 1; i <= _month - 1; i++) {
			totalDays += numberOfDaysInAMonth(i, _year);
		}
		totalDays += _day;
		return totalDays;
	}


	static Date getDateFromDayOrderInYear(short dateOrderInYear, short year) {
		Date date;
		short remainingDays = dateOrderInYear;
		short monthDays = 0;
		date.year = year;
		date.month = 1;
		while (true) {
			monthDays = numberOfDaysInAMonth(date.month, year);
			if (remainingDays > monthDays) {
				remainingDays -= monthDays;
				date.month++;
			}
			else {
				date.day = remainingDays;
				break;
			}
		}
		return date;
	}


	void addDays(short days) {
		short remainingDays = days + daysFromTheBeginningOfTheYear(_day, _month, _year);
		short monthDays = 0;
		_month = 1;
		while (true) {
			monthDays = numberOfDaysInAMonth(_month, _year);
			if (remainingDays > monthDays) {
				remainingDays -= monthDays;
				_month++;
				if (_month > 12) {
					_month = 1;
					_year++;
				}
			}
			else {
				_day = remainingDays;
				break;
			}
		}
	}


	static bool isDate1BeforeDate2(Date date1, Date date2) {
		return (date1.year < date2.year) ? true : ((date1.year == date2.year) ? (date1.month < date2.month ? true : (date1.month == date2.month ? date1.day < date2.day : false)) : false);
	}

	bool isDateBeforeDate2(Date date2) {
		return isDate1BeforeDate2(*this, date2);
	}


	static bool isDate1EqualDate2(Date date1, Date date2) {
		return (date1.year == date2.year) ? ((date1.month == date2.month) ? ((date1.day == date2.day) ? true : false) : false) : false;
	}

	bool isDateEqualDate2(Date date2) {
		return isDate1EqualDate2(*this, date2);
	}


	static bool isLastDayInMonth(Date date) {
		return (date.day == numberOfDaysInAMonth(date.month, date.year));
	}


	bool isLastDayInMonth() {
		return isLastDayInMonth(*this);
	}

	static bool isLastMonthInYear(short month) {
		return (month == 12);
	}


	static Date addOneDay(Date date) {
		if (isLastDayInMonth(date)) {
			if (isLastMonthInYear(date.month)) {
				date.month = 1;
				date.day = 1;
				date.year++;
			}
			else {
				date.day = 1;
				date.month++;
			}
		}
		else {
			date.day++;
		}
		return date;
	}

	void addOneDay() {
		*this = addOneDay(*this);
	}


	static void swapDates(Date& date1, Date& date2) {
		Date tempDate;
		tempDate = date1;
		date1 = date2;
		date2 = tempDate;
	}


	static int getDifferenceInDays(Date date1, Date date2, bool includeEndDay = false) {
		int days = 0;
		short swapFlagValue = 1;
		if (!isDate1BeforeDate2(date1, date2)) {
			swapDates(date1, date2);
			swapFlagValue = -1;
		}
		while (isDate1BeforeDate2(date1, date2)) {
			days++;
			date1 = addOneDay(date1);
		}
		return includeEndDay ? ++days * swapFlagValue : days * swapFlagValue;
	}

	int getDifferenceInDays(Date date2, bool includeEndDay = false) {
		return getDifferenceInDays(*this, date2, includeEndDay);
	}


	static short calculateMyAgeInDays(Date dateOfBirth) {
		return getDifferenceInDays(dateOfBirth, Date::getSystemDate(), true);
	}


	static Date increaseDateByOneWeek(Date& date) {
		for (int i = 1; i <= 7; i++) {
			date = addOneDay(date);
		}
		return date;
	}

	void increaseDateByOneWeek() {
		increaseDateByOneWeek(*this);
	}


	Date increaseDateByXWeeks(short weeks, Date& date) {
		for (short i = 1; i <= weeks; i++) {
			date = increaseDateByOneWeek(date);
		}
		return date;
	}

	void increaseDateByXWeeks(short weeks) {
		increaseDateByXWeeks(weeks, *this);
	}


	Date increaseDateByOneMonth(Date& date) {
		if (date.month == 12) {
			date.month = 1;
			date.year++;
		}
		else {
			date.month++;
		}
		short numberOfDaysInCurrentMonth = numberOfDaysInAMonth(date.month, date.year);
		if (date.day > numberOfDaysInCurrentMonth) {
			date.day = numberOfDaysInCurrentMonth;
		}
		return date;
	}

	void increaseDateByOneMonth() {
		increaseDateByOneMonth(*this);
	}


	Date increaseDateByXDays(short days, Date& date) {
		for (short i = 1; i <= days; i++) {
			date = addOneDay(date);
		}
		return date;
	}

	void increaseDateByXDays(short days) {
		increaseDateByXDays(days, *this);
	}


	Date increaseDateByXMonths(short months, Date& date) {
		for (short i = 1; i <= months; i++) {
			date = increaseDateByOneMonth(date);
		}
		return date;
	}

	void increaseDateByXMonths(short months) {
		increaseDateByXMonths(months, *this);
	}


	static Date increaseDateByOneYear(Date& date) {
		date.year++;
		return date;
	}

	void increaseDateByOneYear() {
		increaseDateByOneYear(*this);
	}


	Date increaseDateByXYears(short years, Date& date) {
		date.year += years;
		return date;
	}

	void increaseDateByXYears(short years) {
		increaseDateByXYears(years, *this);
	}


	Date increaseDateByOneDecade(Date& date) {
		date.year += 10;
		return date;
	}

	void increaseDateByOneDecade() {
		increaseDateByOneDecade(*this);
	}


	Date increaseDateByXDecades(short decades, Date& date) {
		date.year += decades * 10;
		return date;
	}

	void increaseDateByXDecades(short decades) {
		increaseDateByXDecades(decades, *this);
	}


	Date increaseDateByOneCentury(Date& date) {
		date.year += 100;
		return date;
	}

	void increaseDateByOneCentury() {
		increaseDateByOneCentury(*this);
	}


	Date increaseDateByOneMillennium(Date& date) {
		date.year += 1000;
		return date;
	}

	void increaseDateByOneMillennium() {
		increaseDateByOneMillennium(*this);
	}


	static Date decreaseDateByOneDay(Date date) {
		if (date.day == 1) {
			if (date.month == 1) {
				date.month = 12;
				date.day = 31;
				date.year--;
			}
			else {
				date.month--;
				date.day = numberOfDaysInAMonth(date.month, date.year);
			}
		}
		else {
			date.day--;
		}
		return date;
	}

	void decreaseDateByOneDay() {
		decreaseDateByOneDay(*this);
	}


	static Date decreaseDateByOneWeek(Date& date) {
		for (int i = 1; i <= 7; i++) {
			date = decreaseDateByOneDay(date);
		}
		return date;
	}

	void decreaseDateByOneWeek() {
		decreaseDateByOneWeek(*this);
	}


	static Date decreaseDateByXWeeks(short weeks, Date& date) {
		for (short i = 1; i <= weeks; i++) {
			date = decreaseDateByOneWeek(date);
		}
		return date;
	}

	void decreaseDateByXWeeks(short weeks) {
		decreaseDateByXWeeks(weeks, *this);
	}


	static Date decreaseDateByOneMonth(Date& date) {
		if (date.month == 1) {
			date.month = 12;
			date.year--;
		}
		else {
			date.month--;
		}
		short numberOfDaysInCurrentMonth = numberOfDaysInAMonth(date.month, date.year);
		if (date.day > numberOfDaysInCurrentMonth) {
			date.day = numberOfDaysInCurrentMonth;
		}
		return date;
	}

	void decreaseDateByOneMonth() {
		decreaseDateByOneMonth(*this);
	}


	static Date decreaseDateByXDays(short days, Date& date) {
		for (short i = 1; i <= days; i++) {
			date = decreaseDateByOneDay(date);
		}
		return date;
	}

	void decreaseDateByXDays(short days) {
		decreaseDateByXDays(days, *this);
	}


	static Date decreaseDateByXMonths(short months, Date& date) {
		for (short i = 1; i <= months; i++) {
			date = decreaseDateByOneMonth(date);
		}
		return date;
	}

	void decreaseDateByXMonths(short months) {
		decreaseDateByXMonths(months, *this);
	}


	static Date decreaseDateByOneYear(Date& date) {
		date.year--;
		return date;
	}

	void decreaseDateByOneYear() {
		decreaseDateByOneYear(*this);
	}


	static Date decreaseDateByXYears(short years, Date& date) {
		date.year -= years;
		return date;
	}

	void decreaseDateByXYears(short years) {
		decreaseDateByXYears(years, *this);
	}


	static Date decreaseDateByOneDecade(Date& date) {
		date.year -= 10;
		return date;
	}

	void decreaseDateByOneDecade() {
		decreaseDateByOneDecade(*this);
	}


	static Date decreaseDateByXDecades(short decades, Date& date) {
		date.year -= decades * 10;
		return date;
	}

	void decreaseDateByXDecades(short decades) {
		decreaseDateByXDecades(decades, *this);
	}


	static Date decreaseDateByOneCentury(Date& date) {
		date.year -= 100;
		return date;
	}

	void decreaseDateByOneCentury() {
		decreaseDateByOneCentury(*this);
	}


	static Date decreaseDateByOneMillennium(Date& date) {
		date.year -= 1000;
		return date;
	}

	void decreaseDateByOneMillennium() {
		decreaseDateByOneMillennium(*this);
	}


	static short isEndOfWeek(Date date) {
		return dayOfWeekOrder(date.day, date.month, date.year) == 6;
	}

	short isEndOfWeek() {
		return isEndOfWeek(*this);
	}


	static bool isWeekEnd(Date date) {
		short dayIndex = dayOfWeekOrder(date.day, date.month, date.year);
		return (dayIndex == 5 || dayIndex == 6);
	}

	bool isWeekEnd() {
		return isWeekEnd(*this);
	}


	static bool isBusinessDay(Date date) {
		return !isWeekEnd(date);
	}

	bool isBusinessDay() {
		return isBusinessDay(*this);
	}


	static short daysUntilTheEndOfWeek(Date date) {
		return 6 - dayOfWeekOrder(date.day, date.month, date.year);
	}

	short daysUntilTheEndOfWeek() {
		return daysUntilTheEndOfWeek(*this);
	}


	static short daysUntilTheEndOfMonth(Date date1) {
		Date endOfMonthDate;
		endOfMonthDate.day = numberOfDaysInAMonth(date1.month, date1.year);
		endOfMonthDate.month = date1.month;
		endOfMonthDate.year = date1.year;
		return getDifferenceInDays(date1, endOfMonthDate, true);
	}

	short daysUntilTheEndOfMonth() {
		return daysUntilTheEndOfMonth(*this);
	}


	static short daysUntilTheEndOfYear(Date date1) {
		Date endOfYearDate;
		endOfYearDate.day = 31;
		endOfYearDate.month = 12;
		endOfYearDate.year = date1.year;
		return getDifferenceInDays(date1, endOfYearDate, true);
	}

	short daysUntilTheEndOfYear() {
		return daysUntilTheEndOfYear(*this);
	}


	static short calculateBusinessDays(Date dateFrom, Date dateTo) {
		short days = 0;
		while (isDate1BeforeDate2(dateFrom, dateTo)) {
			if (isBusinessDay(dateFrom))
				days++;
			dateFrom = addOneDay(dateFrom);
		}
		return days;
	}

	static short calculateVacationDays(Date dateFrom, Date dateTo) {
		return calculateBusinessDays(dateFrom, dateTo);
	}

	static Date calculateVacationReturnDate(Date dateFrom, short vacationDays) {
		short weekEndCounter = 0;
		for (short i = 1; i <= vacationDays; i++) {
			if (isWeekEnd(dateFrom))
				weekEndCounter++;
			dateFrom = addOneDay(dateFrom);
		}
		for (short i = 1; i <= weekEndCounter; i++)
			dateFrom = addOneDay(dateFrom);
		return dateFrom;
	}


	static bool isDate1AfterDate2(Date date1, Date date2) {
		return (!isDate1BeforeDate2(date1, date2) && !isDate1EqualDate2(date1, date2));
	}

	bool isDateAfterDate2(Date date2) {
		return isDate1AfterDate2(*this, date2);
	}


	static enDateCompare compareDates(Date date1, Date date2) {
		if (isDate1BeforeDate2(date1, date2))
			return enDateCompare::Before;
		if (isDate1EqualDate2(date1, date2))
			return enDateCompare::Equal;
		return enDateCompare::After;
	}

	enDateCompare compareDates(Date date2) {
		return compareDates(*this, date2);
	}
};