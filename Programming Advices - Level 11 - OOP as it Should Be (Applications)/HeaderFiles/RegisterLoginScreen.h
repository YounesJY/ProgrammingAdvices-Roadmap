#pragma once
#include <iostream>
#include "Screen.h"
#include "Person.h"
#include "BankClient.h"
#include "InputValidate.h"

class LoginRegisterScreen :protected Screen {
private:
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
	static void showDeleteClientScreen() {
		if (!checkAccessRights(User::enPermissions::DeleteClient))
			return;// this will exit the function and it will not continue


		string accountNumber = "";
		Input::choice answer;

		_drawScreenHeader("\tDelete Client Screen");
		accountNumber = InputValidate::readString("\nPlease Enter Account Number: ");

		while (!BankClient::isClientExist(accountNumber))
			accountNumber = InputValidate::readString("\nAccount number is not found, choose another one : ");

		BankClient client = BankClient::find(accountNumber);
		_printClient(client);


		answer = Input::getChoice("\nAre you sure you want to delete this client (yes / no) ? ", false);

		if (answer == Input::choice::yes) {
			if (client.deleteClient()) {
				cout << "\nClient Deleted Successfully :-)\n";
				_printClient(client);
			}
			else
				cout << "\nError Client Was not Deleted\n";
		}
	}

};

