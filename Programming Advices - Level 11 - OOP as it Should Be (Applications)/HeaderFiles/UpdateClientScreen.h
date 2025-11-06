#pragma once
#include <iostream>

#include "Screen.h"
#include "Person.h"
#include "BankClient.h"
#include "InputValidate.h"

class UpdateClientScreen :protected Screen {
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
	static void showUpdateClientScreen() {
		if (!checkAccessRights(User::enPermissions::updateClients))
			return;// this will exit the function and it will not continue


		string accountNumber = "";
		Input::choice answer;

		_drawScreenHeader("\tUpdate Client Screen");
		accountNumber = InputValidate::readString("\nPlease Enter Account Number: ");

		while (!BankClient::isClientExist(accountNumber))
			accountNumber = InputValidate::readString("\nAccount number is not found, choose another one : ");

		BankClient client = BankClient::find(accountNumber);
		_printClient(client);

		answer = Input::getChoice("\nAre you sure you want to delete this client (yes / no) ? ", false);

		if (answer == Input::choice::yes) {
			cout << "\n\nUpdate Client Info:";
			cout << "\n____________________\n";

			_readClientInfo(client);

			BankClient::enSaveResults saveResult;
			saveResult = client.save();

			switch (saveResult)
			{
			case  BankClient::enSaveResults::svSucceeded:
				cout << "\nAccount Updated Successfully :-)\n";
				_printClient(client);
				break;
			case BankClient::enSaveResults::svFaildEmptyObject:
				cout << "\nError account was not saved because it's Empty";
				break;
			}

		}

	}
};

