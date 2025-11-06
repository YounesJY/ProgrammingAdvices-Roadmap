#pragma once
#pragma warning(disable : 4996)

#include <iostream>
#include "../stringClassProject/String.h"
#include <vector>
#include <iomanip>

using namespace std;

class Date {
private:
	enum _daysOfTheWeek {
		sunday,
		monday,
		tuesday,
		wednesday,
		thursday,
		friday,
		saturday
	};

	enum comparedDatesResult { before = -1, equal = 0, after = 1 };

	short _year;
	short _month;
	short _day;

public:

	Date(short year, short month, short day) {
		this->_year = year;
		this->_month = month;
		this->_day = day;
	}

	Date(string dateInStringFormat, string delimiter = "/") {
		*this = stringToDate(dateInStringFormat, delimiter);
	}

	Date(short year, short daysFromTheStartOfYear) {
		*this = getDateFromTotalNumberOfDays(year, daysFromTheStartOfYear);
	}

	Date() {
		*this = getSystemDate();
	}


	short getYear() {
		return _year;
	}

	void setYear(short year) {
		_year = year;
	}

	__declspec(property(get = getYear, put = setYear)) short year;


	short getMonth() {
		return _month;
	}

	void setMonth(short month) {
		_month = month;
	}

	__declspec(property(get = getMonth, put = setMonth)) short month;


	short getDay() {
		return _day;
	}

	void setDay(short day) {
		_day = day;
	}

	__declspec(property(get = getDay, put = setDay)) short day;


	void print(char delimiter = '/') {
		cout << dateToString(delimiter) << endl;
	}


