// ProgrammingAdvices.com
// Mohammed Abu-Hadhoud

#pragma once

#include <iostream>
#include <string>

#include "String.h"
#include "Input.h"
#include "Date.h"

class InputValidate {
public:
	template<typename T>
	static bool isNumberBetween(T number, T from, T to) {
		return (number >= from && number <= to);
	}

	template<typename T>
	static T readNumber(string errorMessage = "Invalid Number, Enter again: ") {
		T number;
		while (!(cin >> number)) {
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			cout << errorMessage;
		}
		return number;
	}

	template<typename T>
	static T readNumberBetween(T from, T to, string errorMessage = "Number is not within range, Enter again:\n") {
		T number = readNumber<T>(errorMessage);
		while (!isNumberBetween<T>(number, from, to)) {
			cout << errorMessage;
			number = readNumber<T>(errorMessage);
		}

		return number;
	}


	static bool isDateBetween(Date date, Date from, Date to) {
		// Date >= from && Date <= to
		if ((Date::isDate1AfterDate2(date, from) || Date::isDate1EqualDate2(date, from)) &&
			(Date::isDate1BeforeDate2(date, to) || Date::isDate1EqualDate2(date, to))) {
			return true;
		}

		// Date >= to && Date <= from
		if ((Date::isDate1AfterDate2(date, to) || Date::isDate1EqualDate2(date, to)) &&
			(Date::isDate1BeforeDate2(date, from) || Date::isDate1EqualDate2(date, from))) {
			return true;
		}

		return false;
	}

	static bool isValidDate(Date date) {
		return Date::isValidDate(date);
	}


	static string readString(string message = "") {
		return Input::readString(message);
	}

};