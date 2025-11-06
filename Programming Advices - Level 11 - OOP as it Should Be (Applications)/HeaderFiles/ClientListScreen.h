#pragma once

#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "BankClient.h"

class ClientListScreen : protected Screen {
private:
	static void _printClientRecordLine(BankClient client) {
		cout << setw(8) << left << "";
		cout << "| " << setw(15) << left << client.accountNumber();
		cout << "| " << setw(20) << left << client.fullName();
		cout << "| " << setw(12) << left << client.phone;
		cout << "| " << setw(20) << left << client.email;
		cout << "| " << setw(10) << left << client.pinCode;
		cout << "| " << setw(12) << left << client.accountBalance;
	}

public:
	static void showClientsList() {
		if (!checkAccessRights(User::enPermissions::listClients))
			return;// this will exit the function and it will not continue

		vector<BankClient> vClients = BankClient::getClientsList();
		string title = "\t  Client List Screen";
		string subTitle = "\t    (" + to_string(vClients.size()) + ") Client(s).";

		_drawScreenHeader(title, subTitle);

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;
		cout << setw(8) << left << "";
		cout << "| " << left << setw(15) << "Account Number";
		cout << "| " << left << setw(20) << "Client Name";
		cout << "| " << left << setw(12) << "Phone";
		cout << "| " << left << setw(20) << "Email";
		cout << "| " << left << setw(10) << "Pin Code";
		cout << "| " << left << setw(12) << "Balance";
		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;

		if (vClients.size() == 0)
			cout << "\t\t\t\tNo Clients Available In the System!";
		else
			for (const BankClient& client : vClients) {
				_printClientRecordLine(client);
				cout << endl;
			}

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;
	}
};