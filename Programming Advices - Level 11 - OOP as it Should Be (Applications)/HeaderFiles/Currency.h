#pragma once

#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include "String.h"

class Currency {
private:
	enum enMode { emptyMode = 0, updateMode = 1 };
	enMode _mode;

	string _country;
	string _currencyCode;
	string _currencyName;
	float _rate;

	static Currency _convertLineToCurrencyObject(string line, string separator = "#//#") {
		vector<string> vCurrencyData = String::split(line, separator);
		return Currency(enMode::updateMode, vCurrencyData[0], vCurrencyData[1], vCurrencyData[2], stod(vCurrencyData[3]));
	}

	static string _convertCurrencyObjectToLine(Currency currency, string separator = "#//#") {

		string stCurrencyRecord = "";
		stCurrencyRecord += currency.country() + separator;
		stCurrencyRecord += currency.currencyCode() + separator;
		stCurrencyRecord += currency.currencyName() + separator;
		stCurrencyRecord += to_string(currency.rate());

		return stCurrencyRecord;
	}

	static vector <Currency> _loadCurrenciesDataFromFile() {
		vector <Currency> vCurrencies;
		fstream myFile;

		myFile.open("Currencies.txt", ios::in); //read mode

		if (myFile.is_open()) {
			string line;
			while (getline(myFile, line)) {
				Currency currency = _convertLineToCurrencyObject(line);
				vCurrencies.push_back(currency);
			}

			myFile.close();
		}

		return vCurrencies;
	}

	static void _saveCurrencyDataToFile(vector <Currency> vCurrencies) {
		fstream myFile;
		string dataLine;

		myFile.open("Currencies.txt", ios::out); //overwrite

		if (myFile.is_open()) {
			for (Currency currency : vCurrencies) {
				dataLine = _convertCurrencyObjectToLine(currency);
				myFile << dataLine << endl;
			}

			myFile.close();
		}
	}

	void _update() {
		vector <Currency> vCurrencies;
		vCurrencies = _loadCurrenciesDataFromFile();

		for (Currency& currency : vCurrencies) {
			if (currency.currencyCode() == currencyCode()) {
				currency = *this;
				break;
			}
		}

		_saveCurrencyDataToFile(vCurrencies);
	}

	static Currency _getEmptyCurrencyObject() {
		return Currency(enMode::emptyMode, "", "", "", 0);
	}

public:

	Currency(enMode mode, string country, string currencyCode, string currencyName, float rate) {
		this->_mode = mode;
		this->_country = country;
		this->_currencyCode = currencyCode;
		this->_currencyName = currencyName;
		this->_rate = rate;
	}

	static vector <Currency> getAllUSDRates() {
		return _loadCurrenciesDataFromFile();
	}

	bool isEmpty() {
		return (_mode == enMode::emptyMode);
	}

	string country() {
		return _country;
	}

	string currencyCode() {
		return _currencyCode;
	}

	string currencyName() {
		return _currencyName;
	}

	void updateRate(float newRate) {
		_rate = newRate;
		_update();
	}

	float rate() {
		return _rate;
	}

	static Currency findByCode(string currencyCode) {
		fstream myFile;

		if (currencyCode.size() != 3)
			return _getEmptyCurrencyObject();

		myFile.open("Currencies.txt", ios::in); //read mode

		if (myFile.is_open()) {
			string line;
			currencyCode = String::upperAllString(currencyCode);

			while (getline(myFile, line)) {
				Currency currency = _convertLineToCurrencyObject(line);
				if (currencyCode == String::upperAllString(currency.currencyCode())) {
					myFile.close();
					return currency;
				}
			}

			myFile.close();
		}

		return _getEmptyCurrencyObject();
	}

	static Currency findByCountry(string country) {
		fstream myFile;
		myFile.open("Currencies.txt", ios::in); //read mode

		if (myFile.is_open()) {
			country = String::upperAllString(country);
			string line;
			while (getline(myFile, line)) {
				Currency currency = _convertLineToCurrencyObject(line);
				if (country == String::upperAllString(currency.country())) {
					myFile.close();
					return currency;
				}
			}
			myFile.close();
		}

		return _getEmptyCurrencyObject();
	}

	static bool isCurrencyExist(string currencyCode) {
		return !(Currency::findByCode(currencyCode).isEmpty());
	}

	static vector <Currency> getCurrenciesList() {
		return _loadCurrenciesDataFromFile();
	}


	static float convertToUSD(Currency sourceCurrency, float amount) {
		return (amount / sourceCurrency.rate());
	}

	float convertToUSD(float amount) {
		return convertToUSD(*this, amount);
	}


	static float convertFromUSD(Currency destinationCurrency, float amount) {
		return (amount * destinationCurrency.rate());
	}

	float convertFromUSD(float amount) {
		return convertFromUSD(*this, amount);
	}


	static float convert(Currency sourceCurrency, Currency destinationCurrency, float amountToExchange) {
		if (String::upperAllString(sourceCurrency.currencyCode()) == String::upperAllString(destinationCurrency.currencyCode()))
			return amountToExchange;

		if (String::upperAllString(destinationCurrency.currencyCode()) == "USD")
			return convertToUSD(sourceCurrency, amountToExchange);

		return convertFromUSD(destinationCurrency, convertToUSD(sourceCurrency, amountToExchange));
	}


	float convert(Currency destinationCurrency, float amountToExchange) {
		return convert(*this, destinationCurrency, amountToExchange);
	}

};