	static bool isLeapYear(short year) {
		return ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0));
	}

	bool isLeapYear() {
		return isLeapYear(_year);
	}


	static bool isValidDate(Date date) {
		return ((1 <= date.month && date.month <= 12) ? (1 <= date.day && date.day <= daysPerMonth(date.year, date.month) ? true : false) : false);
		//return ((1 > date.month || date.month > 12) ? false : ((1 > date.day || date.day > daysPerMonth(date.year, date.month)) ? false : true));
	}

	bool isValidDate() {
		return isValidDate(*this);
	}

	static	Date getSystemDate() {
		short year, month, day;

		time_t t = time(0);
		tm* now = localtime(&t);

		year = now->tm_year + 1900;
		month = now->tm_mon + 1;
		day = now->tm_mday;

		return Date(year, month, day);
	}


	static string dateToString(short year, short month, short day, char delimiter = '/') {
		if (!isValidDate(Date(year, month, day)))
			return "Invalid Date!";

		return (to_string(day) + delimiter + to_string(month) + delimiter + to_string(year));
	}

	static string dateToString(Date date, char delimiter = '/') {
		if (!isValidDate(date))
			return "Invalid Date!";

		return (to_string(date.day) + delimiter + to_string(date.month) + delimiter + to_string(date.year));
	}

	string dateToString(char delimiter = '/') {
		return dateToString(*this, delimiter);
	}


	static Date stringToDate(string dateInStringFormat, string delimiter = "/") {
		Date date;
		vector<string> dateElements;

		dateElements = String::splitStringWords(dateInStringFormat, delimiter);

		date.day = stoi(dateElements[0]);
		date.month = stoi(dateElements[1]);
		date.year = stoi(dateElements[2]);

		return date;
	}


	static short daysPerMonth(short year, short month) {
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

	short daysPerMonth() {
		return daysPerMonth(_year, _month);
	}


	static string getMonthName(short month) {
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

	string getMonthName() {
		return getMonthName(_month);
	}


	static short getDayOfWeekOrderFromDate(short year, short month, short day) {
		short y, a, m, dayOrder;

		a = (14 - month) / 12;
		y = year - a;
		m = month + (12 * a) - 2;
		dayOrder = (day + y + (y / 4) - (y / 100) + (y / 400) + ((31 * m) / 12)) % 7;

		return dayOrder;
	}

	short getDayOfWeekOrderFromDate() {
		return getDayOfWeekOrderFromDate(_year, _month, _day);
	}


	static int hoursPerMonth(short year, short month) {
		return (24 * daysPerMonth(year, month));
	}

	int hoursPerMonth() {
		return hoursPerMonth(_year, _month);
	}


	static int minutesPerMonth(short year, short month) {
		return (60 * hoursPerMonth(year, month));
	}

	int minutesPerMonth() {
		return minutesPerMonth(_year, _month);
	}


	static int secondsPerMonth(short year, short month) {
		return (60 * minutesPerMonth(year, month));
	}

	int secondsPerMonth() {
		return secondsPerMonth(_year, _month);
	}

private:
	static void printWeekDaysHeader() {

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

	static short getCalendarFirstDayPosition(short year, short month) {
		short firstDay = getDayOfWeekOrderFromDate(year, month, 1);
		return (5 * (firstDay + 1));
	}

public:
	static void printMonthDaysForCalendar(short year, short month) {
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
			if (currentDay == _daysOfTheWeek::saturday) {
				cout << endl;
				currentDay = _daysOfTheWeek::sunday;
			}
			else
				++currentDay;

			cout << setw(5) << day;
		}

		cout << endl;
	}

	void printMonthDaysForCalendar() {
		printMonthDaysForCalendar(_year, _month);
	}


	void printMonthCalendar(short year, short month) {
		cout << "  " << "---------------" << getMonthName(month) << "---------------" << endl;
		printWeekDaysHeader();
		printMonthDaysForCalendar(year, month);
		cout << "  " << "---------------------------------" << endl;
	}

	void printMonthCalendar() {
		printMonthCalendar(_year, _month);
	}
	

	static short getTotalNumberOfDaysFromThebeginningOfYear(short year, short currentMonth, short currentDay) {
		short totalNumberOfDays = 0;

		for (short month = 1; month < currentMonth; month++)
			totalNumberOfDays += daysPerMonth(year, month);

		totalNumberOfDays += currentDay;

		return totalNumberOfDays;
	}

	short getTotalNumberOfDaysFromThebeginningOfYear() {
		return getTotalNumberOfDaysFromThebeginningOfYear(_year, _month, _day);
	}


	string getDateFromTotalNumberOfDays(short year, short totalNumberOfDays) {
		short currentMonth = 1;

		while (totalNumberOfDays > daysPerMonth(year, currentMonth)) {
			totalNumberOfDays -= daysPerMonth(year, currentMonth);
			currentMonth++;

			if (currentMonth > 12) {
				year++;
				currentMonth = 1;
			}
		}

		return dateToString(year, currentMonth, totalNumberOfDays);
	}

	
	static bool isDatesAreEqual(Date date1, Date date2) {
		return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (((date1.day == date2.day)) ? true : false) : false) : false);
	}

	bool isDatesAreEqual(Date date) {
		return isDatesAreEqual(*this, date);
	}

	
	static string getStringFormatOfDate(Date date, char delimiter = '/') {
		return (to_string(date.day) + delimiter + to_string(date.month) + delimiter + to_string(date.year));
	}

	string getStringFormatOfDate(char delimiter = '/') {
		return getStringFormatOfDate(*this, delimiter);
	}


	static bool isLastMonthInYear(short month) {
		return (month == 12);
	}

	bool isLastMonthInYear() {
		return isLastMonthInYear(_month);
	}


	static bool isLastDayInMonth(Date date) {
		return (daysPerMonth(date.year, date.month) == date.day);
	}

	bool isLastDayInMonth() {
		return isLastDayInMonth(*this);
	}


	static Date IncreaseDateByOneDay(Date date) {
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

	Date IncreaseDateByOneDay() {
		return IncreaseDateByOneDay(*this);
	}


	static bool isDate1LessThanDate2(Date date1, Date date2) {
		return ((date1.year == date2.year) ? ((date1.month == date2.month) ? (date1.day < date2.day) : (date1.month < date2.month)) : (date1.year < date2.year));
	}

	bool isCurrentDateLessThanDate(Date date) {
		return isDate1LessThanDate2(*this, date);
	}
	

	static void swapDates(Date& date1, Date& date2) {
		Date tempDate;

		tempDate = date1;
		date1 = date2;
		date2 = tempDate;
	}
	

	static 	short getTheDifferenceBetweenTwoDates(Date date1, Date date2, bool isLastDayIncluded = false) {
		short differenceBetweenDates = 0;
		short swappingDatesflag = false;

		if (!isDate1LessThanDate2(date1, date2))
		{
			swapDates(date1, date2);
			swappingDatesflag = -1;
		}

		while (isDate1LessThanDate2(date1, date2))
		{
			date1 = IncreaseDateByOneDay(date1);
			++differenceBetweenDates;
		}

		return ((isLastDayIncluded ? ++differenceBetweenDates : differenceBetweenDates) * swappingDatesflag);

	}

	short getTheDifferenceBetweenTwoDates(Date date, bool isLastDayIncluded = false) {
		return getTheDifferenceBetweenTwoDates(*this, date, isLastDayIncluded);
	}


	static short compareDates(Date date1, Date date2) {
		return (isDate1LessThanDate2(date1, date2) ? comparedDatesResult::before : ((isDatesAreEqual(date1, date2)) ? comparedDatesResult::equal : comparedDatesResult::after));
	}

	short compareToDate(Date date) {
		return compareDates(*this, date);
	}

};

