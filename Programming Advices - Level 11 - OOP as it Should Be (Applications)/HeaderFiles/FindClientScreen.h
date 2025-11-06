#pragma once

#include <iostream>
#include "Screen.h"
#include "Person.h"
#include "BankClient.h"
#include "InputValidate.h"

class FindClientScreen :protected Screen {
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

	static void showFindClientScreen() {
		if (!checkAccessRights(User::enPermissions::findClient))
			return;// this will exit the function and it will not continue


		string accountNumber = "";

		_drawScreenHeader("\tFind Client Screen");
		accountNumber = InputValidate::readString("\nPlease Enter Account Number: ");

		while (!BankClient::isClientExist(accountNumber))
			accountNumber = InputValidate::readString("\nAccount number is not found, choose another one : ");


		BankClient client = BankClient::find(accountNumber);

		if (!client.isEmpty())
			cout << "\nClient Found :-)\n";
		else
			cout << "\nClient Was not Found :-(\n";

		_printClient(client);
	}

};

