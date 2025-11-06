#pragma once
#include <iostream>

#include "Screen.h"
#include "Person.h"
#include "BankClient.h"
#include "InputValidate.h"

class UpdateCurrencyRateScreen :protected Screen {
private:
	static void _updateCurrencyRate(Currency& currency) {

	}

	static void _printCurrencyCard(Currency currency) {
		cout << "\nCurrency Card:";
		cout << "\n___________________";
		cout << "\nCountry Name  : " << currency.country();
		cout << "\nCurrency Code : " << currency.currencyCode();
		cout << "\nCurrency Name : " << currency.currencyName();
		cout << "\nRate (1$)  =  : " << currency.rate();
		cout << "\n___________________";
	}

public:
	static void showUpdateCurrencyRateScreen() {
		string currencyCode = "";
		Input::choice answer;

		_drawScreenHeader("\tUpdate Currency Screen");

		currencyCode = InputValidate::readString("\nPlease enter currency code: ");
		while (!Currency::isCurrencyExist(currencyCode))
			currencyCode = InputValidate::readString("\Currecny code is not found, choose another one :");

		Currency currency = Currency::findByCode(currencyCode);
		_printCurrencyCard(currency);

		answer = Input::getChoice("\nAre you sure you want to update the currency rate this client (yes / no) ? ", false);
		if (answer == Input::choice::yes) {
			cout << "\nUpdate Currency Rate";
			cout << "\n___________________";
			cout << "\nEnter the new rate: ";
			currency.updateRate(InputValidate::readNumber<float>());
			cout << "\nCurrency rate updated Successfully :-)\n";
			cout << "\n___ ________________";
			_printCurrencyCard(currency);
		}
		else
			cout << "\nOperation was cancelled.\n";

	}
};

