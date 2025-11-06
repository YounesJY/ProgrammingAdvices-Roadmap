#pragma once

#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "InputValidate.h"


using namespace std;

class CurrencyCalculatorScreen :protected Screen {
private:
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
	static void showCurrencyCalculatorScreen() {
		system("cls");
		_drawScreenHeader("    Currancy Exhange Main Screen");

		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t\t  Currency Exhange Menue\n";
		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t[1] List Currencies.\n";
		cout << setw(37) << left << "" << "\t[2] Find Currency.\n";
		cout << setw(37) << left << "" << "\t[3] Update Rate.\n";
		cout << setw(37) << left << "" << "\t[4] Currency Calculator.\n";
		cout << setw(37) << left << "" << "\t[5] Main Menue.\n";
		cout << setw(37) << left << "" << "===========================================\n";

		performCurrenciesMainMenueOptions((enCurrenciesMainMenueOptions)readCurrenciesMainMenueOptions());
	}
};