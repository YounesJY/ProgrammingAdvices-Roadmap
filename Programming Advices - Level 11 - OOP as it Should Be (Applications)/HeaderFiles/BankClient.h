#pragma once
#include <iostream>
#include <string>
#include <vector>
#include <fstream>

#include "String.h"
#include "Person.h"
#include "Global.h"

using namespace std;

class BankClient : public Person {
private:
	enum enMode { EmptyMode = 0, UpdateMode = 1, AddNewMode = 2 };

	enMode _mode;
	string _accountNumber;
	string _pinCode;
	float _accountBalance;
	bool _markedForDelete = false;

	static BankClient _convertLineToClientObject(string line, string separator = "#//#") {
		vector<string> vClientData = String::split(line, separator);
		return BankClient(enMode::UpdateMode, vClientData[0], vClientData[1], vClientData[2],
			vClientData[3], vClientData[4], vClientData[5], stod(vClientData[6]));
	}

	static string _convertClientObjectToLine(BankClient client, string separator = "#//#") {
		return client.firstName + separator + client.lastName + separator + client.email + separator +
			client.phone + separator + client.accountNumber() + separator + client.pinCode + separator +
			to_string(client.accountBalance);
	}

	static vector<BankClient> _loadClientsDataFromFile() {
		vector<BankClient> vClients;
		fstream myFile;
		myFile.open("Clients.txt", ios::in);

		if (myFile.is_open()) {
			string line;
			while (getline(myFile, line)) {
				BankClient client = _convertLineToClientObject(line);
				vClients.push_back(client);
			}
			myFile.close();
		}
		return vClients;
	}

	static void _saveClientsDataToFile(vector<BankClient> vClients) {
		fstream myFile;
		myFile.open("Clients.txt", ios::out);

		if (myFile.is_open()) {
			for (BankClient c : vClients) {
				if (!c.markedForDeleted()) {
					myFile << _convertClientObjectToLine(c) << endl;
				}
			}
			myFile.close();
		}
	}

	void _update() {
		vector<BankClient> vClients = _loadClientsDataFromFile();
		for (BankClient& c : vClients) {
			if (c.accountNumber() == accountNumber()) {
				c = *this;
				break;
			}
		}
		_saveClientsDataToFile(vClients);
	}

	void _addNew() {
		_addDataLineToFile(_convertClientObjectToLine(*this));
	}

	void _addDataLineToFile(string stDataLine) {
		fstream myFile;
		myFile.open("Clients.txt", ios::out | ios::app);
		if (myFile.is_open()) {
			myFile << stDataLine << endl;
			myFile.close();
		}
	}

	static BankClient _getEmptyClientObject() {
		return BankClient(enMode::EmptyMode, "", "", "", "", "", "", 0);
	}

public:
	struct TransferRecord {
		string dateTime;
		string sourceAccount;
		string destinationAccount;
		double transferedAmount = 0;
		double sourceBalance = 0;
		double destinationBalance = 0;
		string username;
	};

	enum enSaveResults { svFaildEmptyObject = 0, svSucceeded = 1, svFaildAccountNumberExists = 2 };

	BankClient(enMode mode, string firstName, string lastName, string email, string phone, string accountNumber, string pinCode, float accountBalance) :
		Person(firstName, lastName, email, phone) {
		_mode = mode;
		_accountNumber = accountNumber;
		_pinCode = pinCode;
		_accountBalance = accountBalance;
	}

	bool isEmpty() { return (_mode == enMode::EmptyMode); }

	bool markedForDeleted() { return _markedForDelete; }


	string accountNumber() { return _accountNumber; }


	void setPinCode(string pinCode) { _pinCode = pinCode; }

	string getPinCode() { return _pinCode; }

	__declspec(property(get = getPinCode, put = setPinCode)) string pinCode;


	void setAccountBalance(float accountBalance) { _accountBalance = accountBalance; }

	float getAccountBalance() { return _accountBalance; }

	__declspec(property(get = getAccountBalance, put = setAccountBalance)) float accountBalance;


	/*
   No UI Related code iside object.

	void print() {
		cout << "\nClient Card:";
		cout << "\n___________________";
		cout << "\nFirstName   : " << firstName;
		cout << "\nLastName    : " << lastName;
		cout << "\nFull Name   : " << fullName();
		cout << "\nEmail       : " << email;
		cout << "\nPhone       : " << phone;
		cout << "\nAcc. Number : " << _accountNumber;
		cout << "\nPassword    : " << _pinCode;
		cout << "\nBalance     : " << _accountBalance;
		cout << "\n___________________\n";
	}
	*/

	static BankClient find(string accountNumber) {
		fstream myFile;
		myFile.open("Clients.txt", ios::in);

		if (myFile.is_open()) {
			string line;
			while (getline(myFile, line)) {
				BankClient client = _convertLineToClientObject(line);
				if (client.accountNumber() == accountNumber) {
					myFile.close();
					return client;
				}
			}
			myFile.close();
		}
		return _getEmptyClientObject();
	}

