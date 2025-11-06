#pragma once

#include <iostream>
#include <iomanip>

#include "String.h"
#include "Screen.h"
#include "InputValidate.h"
#include "Currency.h"


using namespace std;

class CurrencyCalculatorScreen :protected Screen {
private:
	static string _readCurrencyCode(string message = "") {
		string currencyCode;

		currencyCode = InputValidate::readString(message);
		while (!Currency::isCurrencyExist(currencyCode))
			currencyCode = InputValidate::readString("\Currecny code is not found, choose another one :");

		return currencyCode;
	}

	static void _printCurrencyCard(Currency currency) {
		cout << "\t Currency Card:" << endl;
		cout << "\t ______________________________________" << endl;
		cout << "\t Country Name  : " << currency.country() << endl;
		cout << "\t Currency Code : " << currency.currencyCode() << endl;
		cout << "\t Currency Name : " << currency.currencyName() << endl;
		cout << "\t Rate (1$)  =  : " << currency.rate() << endl;
		cout << "\t ______________________________________" << endl;
	}

	static string _convert(Currency sourceCurrency, Currency destinationCurrency, float amountToExchange) {
		return (
			to_string(amountToExchange)
			+ " " + sourceCurrency.currencyCode()
			+ " " + "="
			+ " " + to_string(sourceCurrency.convert(destinationCurrency, amountToExchange))
			+ " " + destinationCurrency.currencyCode()
			);
	}

	static void _convertCurrency(Currency sourceCurrency, Currency destinationCurrency, float amountToExchange) {
		cout << "\t == Convert From " << sourceCurrency.currencyCode() << " ==" << endl;
		_printCurrencyCard(sourceCurrency);
		cout << _convert(sourceCurrency, Currency::findByCode("USD"), amountToExchange) << endl << endl;

		if (String::upperAllString(destinationCurrency.currencyCode()) == "USD")
			return;

		cout << "\t == Converting From USD To " << destinationCurrency.currencyCode() << " ==" << endl;
		_printCurrencyCard(destinationCurrency);
		cout << _convert(sourceCurrency, Currency::findByCode(destinationCurrency.currencyCode()), amountToExchange) << endl << endl;
	}

public:
	static void showCurrencyCalculatorScreen() {
		string srcCurrencyCode;
		string dstCurrencyCode;
		float amountToExchange;
		Input::choice performAgain;

		while (true) {
			system("cls");
			_drawScreenHeader("Currency Calculator Screen");

			srcCurrencyCode = _readCurrencyCode("\nPlease enter the [source] currency code: ");
			dstCurrencyCode = _readCurrencyCode("\nPlease enter the [destination] currency code: ");
			amountToExchange = Input::readDouble("\nPlease enter the amount to exchange: ");

			Currency sourceCurrency = Currency::findByCode(srcCurrencyCode);
			Currency destinationCurrency = Currency::findByCode(dstCurrencyCode);

			_convertCurrency(sourceCurrency, destinationCurrency, amountToExchange);

			performAgain = Input::getChoice("\nDo you want to perform another calculation (yes / no) ? ", false);
			if (performAgain == Input::choice::no)
				break;
		}

	}
};