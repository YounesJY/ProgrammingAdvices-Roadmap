#pragma once

#include <iostream>
#include <iomanip>
#include "Screen.h"
#include "BankClient.h"
#include "InputValidate.h"

class AddNewClientScreen : protected Screen
{
private:
	static void _readClientInfo(BankClient& client) {
		cout << "\nEnter FirstName: ";
		client.firstName = InputValidate::readString();

		cout << "\nEnter LastName: ";
		client.lastName = InputValidate::readString();

		cout << "\nEnter Email: ";
		client.email = InputValidate::readString();

		cout << "\nEnter Phone: ";
		client.phone = InputValidate::readString();

		cout << "\nEnter PinCode: ";
		client.pinCode = InputValidate::readString();

		cout << "\nEnter Account Balance: ";
		client.accountBalance = InputValidate::readNumber<float>();
	}

	static void _printClient(BankClient Client) {
		cout << "\nClient Card:";
		cout << "\n___________________";
		cout << "\nFirstName   : " << Client.firstName;
		cout << "\nLastName    : " << Client.lastName;
		cout << "\nFull Name   : " << Client.fullName();
		cout << "\nEmail       : " << Client.email;
		cout << "\nPhone       : " << Client.phone;
		cout << "\nAcc. Number : " << Client.accountNumber();
		cout << "\nPassword    : " << Client.pinCode;
		cout << "\nBalance     : " << Client.accountBalance;
		cout << "\n___________________\n";
	}

public:

	static void showAddNewClientScreen() {
		if (!checkAccessRights(User::enPermissions::addNewClient))
			return;// this will exit the function and it will not continue


		string accountNumber = "";
		BankClient::enSaveResults saveResult;

		_drawScreenHeader("\t  Add New Client Screen");
		accountNumber = InputValidate::readString("\nPlease Enter Account Number: ");

		while (BankClient::isClientExist(accountNumber))
			accountNumber = InputValidate::readString("\nAccount Number Is Already Used, Choose another one: ");

		BankClient newClient = BankClient::getAddNewClientObject(accountNumber);


		_readClientInfo(newClient);

		saveResult = newClient.save();

		switch (saveResult)
		{
		case  BankClient::enSaveResults::svSucceeded: {
			cout << "\nAccount Addeded Successfully :-)\n";
			_printClient(newClient);
			break;
		}
		case BankClient::enSaveResults::svFaildEmptyObject: {
			cout << "\nError account was not saved because it's Empty";
			break;
		}
		case BankClient::enSaveResults::svFaildAccountNumberExists: {
			cout << "\nError account was not saved because account number is used!\n";
			break;
		}
		}
	}
};