	static BankClient find(string accountNumber, string pinCode) {
		fstream myFile;
		myFile.open("Clients.txt", ios::in);

		if (myFile.is_open()) {
			string line;
			while (getline(myFile, line)) {
				BankClient client = _convertLineToClientObject(line);
				if (client.accountNumber() == accountNumber && client.pinCode == pinCode) {
					myFile.close();
					return client;
				}
			}
			myFile.close();
		}
		return _getEmptyClientObject();
	}


	static bool isClientExist(string accountNumber) {
		BankClient client1 = BankClient::find(accountNumber);
		return (!client1.isEmpty());
	}


	enSaveResults save() {
		switch (this->_mode) {
		case enMode::EmptyMode:
			return enSaveResults::svFaildEmptyObject;
		case enMode::UpdateMode:
			_update();
			return enSaveResults::svSucceeded;
		case enMode::AddNewMode:
			if (BankClient::isClientExist(_accountNumber))
				return enSaveResults::svFaildAccountNumberExists;
			else {
				_addNew();
				_mode = enMode::UpdateMode;
				return enSaveResults::svSucceeded;
			}
		}
	}

	bool deleteClient() {
		vector<BankClient> vClients = _loadClientsDataFromFile();
		for (BankClient& c : vClients) {
			if (c.accountNumber() == _accountNumber) {
				c._markedForDelete = true;
				break;
			}
		}
		_saveClientsDataToFile(vClients);
		*this = _getEmptyClientObject();

		return true;
	}

	static BankClient getAddNewClientObject(string accountNumber) {
		return BankClient(enMode::AddNewMode, "", "", "", "", accountNumber, "", 0);
	}

	static vector<BankClient> getClientsList() {
		return _loadClientsDataFromFile();
	}

	static float getTotalBalances() {
		vector<BankClient> vClients = BankClient::getClientsList();
		double totalBalances = 0;
		for (BankClient client : vClients) {
			totalBalances += client.accountBalance;
		}
		return totalBalances;
	}

private:
	string _prepareTransferRecord(double amountToTransfer, BankClient& destinationClient, string seperator = "#//#") {
		string clientRecord = "";

		clientRecord += Date::GetSystemDateTimeString() + seperator;
		clientRecord += this->accountNumber() + seperator;
		clientRecord += destinationClient.accountNumber() + seperator;
		clientRecord += to_string(amountToTransfer) + seperator;
		clientRecord += to_string(this->accountBalance) + seperator;
		clientRecord += to_string(destinationClient.accountBalance) + seperator;
		clientRecord += currentUser.userName;

		return clientRecord;
	}

	static vector<string> _loadTransferRecordsFromFile() {
		vector<string> vTransferRecords;
		fstream myFile;

		myFile.open("../Transferlog.txt", ios::in); // read mode

		if (myFile.is_open()) {
			string transferRecord;
			while (getline(myFile, transferRecord))
				vTransferRecords.push_back(transferRecord);
			myFile.close();
		}

		return vTransferRecords;
	}

	static TransferRecord _convertTransferRecordToObject(string record, string separator = "#//#") {
		struct  TransferRecord transferRecord;
		vector<string> vTransferRecordFields = String::split(record, separator);

		transferRecord.dateTime = vTransferRecordFields[0];
		transferRecord.sourceAccount = vTransferRecordFields[1];
		transferRecord.destinationAccount = vTransferRecordFields[2];
		transferRecord.transferedAmount = stoi(vTransferRecordFields[3]);
		transferRecord.sourceBalance = stoi(vTransferRecordFields[4]);
		transferRecord.destinationBalance = stoi(vTransferRecordFields[5]);
		transferRecord.username = vTransferRecordFields[6];

		return transferRecord;
	}

	void _registerTransfer(double amountToTransfer, BankClient& destinationClient) {
		fstream myFile;
		vector<string> vTransferRecords = _loadTransferRecordsFromFile();

		myFile.open("../TransferLog.txt", ios::out); // write mode

		if (myFile.is_open()) {
			myFile << _prepareTransferRecord(amountToTransfer, destinationClient) << endl;
			for (string record : vTransferRecords)
				myFile << record << endl;
			myFile.close();
		}
	}


public:
	void deposit(double amount) {
		_accountBalance += amount;
		save();
	}

	bool withdraw(double amount) {
		if (amount > _accountBalance)
			return false;
		else {
			_accountBalance -= amount;
			save();
			return true;
		}
	}

	bool transfer(double amountToTransfer, BankClient& destinationClient) {
		if (amountToTransfer > this->accountBalance)
			return false;

		this->withdraw(amountToTransfer);
		destinationClient.deposit(amountToTransfer);
		this->_registerTransfer(amountToTransfer, destinationClient);

		return true;
	}

public:
	static vector<TransferRecord> getTransferLogList() {
		vector<TransferRecord> vTransferRecordsObjects;
		string transferRecord;
		fstream myFile;

		myFile.open("../Transferlog.txt", ios::in); // read mode

		if (myFile.is_open()) {
			while (getline(myFile, transferRecord))
				vTransferRecordsObjects.push_back(_convertTransferRecordToObject(transferRecord));
			myFile.close();
		}

		return vTransferRecordsObjects;
	}
};