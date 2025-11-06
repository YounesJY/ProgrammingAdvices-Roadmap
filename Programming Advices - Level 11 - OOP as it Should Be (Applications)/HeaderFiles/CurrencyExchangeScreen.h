#pragma once

#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "InputValidate.h"
#include "CurrenciesListScreen.h"
#include "FindCurrencyScreen.h"
#include "UpdateCurrencyRateScreen.h"
#include "CurrencyCalculatorScreen.h"

using namespace std;

class CurrencyExchangeScreen :protected Screen {
private:
	enum enCurrenciesMainMenueOptions {
		listCurrencies = 1, findCurrency = 2, updateCurrencyRate = 3, currencyCalculator = 4, mainMenu = 5
	};

	static short readCurrenciesMainMenueOptions() {
		cout << setw(37) << left << "" << "Choose what do you want to do? [1 to 5]? ";
		return InputValidate::readNumberBetween<short>(1, 5, "Enter Number between 1 to 5? ");

	}

	static void goBackToCurrenciesMenu() {
		cout << "\n\nPress any key to go back to Currencies Menue...";
		system("pause>0");
		showCurrencyExchangeMenu();
	}

	static void showCurrenciesListScreen() {
		//	cout << "\nCurriencies List Screen Will Be Here.\n";
		CurrenciesListScreen::showCurrenciesList();
	}

	static void showFindCurrencyScreen() {
		//	cout << "\nFind Currency Screen Will Be Here.\n";
		FindCurrencyScreen::showFindCurrencyScreen();
	}

	static void showUpdateCurrencyRateScreen() {
		//	cout << "\nUpdate Currency Rate Screen Will Be Here.\n";
		UpdateCurrencyRateScreen::showUpdateCurrencyRateScreen();
	}

	static void showCurrencyCalculatorScreen() {
		//	cout << "\nCurrency Calculator Screen Will Be Here.\n";
		CurrencyCalculatorScreen::showCurrencyCalculatorScreen();
	}

	static void performCurrenciesMainMenueOptions(enCurrenciesMainMenueOptions currenciesMainMenueOptions) {
		system("cls");

		switch (currenciesMainMenueOptions) {
		case enCurrenciesMainMenueOptions::listCurrencies: {
			showCurrenciesListScreen();
			goBackToCurrenciesMenu();
			break;
		}
		case enCurrenciesMainMenueOptions::findCurrency: {
			showFindCurrencyScreen();
			goBackToCurrenciesMenu();
			break;
		}
		case enCurrenciesMainMenueOptions::updateCurrencyRate: {
			showUpdateCurrencyRateScreen();
			goBackToCurrenciesMenu();
			break;
		}
		case enCurrenciesMainMenueOptions::currencyCalculator: {
			showCurrencyCalculatorScreen();
			goBackToCurrenciesMenu();
			break;
		}
		case enCurrenciesMainMenueOptions::mainMenu: {
			//do nothing here the main screen will handle it :-) ;
		}
		}
	}

public:
	static void showCurrencyExchangeMenu() {
		system("cls");
		_drawScreenHeader("Currency Exchange Main Screen");

		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t\t  Currency Exchange Menue\n";
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