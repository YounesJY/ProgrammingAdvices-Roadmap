#pragma once

#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "InputValidate.h"
#include "Currency.h"

using namespace std;

class CurrenciesListScreen :protected Screen {
private:
	static void _printCurrencyRecordLine(Currency currency) {
		cout << setw(8) << left << "";
		cout << "| " << setw(30) << left << currency.currencyName();
		cout << "| " << setw(15) << left << currency.currencyCode();
		cout << "| " << setw(30) << left << currency.country();
		cout << "| " << setw(15) << left << currency.rate();
	}

public:
	static void showCurrenciesList() {
		vector<Currency> vCurrencies = Currency::getCurrenciesList();
		string title = "\t  Currencies List Screen";
		string subTitle = "\t    (" + to_string(vCurrencies.size()) + ") Currencies.";

		_drawScreenHeader(title, subTitle);

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;
		cout << setw(8) << left << "";
		cout << "| " << left << setw(30) << "Currency Name";
		cout << "| " << left << setw(15) << "Currency Code";
		cout << "| " << left << setw(30) << "Country";
		cout << "| " << left << setw(15) << "Rate";
		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;

		if (vCurrencies.size() == 0)
			cout << "\t\t\t\tNo Currencies Available In the System!";
		else
			for (const Currency& currency : vCurrencies) {
				_printCurrencyRecordLine(currency);
				cout << endl;
			}

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;
	}
};