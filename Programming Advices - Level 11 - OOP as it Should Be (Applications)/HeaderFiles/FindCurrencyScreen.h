#pragma once

#include <iostream>
#include <iomanip>

#include "InputValidate.h"
#include "Currency.h"
#include "Screen.h"

using namespace std;

class FindCurrencyScreen :protected Screen {
private:
	enum enFindBy { code = 1, country = 2 };
	static void _printCurrencyCard(Currency currency) {
		cout << "\nCurrency Card:";
		cout << "\n___________________";
		cout << "\nCountry Name  : " << currency.country();
		cout << "\nCurrency Code : " << currency.currencyCode();
		cout << "\nCurrency Name : " << currency.currencyName();
		cout << "\nRate (1$)  =  : " << currency.rate();
		cout << "\n___________________";
	}

	static Currency _findCurreny() {
		enFindBy answer;

		cout << " Find by: \n";
		cout << "	[1] Code\n";
		cout << "	[2] Country\n";

		answer = (enum enFindBy)Input::readIntegerBetween(1, 2, " Please choose between [1] and [2]: ");

		if (answer == enFindBy::code)
			return Currency::findByCode((Input::readString(" Please enter the currency code: ")));
		else
			return Currency::findByCountry((Input::readString(" Please enter the country name: ")));

	}

public:
	static void showFindCurrencyScreen() {
		_drawScreenHeader("\tFind Currency Screen");
		Currency curreny = _findCurreny();

		if (!curreny.isEmpty()) {
			cout << "\Currency Found :-)\n";
			_printCurrencyCard(curreny);
		}
		else
			cout << "\Currency Was not Found :-(\n";
	}
};